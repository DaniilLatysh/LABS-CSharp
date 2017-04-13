using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace WPF_Lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB_Connect db = new DB_Connect();
        public MainWindow()
        {
            InitializeComponent();

            db.LoadData();
            priceGrid.ItemsSource = db.priceTable.DefaultView;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            db.Update();
            priceGrid.ItemsSource = db.priceTable.DefaultView;
        }

        //-- Dele
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (priceGrid.SelectedItem != null)
            {
                for (int i = 0; i < priceGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = priceGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            db.Update();
        }
    }


    public class DB_Connect 
    {
        //-- You must add dll "System.Configuration.dll" ----
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlDataAdapter adapter;
        private SqlConnection connection = null;
        public DataTable priceTable;
        

        //-- Connection with database ----
        public void Connect()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("\n Success connection... \n");
            }
            catch
            {
                Console.WriteLine("\n No connection... \n");
            }
        }

        //-- Load all ----
        public void LoadData()
        {

            priceTable = new DataTable();

            try
            {
                Connect();

                SqlCommand command = new SqlCommand("SELECT * FROM Price", connection);
                adapter = new SqlDataAdapter(command);

                adapter.InsertCommand = new SqlCommand("insertRow", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar, 150, "ProductName"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal, 8, "Price"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@pCount", SqlDbType.Int, 0, "pCount"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Warranty", SqlDbType.Int, 0, "Warranty"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID ", SqlDbType.Int, 0, "ID");
                parameter.Direction = ParameterDirection.Output;

                adapter.Fill(priceTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

        }

        public void Update()
        {
            try
            {
                SqlCommandBuilder cb = new SqlCommandBuilder(adapter);
                adapter.Update(priceTable);
            }
            catch
            {
                LoadData();
            }
        }

    }
}
