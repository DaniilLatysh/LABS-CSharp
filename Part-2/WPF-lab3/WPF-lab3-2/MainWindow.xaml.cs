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

namespace WPF_lab3_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {
        private string Name;
        private int Pay;
        private string Post;
        private string City;
        private string Street;
        private int NumberStreet;

        
        public MainWindow()
        {
            InitializeComponent();

            string item = "";
            StreamReader rd = new StreamReader("data.txt");

            while ((item = rd.ReadLine()) != null) {
                list.Items.Add(item);
            }
        }
       

        private void ComponentsBox() {
            
        }

        private void AddPerson() {
            try
            {
                string item = "";

                Name = name.Text.ToString();
                Pay = int.Parse(pay.Text.ToString());
                Post = post.Text.ToString();
                City = city.Text.ToString();
                Street = street.Text.ToString();
                NumberStreet = int.Parse(Number.Text.ToString());

                if (Name.Length > 3 && Pay > 0 && Post.Length > 3 && City.Length > 2 && Street.Length > 3 && NumberStreet > 0)
                {

                    item += Name + " | " + Pay + " | " + Post + " | " + City + " | " + Street + " | " + NumberStreet;
                    list.Items.Add(item);
                    StreamWriter sw = new StreamWriter("data.txt");
                    sw.WriteLine(item);
                }
                else
                {
                    MessageBox.Show("Incorrect values!");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: ", e.Message);
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
                AddPerson();
            
        }

    }

}
