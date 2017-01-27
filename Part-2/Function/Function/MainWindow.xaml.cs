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

namespace Function
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static CheckBox minCkB;
        private static CheckBox maxCkB;
        private static double min;
        private static double max;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                ResultRow();
                InputBox();
                CheckPanel();
                CheckBox();
                StartBtn();


                foreach (UIElement el in Layout.Children)
                {
                    if (el is TextBox)
                    {
                        FontSize = 18;
                        ((TextBox)el).TextAlignment = TextAlignment.Center;
                        ((TextBox)el).Padding = new Thickness(5, 30, 5, 5);
                    }
                    else if (el is StackPanel)
                    {
                        ((StackPanel)el).HorizontalAlignment = HorizontalAlignment.Center;
                        ((StackPanel)el).VerticalAlignment = VerticalAlignment.Center;
                    }
                    else if (el is Label)
                    {
                        ((Label)el).HorizontalAlignment = HorizontalAlignment.Center;
                        ((Label)el).VerticalAlignment = VerticalAlignment.Center;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(" Error: " + e.Message);
            }

        }

        private void ResultRow()
        {

            //RESULT

            //Title
            Label resLab = new Label();
            resLab.Content = "- RESULT -";
            Grid.SetColumn(resLab, 0);
            Grid.SetRow(resLab, 0);

            //Text
            TextBox tBoxResult = new TextBox();
            tBoxResult.Name = "res";
            Grid.SetColumn(tBoxResult, 0);
            Grid.SetRow(tBoxResult, 1);


            //BRUNCH

            //Title
            Label resBr = new Label();
            resBr.Content = "- BRUNCH -";
            Grid.SetColumn(resBr, 1);
            Grid.SetRow(resBr, 0);

            //Text
            TextBox tBoxBraanch = new TextBox();
            tBoxBraanch.Name = "brunch";
            Grid.SetColumn(tBoxBraanch, 1);
            Grid.SetRow(tBoxBraanch, 1);


            //MIN or MAX

            //Title
            Label resMax = new Label();
            resMax.Content = "- M -";
            Grid.SetColumn(resMax, 2);
            Grid.SetRow(resMax, 0);

            //Text
            TextBox tBoxMax = new TextBox();
            tBoxMax.Name = "Max";
            Grid.SetColumn(tBoxMax, 2);
            Grid.SetRow(tBoxMax, 1);


            //Add to layout
            
            Layout.Children.Add(resLab);
            Layout.Children.Add(resBr);
            Layout.Children.Add(resMax);

            Layout.Children.Add(tBoxResult);
            Layout.Children.Add(tBoxBraanch);
            Layout.Children.Add(tBoxMax);
            
        }


        private void InputBox()
        {
            //X

            //Title
            Label tileX = new Label();
            tileX.Content = "- x -";
            Grid.SetColumn(tileX, 0);
            Grid.SetRow(tileX, 2);

            //Text
            TextBox tBoxX = new TextBox();
            tBoxX.Name = "x";
            Grid.SetColumn(tBoxX,0);
            Grid.SetRow(tBoxX, 3);

            //Y

            //Title
            Label tileY = new Label();
            tileY.Content = "- y -";
            Grid.SetColumn(tileY, 1);
            Grid.SetRow(tileY, 2);

            //Text
            TextBox tBoxY = new TextBox();
            tBoxY.Name = "y";
            Grid.SetColumn(tBoxY, 1);
            Grid.SetRow(tBoxY, 3);
            


            //Add to layout

            Layout.Children.Add(tileX);
            Layout.Children.Add(tileY);

            Layout.Children.Add(tBoxX);
            Layout.Children.Add(tBoxY);
        }


        private void CheckPanel()
        {
            StackPanel sp = new StackPanel();
            sp.Name = "chkRbFunk";
            Grid.SetColumn(sp,0);
            Grid.SetRow(sp, 4);

            RadioButton sin = new RadioButton();
            sin.Name = "sin";
            sin.Content = "Sin(x)";
            sin.IsChecked = true;

            RadioButton cos = new RadioButton();
            cos.Name = "cos";
            cos.Content = "Cos(x)";

            RadioButton tg = new RadioButton();
            tg.Name = "tg";
            tg.Content = "Tg(x)";


            sp.Children.Add(sin);
            sp.Children.Add(cos);
            sp.Children.Add(tg);
            Layout.Children.Add(sp);
        }

        private void CheckBox()
        {
            StackPanel sp = new StackPanel();
            sp.Name = "chkBoxFunk";
            Grid.SetColumn(sp, 1);
            Grid.SetRow(sp, 4);

            minCkB = new CheckBox();
            minCkB.Content = "Min";
            minCkB.Checked += CheackMin_Checked;

            maxCkB = new CheckBox();
            maxCkB.Content = "Max";
            maxCkB.IsChecked = true;
            maxCkB.Checked += CheackMax_Checked;


            sp.Children.Add(minCkB);
            sp.Children.Add(maxCkB);

            Layout.Children.Add(sp);

        }

        private void StartBtn() {
            Button btn = new Button();
            btn.Content = "Start";
            btn.Click += Start;
            btn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            Grid.SetColumn(btn, 2);
            Grid.SetRow(btn, 3);
            Grid.SetRowSpan(btn, 4);

            Layout.Children.Add(btn);
        }

        private void CheackMin_Checked(object sender, RoutedEventArgs e)
        {
            if (maxCkB.IsChecked == true) {
                maxCkB.IsChecked = false;
            }
        }

        private void CheackMax_Checked(object sender, RoutedEventArgs e)
        {
            if (minCkB.IsChecked == true)
            {
                minCkB.IsChecked = false;
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = 0, y = 0, z = 0, fx = 0, brunch = 0;
                string brcText = "";

                //--Get param----
                foreach (UIElement el in Layout.Children)
                {
                    if (el is TextBox)
                    {
                        if (((TextBox)el).Name == "x")
                        {
                            x = double.Parse(((TextBox)el).Text);
                            ((TextBox)el).Text = "";
                        }
                        else if (((TextBox)el).Name == "y")
                        {
                            y = double.Parse(((TextBox)el).Text);
                            ((TextBox)el).Text = "";
                        }

                    }
                }

                //--Check radio button----

                foreach (UIElement el in Layout.Children)
                {
                    if (el is RadioButton)
                    {
                        if (((RadioButton)el).Name == "sin" && ((RadioButton)el).IsChecked == true)
                        {
                            fx = Math.Sin(x);
                        }
                        if (((RadioButton)el).Name == "cos" && ((RadioButton)el).IsChecked == true)
                        {
                            fx = Math.Cos(x);
                        }
                        else
                        {
                            fx = Math.Tan(x);
                        }
                    }
                }


              
                
                //--MATH----
                if (x > Math.Abs(y))
                { 
                    z = (2 * Math.Pow(fx, 3)) + (3 * Math.Pow(y, 2));
                    brunch = 1;
                    brcText = "X > |Y|";
                }
                else if (3 < x && x < Math.Abs(y))
                {
                    z = Math.Abs(fx - y);
                    brunch = 2;
                    brcText = "3 < X < |Y|";
                }
                else if (x == Math.Abs(y))
                {
                    z = Math.Pow((fx - y), 2);
                    brunch = 3;
                    brcText = "X = |Y|";
                }
                else {
                    MessageBox.Show(" xz");
                }



                min = z;
                if (min > max)
                {
                    max = min;
                }


                //--Set result----
                foreach (UIElement el in Layout.Children)
                {
                    if (el is TextBox)
                    {
                        if (((TextBox)el).Name == "res")
                        {
                            ((TextBox)el).Text = z.ToString();
                        }
                        else if (((TextBox)el).Name == "brunch")
                        {
                            ((TextBox)el).Text = "Brunch: " + brunch.ToString() + "\n" + brcText;
                        }
                        else if (((TextBox)el).Name == "Max")
                        {

                            if (maxCkB.IsChecked == true)
                            {
                                ((TextBox)el).Text = max.ToString();
                            }
                            else if (minCkB.IsChecked == true)
                            {
                                ((TextBox)el).Text = min.ToString();
                            }
                            else {
                                ((TextBox)el).Text = "";
                            }
                        }
                    }
                }


            }
            catch (FormatException)
            {
                MessageBox.Show(" You must enter the number!");
            }
        }
    }
}
