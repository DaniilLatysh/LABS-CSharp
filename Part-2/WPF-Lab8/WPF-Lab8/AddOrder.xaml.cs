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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;

namespace WPF_Lab8
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        private SqlConnection connection = null;
        private string connectionString = null;
        private DataSet dataSetClients;
        private DataSet dataSetPrice;
        private DataSet dataSetDoOreders;
        private SqlDataAdapter adapterDoOrder = new SqlDataAdapter();
        private SqlCommand command;
        private ErrorID checkID;
        public AddOrder(DataSet clientsDS, DataSet priceDS, SqlConnection connection, string connectionString)
        {
            InitializeComponent();

            dataSetClients = clientsDS;
            dataSetPrice = priceDS;

            this.connectionString = connectionString;
            this.connection = connection;
            checkID = new ErrorID();
            this.DataContext = checkID;
            Clients.ItemsSource = dataSetClients.Tables[0].DefaultView;
            Price.ItemsSource = dataSetPrice.Tables[0].DefaultView;
        }

        public DataSet dataClients
        {
            get { return dataSetClients; }
            set { dataSetClients = value; }
        }

        public DataSet dataPrice
        {
            get { return dataSetClients; }
            set { dataSetClients = value; }
        }



        //-- DoOrders ----
        public void OrdersTable(SqlCommand command)
        {
            dataSetDoOreders = new DataSet();

            // Create a SqlCommand to retrieve Suppliers data.
            
            command.CommandType = CommandType.Text;

            // Set the SqlDataAdapter's SelectCommand.
            adapterDoOrder.SelectCommand = command;

            // Configuring the adapter 
            adapterDoOrder.InsertCommand = new SqlCommand("NewOrder", connection); // calling procedure
            adapterDoOrder.InsertCommand.CommandType = CommandType.StoredProcedure;
            adapterDoOrder.InsertCommand.Parameters.Add(new SqlParameter("@ID_Order", SqlDbType.Int, 8, "ID_Order"));
            adapterDoOrder.InsertCommand.Parameters.Add(new SqlParameter("@ID_PartOrder", SqlDbType.Int, 8, "ID_Part"));
            adapterDoOrder.InsertCommand.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 8, "Count"));

            // Fill the DataSet.
            adapterDoOrder.Fill(dataSetDoOreders);
            

        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);


                connection.Open();

                command = new SqlCommand("INSERT INTO [dbo].[DoOrders] (ID_Order,ID_PartOrder,CountPart) VALUES(" + checkID.ID_Order + ", " + checkID.ID_Part + ", " + checkID.Count + ")", connection);
                OrdersTable(command);

                MessageBox.Show("Complite!");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Invalid value, or this item is already exist in the order! ");
                //MessageBox.Show("ERROR: " + ex.Message);
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    public interface IDataErrorInfo
    {
        string Error { get; }
        string this[string columnName] { get; }
    }

    public class ErrorID : IDataErrorInfo
    {
       

        public int ID_Order { get; set; }
        public int ID_Part { get; set; }
        public int Count { get; set; }

        /*
        private int idValue;

        public int ID_Order {
            get { return ID_Order; }
            set { if (IsIdValid(value) && idValue != value) idValue = value; }
        }
        public int ID_Part
        {
            get { return ID_Part; }
            set { if (IsIdValid(value) && idValue != value) idValue = value; }
        }
        public int Count
        {
            get { return Count; }
            set { if (IsIdValid(value) && idValue != value) idValue = value; }
        }
        */

        private Dictionary<String, List<String>> errors =
            new Dictionary<string, List<string>>();
        private const string ID_ERROR = "Value cannot be less than 0.";

        // Validates the Id property, updating the errors collection as needed.
        public bool IsIdValid(int value)
        {
            bool isValid = true;

            if (value <= 0)
            {
                AddError("Id", ID_ERROR, false);
                isValid = false;
            }
            else RemoveError("Id", ID_ERROR);

            return isValid;
        }

        // Adds the specified error to the errors collection if it is not already 
        // present, inserting it in the first position if isWarning is false. 
        public void AddError(string propertyName, string error, bool isWarning)
        {
            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<string>();

            if (!errors[propertyName].Contains(error))
            {
                if (isWarning) errors[propertyName].Add(error);
                else errors[propertyName].Insert(0, error);
            }
        }

        // Removes the specified error from the errors collection if it is present. 
        public void RemoveError(string propertyName, string error)
        {
            if (errors.ContainsKey(propertyName) &&
                errors[propertyName].Contains(error))
            {
                errors[propertyName].Remove(error);
                if (errors[propertyName].Count == 0) errors.Remove(propertyName);
            }
        }

        
        #region 
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string propertyName]
        {
            get
            {
                return (!errors.ContainsKey(propertyName) ? null :
                    String.Join(Environment.NewLine, errors[propertyName]));
            }
        }
        #endregion

    }
}
