using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WPF_Lab8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection connection = null;
        private DataSet dataSetOrder = null;
        private DataSet dataSetPrice = null;
        private SqlDataAdapter adapterOrder = new SqlDataAdapter();
        private SqlDataAdapter adapterPrice = new SqlDataAdapter();


        public MainWindow()
        {
            InitializeComponent();
            ConnectToData(connectionString);
        }


        //-- Connection ----
        public void ConnectToData(string connectionString)
        {
            //Create a SqlConnection to the Northwind database.
            try
            {
                connection = new SqlConnection(connectionString);
                // Open the connection.
                connection.Open();

                Console.WriteLine("The SqlConnection is open.");

                OrdersTable();
                
                // Close the connection
                connection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Exeption: " + e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        //-- Orders ----
        public void OrdersTable()
        {
            dataSetOrder = new DataSet();

            // Create a SqlCommand to retrieve Suppliers data.
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Orders]", connection);
            command.CommandType = CommandType.Text;

            // Set the SqlDataAdapter's SelectCommand.
            adapterOrder.SelectCommand = command;

            // Configuring the adapter 
            adapterOrder.InsertCommand = new SqlCommand("insertOrder", connection); // calling procedure
            adapterOrder.InsertCommand.CommandType = CommandType.StoredProcedure;
            adapterOrder.InsertCommand.Parameters.Add(new SqlParameter("@NameClient", SqlDbType.NVarChar, 150, "NameClient"));
            adapterOrder.InsertCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.Int, 7, "PhoneNumberClient"));
            adapterOrder.InsertCommand.Parameters.Add(new SqlParameter("@Adress", SqlDbType.NVarChar, 150, "AddressClient"));
            SqlParameter parameter = adapterOrder.InsertCommand.Parameters.Add("@ID_Order ", SqlDbType.Int, 0, "ID_Order");
            parameter.Direction = ParameterDirection.Output;

            // Fill the DataSet.
            adapterOrder.Fill(dataSetOrder);
            Order.ItemsSource = dataSetOrder.Tables[0].DefaultView;
            
        }


        //-- Price ----
        public void PriceTable(SqlCommand command)
        {

            // Create a SqlCommand to retrieve Suppliers data.
            
            command.CommandType = CommandType.Text;

            dataSetPrice = new DataSet();
            
            // Set the SqlDataAdapter's SelectCommand.
            adapterPrice.SelectCommand = command;

            // Configuring the adapter 
            adapterPrice.InsertCommand = new SqlCommand("InsertPrice", connection); // calling procedure
            adapterPrice.InsertCommand.CommandType = CommandType.StoredProcedure;
            adapterPrice.InsertCommand.Parameters.Add(new SqlParameter("@Сategory", SqlDbType.NVarChar, 150, "Сategory"));
            adapterPrice.InsertCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 150, "Name"));
            adapterPrice.InsertCommand.Parameters.Add(new SqlParameter("@PartCount", SqlDbType.Int, 0, "PartCount"));
            adapterPrice.InsertCommand.Parameters.Add(new SqlParameter("@Cost", SqlDbType.Int, 18, "Cost"));
            SqlParameter parameter = adapterPrice.InsertCommand.Parameters.Add("@ID_Part", SqlDbType.Int, 0, "ID_Part");
            parameter.Direction = ParameterDirection.Output;
            connection.ConnectionString = connectionString;
            // Fill the DataSet.
            adapterPrice.Fill(dataSetPrice);
            Price.ItemsSource = dataSetPrice.Tables[0].DefaultView;


        }

        //-- U P D A T E ----

        public void Update(SqlDataAdapter adapter, DataSet dataSet)
        {
            try
            {
                connection.Close();

                connection.ConnectionString = connectionString;

                connection.Open();

                adapter.Update(dataSet);

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        //-- O R D E R ----

        private void OrderUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            LockBtn(true);
            adapterOrder.UpdateCommand = new SqlCommandBuilder(adapterOrder).GetUpdateCommand();
            Update(adapterOrder, dataSetOrder);
        }

        private void OrderDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            LockBtn(true);
            if (Order.SelectedItem != null)
            {
                for (int i = 0; i < Order.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = Order.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = datarowView.Row;
                        dataRow.Delete();
                        
                    }
                }
            }
            connection.ConnectionString = connectionString;
            adapterOrder.DeleteCommand = new SqlCommandBuilder(adapterOrder).GetDeleteCommand();

            Update(adapterOrder, dataSetOrder);
        }


        //-- P R I C E ----

        private void PriceUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            adapterPrice.UpdateCommand = new SqlCommandBuilder(adapterPrice).GetUpdateCommand();
            Update(adapterPrice, dataSetPrice);
            
        }

        private void PriceDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Price.SelectedItem != null)
            {
                for (int i = 0; i < Price.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = Price.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            //adapterPrice.DeleteCommand = new SqlCommandBuilder(adapterPrice).GetDeleteCommand();

            Update(adapterPrice, dataSetPrice);
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Price]", connection))
            {
                LockBtn(true);
                PriceTable(command);
                AddOrder orderWin = new AddOrder(dataSetOrder, dataSetPrice, connection, connectionString);
                orderWin.Show();
            }
        }


        private void Order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LockBtn(true);

                int id = 0;
                if (Order.SelectedItem != null)
                {
                    int selectedColumn = Order.CurrentCell.Column.DisplayIndex;
                    var selectedCell = Order.SelectedCells[selectedColumn];
                    var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                    if (cellContent is TextBlock)
                    {
                        string str = (cellContent as TextBlock).Text.ToString();
                        id = int.Parse(str);
                        SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Price],[dbo].[DoOrders] WHERE ID_Order = " + id + " AND ID_Part = ID_PartOrder", connection);
                        PriceTable(command);
                    }
                }
            }
            catch
            {

                connection.ConnectionString = connectionString;
                Update(adapterOrder, dataSetOrder);
            }
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Price]", connection))
            {
                PriceTable(command);
                LockBtn(false);
            }

            
        }


        //-- L O C K  -  B U T T O N S ----
        private void LockBtn(bool state) {
            if (state) // Lock
            {
                Price.IsReadOnly = true;
                PriceUpdateButton.IsEnabled = false;
                PriceDeleteButton.IsEnabled = false;
                OrderDeleteButton.IsEnabled = true;
            }
            else // Unlock
            {
                Price.IsReadOnly = false;
                PriceUpdateButton.IsEnabled = true;
                PriceDeleteButton.IsEnabled = true;
                OrderDeleteButton.IsEnabled = false;
            }
        }
    }


}
