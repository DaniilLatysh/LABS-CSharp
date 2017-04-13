using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WPF_Lab6
{



    public class Work
    {
        private int N = 0;
        private double y = 0, a = 0, b = 0, h = 0;
        private bool _checkComplite = false;

        public event Action<double> ProgressChanged;

        public event Action<bool, double> WorkComlite;


        public Work(double a, double b, int N)
        {
            this.a = a;
            this.b = b;
            this.N = N;
        }

        public void Formula()
        {
            
            h = (b - a) / N;
            
            for (double i = a; i <= b; i += h)
            {
                y = Math.Pow(i, 9) * h;

                ProgressChanged((i + h));
                Thread.Sleep(100);
            }
             

            _checkComplite = true;
            WorkComlite(_checkComplite, y);
        }
  
    }
   

    public partial class MainWindow : Window
    {
        private Work _worker;
        private static int N;
        private static double a, b;

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                a = Convert.ToDouble(aBox.Text.ToString());
                b = Convert.ToDouble(bBox.Text.ToString());

                var dialog = new DialogWindow();
                dialog.ShowDialog();
                N = dialog.Value;

                _worker = new Work(a, b, N);
                
                _worker.ProgressChanged += Progress;
                _worker.WorkComlite += Complite;
                

                Thread thread = new Thread(_worker.Formula);
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.Normal;
                thread.Start();

                Start.IsEnabled = false; // Block button
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Complite(bool comlite, double result) {
            Action act = () => 
            {
                Start.IsEnabled = comlite;
                Result.Text = Convert.ToString(result);
            };
            

            Start.Dispatcher.BeginInvoke(act);
        }

        

        private void Progress(double progress)
        {
            
            Action act = () =>
            {
               
                ProgressBar.Value = progress;
                ProgressBar.Minimum = a;
                ProgressBar.Maximum = b;
            };
            ProgressBar.Dispatcher.BeginInvoke(act);
        }



        public MainWindow()
        {
            InitializeComponent();

        }

    }
}
