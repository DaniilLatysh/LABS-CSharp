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

                pers.Name = name.Text;
                pers.Pay = double.Parse(pay.Text);
                pers.Post = post.Text;
                pers.Sity = Sity.Text;
                pers.Street = street.Text;
                pers.NumberStreet = int.Parse(Number.Text);

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

    }
  

    public class Person : IDataErrorInfo
    {
        
        private string _Name;
        private double _Pay;
        private string _Post;
        private string _Sity;
        private string _Street;
        private int _NumberStreet;

   

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public double Pay
        {
            get
            {
                return _Pay;
            }
            set
            {
                _Pay = value;
            }
        }
        public string Post
        {
            get
            {
                return _Post;
            }
            set
            {
                _Post = value;
            }
        }

        public string Sity
        {
            get
            {
                return _Sity;
            }
            set
            {
                _Sity = value;
            }
        }
        public string Street
        {
            get
            {
                return _Street;
            }
            set
            {
                _Street = value;
            }
        }

        public int NumberStreet
        {
            get
            {
                return _NumberStreet;
            }
            set
            {
                _NumberStreet = value;
            }
        }




        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Pay":
                        if ((Pay < 0))
                        {
                            error = "Incorrect value";
                        }
                        break;
                    case "Name":
                        if (String.IsNullOrEmpty(Name))
                        {
                            error = "Input name";
                        }
                        break;
                    case "Post":
                        if (String.IsNullOrEmpty(Post))
                        {
                            error = "Input post";
                        }
                        break;
                    case "Sity":
                        if (String.IsNullOrEmpty(Post))
                        {
                            error = "Input Sity";
                        }
                        break;
                    case "street":
                        if (String.IsNullOrEmpty(Post))
                        {
                            error = "Input Sreet";
                        }
                        break;
                    case "NumberStreet":
                        if ((NumberStreet < 0))
                        {
                            error = "Incorrect value";
                        }
                        break;
                }
                return error;
            }
           
           
        }

        public string Error
        {
            get
            {
                return String.Empty;
            }
        }

        
        public override string ToString()
        {
            return (_Name + " " + _Pay + " " + _Post + " " + _Sity + " " + _Street + " " + _NumberStreet);
        }
        
    }
    
}
