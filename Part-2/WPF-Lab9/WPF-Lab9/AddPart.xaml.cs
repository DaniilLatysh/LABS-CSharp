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
using System.Data;
using System.Data.Entity;

namespace WPF_Lab9
{
    /// <summary>
    /// Interaction logic for AddPart.xaml
    /// </summary>
    public partial class AddPart : Window
    {

        private static ADOModel adoDB = new ADOModel();
        public AddPart()
        {
            InitializeComponent();
            Connect();
            
        }

        private void Connect()
        {
            adoDB = new ADOModel();
            IEnumerable<Category> allCategory = adoDB.Categories.Include(p => p.WHs);

            foreach (var a in allCategory)
            {
                Category resCategory = new Category();
                resCategory.CategoryID = a.CategoryID;
                resCategory.NameCategory = a.NameCategory;
                CategoryGrid.Items.Add(resCategory);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Category sel = CategoryGrid.SelectedItem as Category;


                if (Manufacturer.Text != "Manufacturer" && Manufacturer.Text != null &&
                    Model.Text != "Model" && Model.Text != null &&
                    Count.Text != "Count" && Count.Text != null &&
                    Price.Text != "Price" && Price.Text != null)
                {
                    WH part = new WH();
                    
                    part.Manufacturer = Manufacturer.Text;
                    part.Model = Model.Text;
                    part.Count = int.Parse(Count.Text);
                    part.Price = int.Parse(Price.Text);
                    part.Categories.Add(adoDB.Categories.Where(c => c.NameCategory == sel.NameCategory).First());

                    adoDB.WHs.Add(part);
                    adoDB.SaveChanges();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter correct value");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
    }
}
