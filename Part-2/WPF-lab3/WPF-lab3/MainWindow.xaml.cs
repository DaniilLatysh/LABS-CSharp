using System.Windows;
using System.Windows.Controls;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace WPF_lab3
{


    public partial class MainWindow : Window
    {
        private double g_xBegin = 0;
        private double g_xEnd = 0;
        private double g_Stap = 0;
        private int g_nCount = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void CheckParam() {
            try
            {
                double xBegin = g_xBegin;
                double end = g_xEnd;
                double stap = g_Stap;
                int n = g_nCount;
                double y = 0;



                if (xBegin < end)
                {
                    for (double i = xBegin; i < end; i += stap)
                    {
                        double S = 0;
                        int k = 0;

                        y = (Math.Log((1 + i) / (1 - i)) / 4) + Math.Atan(1 / 2);

                        for (int j = 0; j <= n; j++)
                        {
                            S = (Math.Pow(i, 4 * k + 1) / (4 * k + 1));
                        }
                        result.Text += "X: " + i + "\tY: " + (float)y + "\t S: " + (float)S + "\n";
                    }
                }
                else
                {
                    for (double i = xBegin; i > end; i += -stap)
                    {
                        double S = 0;
                        int k = 0;

                        y = (Math.Log((1 + i) / (1 - i)) / 4) + Math.Atan(1 / 2);

                        for (int j = 0; j <= n; j++)
                        {
                            S = (Math.Pow(i, 4 * k + 1) / (4 * k + 1));
                        }

                        result.Text += "X: " + i + "\tY: " + (float)y + "\t S: " + (float)S + "\n";
                    }
                }

            }
            catch (OverflowException e)
            {
                MessageBox.Show("Exception: " + e);
            }

        }

        private void xBegin_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                xErr1.Visibility = Visibility.Hidden;
                double temp = double.Parse(xBegin.Text.ToString());
                g_xBegin = temp;

            }
            catch
            {
                xErr1.Visibility = Visibility.Visible;
                xErr1.ToolTip = "Incorrect value!";
            }
            
        }

        private void xEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                xErr2.Visibility = Visibility.Hidden;
                double temp = double.Parse(xEnd.Text.ToString());
                if (g_xBegin != temp)
                {
                    g_xEnd = temp;
                }
                else
                {
                    xErr2.Visibility = Visibility.Visible;
                    xErr2.ToolTip = "Value equals x begin!";
                }
            }
            catch
            {
                xErr2.Visibility = Visibility.Visible;
                xErr2.ToolTip = "Incorrect value!";
            }

        }

        private void hStap_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                hErr.Visibility = Visibility.Hidden;

                double temp = double.Parse(hStap.Text.ToString());
                if (temp > 0)
                {
                    g_Stap = temp;
                }
                else
                {
                    hErr.Visibility = Visibility.Visible;
                    hErr.ToolTip = "Stap must be large 0!";
                }           
            }
            catch
            {

                hErr.Visibility = Visibility.Visible;
                hErr.ToolTip = "Incorrect value!";
            }
        }

        private void nCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                nErr.Visibility = Visibility.Hidden;

                int temp = int.Parse(nCount.Text.ToString());
                if (temp > 0)
                {
                    g_nCount = temp;
                    
                }
                else
                {
                    nErr.Visibility = Visibility.Visible;
                    nErr.ToolTip = "N must be large 0!";
                }
            }
            catch
            {
                nErr.Visibility = Visibility.Visible;
                nErr.ToolTip = "Incorrect value!";

            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            result.Clear();
            CheckParam();
        }
    }
}
