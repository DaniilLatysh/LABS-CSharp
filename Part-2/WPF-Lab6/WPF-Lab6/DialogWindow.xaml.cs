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

namespace WPF_Lab6
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        private int _Value;

        public int ValueCheck {
            get { return _Value; }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The value must be more 0!");
                }
                _Value = value;
            }
        }
        public int Value
        {
            get { return Convert.ToInt32(nForm.Text.ToString()); }
        }

        private void validationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }
        public DialogWindow()
        {
            InitializeComponent();
        }
        private void Click_Button_Ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Click_Button_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
