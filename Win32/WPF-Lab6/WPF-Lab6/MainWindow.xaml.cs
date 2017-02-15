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

namespace WPF_Lab6
{


    public partial class MainWindow : Window
    {
        private double xi = 0, a = 0, b = 0, d = 0, h = 0, i = 0, N = 0;

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            ValidationForm();
        }

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ValidationForm() {
            try
            {
                a = Convert.ToDouble(aBox.Text.ToString());
                b = Convert.ToDouble(bBox.Text.ToString());
                d = Convert.ToDouble(dBox.Text.ToString());
                i = Convert.ToDouble(iBox.Text.ToString());
                N = Convert.ToDouble(NBox.Text.ToString());

                Function();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void Function()
        {
            h = (b - a) / N; 
            xi = a + h * i;
        }



    }
}
