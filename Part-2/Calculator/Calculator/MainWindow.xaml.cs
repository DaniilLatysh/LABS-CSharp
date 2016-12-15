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


namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string left = "0";
        private static string right = "";
        private static string oper = "";
        private static string specOperPer = "";
        private static int count = 0;


        public MainWindow()
        {
            InitializeComponent();
            //initialize all button for click
            foreach (UIElement e in Layout.Children)
            {
                if (e is Button)
                {
                    ((Button)e).Click += ClickB;
                }
            }
        }

        //events
        private void ClickB(object sender, RoutedEventArgs e)
        {
            View.FontSize = 40;
            //get date of button
            string str = (string)((Button)(e.OriginalSource)).Content;

            //check on number
            double num;
            bool check = double.TryParse(str, out num);

            if (check) //true - number || false - other
            {
                View.Text += str;
                if (oper == "")
                {
                    left += str;
                }
                right += str;
            }
            else if (str == "CL")
            {
                Clear();
            }
            else if (str == "SIN")
            {
                oper = str;
                Func();
                View.Text = right;
                oper = "";
            }
            else if (str == "COS")
            {
                oper = str;
                Func();
                View.Text = right;
                oper = "";
            }
            else if (str == "TG")
            {
                oper = str;
                Func();
                View.Text = right;
                oper = "";
            }
            else if (str == "SQR")
            {
                oper = str;
                Func();
                View.Text = right;
                oper = "";
            }
            else if (str == "BS")
            {
                bs_Click();
            }
            else if (str == ".")
            {
                if (oper == "")
                {
                    left += ".";
                    View.Text = left;
                }
                    right += ".";
                    View.Text = right;
            }
            else if (str == "%")
            {
                specOperPer = "%";
                Func();
                View.Text = right;
                oper = "";
               
            }
            else if (str == "=" || oper != "")
            {
                Func();
                View.Text = right;
                oper = "";
            }
            else
            {
                if (right != "")
                {
                    View.Text += str;
                    Func();
                    left = right;
                    right = "";
                }
                oper = str;
            }
        }

        private void Clear()
        {
            View.Text = "";
            left = "";
            right = "";
            oper = "";
        }

        private void Func()
        {
            try
            {
                double leftD = double.Parse(left);
                double rightD = double.Parse(right);
                string specOper = check.Content.ToString();
                double res = 0;
                double ed = 0;



                if (specOper == "RAD")
                {
                    ed = leftD;
                }
                else if (specOper == "DEG")
                {
                    ed = leftD * Math.PI / 180;
                }

                if (specOperPer == "%" && oper == "")
                {
                    right = (leftD / 100).ToString();
                }
                else if (specOperPer == "%") {
                    rightD = (leftD * rightD) / 100;
                }
                specOperPer = "";

                switch (oper)
                {
                    case "+":
                        right = (leftD + rightD).ToString();
                        break;

                    case "-":
                        right = (leftD - rightD).ToString();
                        break;

                    case "*":
                        right = (leftD * rightD).ToString();
                        break;

                    case "/":
                        if (leftD != 0 && rightD != 0)
                        {
                            right = (leftD / rightD).ToString();
                        }
                        else
                        {
                            View.FontSize = 18;
                            right = " Divizion by zero!";
                            break;
                        }
                        break;

                    case "COS":
                        res = Math.Cos(ed);
                        right = res.ToString();
                        break;

                    case "SIN":
                        res = Math.Sin(ed);
                        right = res.ToString();
                        break;

                    case "TG":
                        res = Math.Tan(ed);
                        right = res.ToString();
                        break;

                    case "SQR":
                        res = Math.Sqrt(leftD);
                        right = res.ToString();
                        break;

                    default:
                        break;
                }
            }
            catch (FormatException) {
                View.Text = "0";
            }
            catch (Exception e)
            {
                View.FontSize = 14;
                View.Text = e.Message;
            }
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ++count;
                switch (count)
                {
                    case 1:
                        check.Content = "RAD";
                        check.Background = new SolidColorBrush(Color.FromRgb(165, 200, 210));
                        break;

                    case 2:
                        check.Content = "DEG";
                        count = 0;
                        check.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                        break;

                    default:
                        break;
                }
                Func(); //bcs need resent content...
            }
            catch (Exception ex)
            {
                View.FontSize = 14;
                View.Text = ex.Message;
            }

        }

      
        private void bs_Click()
        {
            try
            {
                //delete before operator
                if (oper != "")
                {
                    right = right.Substring(0, right.Length - 1);
                    View.Text = left + oper + right;
                }
                //delete operator
                else if (oper != "" && right == "")
                {
                    oper = "";
                    View.Text = left;
                }
                //delete left size
                else if (oper == "")
                {
                    left = left.Substring(0, left.Length - 1);
                    View.Text = left;
                }      
            }
            catch
            {
                View.Text = "0";
            }
            
        }
    }
}
