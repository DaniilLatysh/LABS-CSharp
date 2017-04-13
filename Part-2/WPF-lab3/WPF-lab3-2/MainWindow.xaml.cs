using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.IO;
using System.ComponentModel;

namespace WPF_lab3_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        private ListBox worker = new ListBox();
        Person pers;


        public MainWindow()
        {
            try
            {
                InitializeComponent();
                pers = new Person();
                this.DataContext = pers;

                string item = "";


                StreamReader rd = new StreamReader("data.txt", true);

                while ((item = rd.ReadLine()) != null)
                {
                    list.Items.Add(item);
                }
                rd.Close();
            }
            catch {
                File.Create("data.txt");
            }
        }



        private void AddPerson()
        {
            try
            {

                pers._Name = name.Text;
                pers._Pay = int.Parse(pay.Text);
                pers._Post = post.Text;
                pers._Sity = Sity.Text;
                pers._Street = street.Text;
                pers._NumberStreet = int.Parse(Number.Text);

                string item = pers.ToString();
                list.Items.Add(item);

                List<Person> col = new List<Person>();
                col.Add(pers);

                StreamWriter sw = new StreamWriter("data.txt", true);
                
                foreach (Person items in col)
                {
                    sw.WriteLine(items.ToString());
                }

                sw.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }

        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            AddPerson();
        }
        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }

    }
    public interface IDataErrorInfo
    {
        string Error { get; }
        string this[string columnName] { get; }
    }

    public class Person : IDataErrorInfo
    {
        
        public string _Name { get; set; }
        public string _Post { get; set; }
        public string _Sity { get; set; }
        public string _Street { get; set; }

        private int pValue, sValue;
        public int _Pay
        {
            get { return pValue; }
            set { if (IsIdValid(value) && pValue != value) pValue = value; }
        }

        public int _NumberStreet
        {
            get { return sValue; }
            set { if (IsIdValid(value) && sValue != value) sValue = value; }
        }


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

        public override string ToString()
        {
            return (_Name + " " + _Pay + " " + _Post + " " + _Sity + " " + _Street + " " + _NumberStreet);
        }
        
    }
    
}
