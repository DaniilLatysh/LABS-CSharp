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
            InitializeComponent();

            string item = "";
            StreamReader rd = new StreamReader("data.txt");

            while ((item = rd.ReadLine()) != null)
            {
                list.Items.Add(item);
            }
        }



        private void AddPerson()
        {


            pers = new Person();
            this.DataContext = pers;

            pers.Name = name.Text;
            pers.Pay = int.Parse(pay.Text.ToString());

            string item = pers.ToString();
            list.Items.Add(item);





            StreamWriter sw = new StreamWriter("data.txt");
            foreach (var items in worker.Items)
            {
                sw.WriteLine(items.ToString());
            }
            sw.Close();

        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            AddPerson();
        }

    }
  

    public class Person //: IDataErrorInfo
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
                if (value == "")
                {
                    throw new NotImplementedException("Incorrect Value!");
                }
                else
                {
                    _Name = value;
                }
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
                if (value <= 0)
                {
                    throw new NotImplementedException("Value must be more 0!");
                }
                else
                {
                    _Pay = value;
                }
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
                if (value == "")
                {
                    throw new NotImplementedException("Incorrect Value!");
                }
                else
                {
                    _Post = value;
                }
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
                if (value == "")
                {
                    throw new NotImplementedException("Incorrect Value!");
                }
                else
                {
                    _Sity = value;
                }
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
                if (value == "")
                {
                    throw new NotImplementedException("Incorrect Value!");
                }
                else
                {
                    _Street = value;
                }
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
                if (value <= 0)
                {
                    throw new NotImplementedException("Value must be more 0!");
                }
                else
                {
                    _NumberStreet = value;
                }
            }
        }
        /*
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }*/

        public override string ToString()
        {
            return (_Name + " " + _Pay + " " + _Post + " " + _Sity + " " + _Street + " " + _NumberStreet);
        }

    }
    
}
