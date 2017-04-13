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
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    /// 
   
    public partial class CategoryWindow : Window
    {
        private static ADOModel adoDB;
        private DataGrid grid;
        public Category selectedCategory;
        public CategoryWindow(DataGrid grid)
        {
            InitializeComponent();
            Connect();
            this.grid = grid;
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

        private void Connect(DataGrid grid)
        {
            try
            {
                adoDB = new ADOModel();
                IEnumerable<Category> categories = adoDB.Categories.Include(c => c.WHs).Where(c => c.NameCategory == selectedCategory.NameCategory).ToList();
                IEnumerable<WH> parts = categories.SelectMany(p => p.WHs).ToList();

                foreach (var a in parts)
                {
                    List<string> str = a.Categories.Select(c => c.NameCategory).ToList();

                    foreach (string s in str)
                    {
                        AllResullt res = new AllResullt();
                        res.Manufacturer = a.Manufacturer;
                        res.Model = a.Model;
                        res.Count = a.Count;
                        res.Price = a.Price;
                        res.CategoryName = s;
                        grid.Items.Add(res);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(" Error: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            grid.Items.Clear();
            selectedCategory = CategoryGrid.SelectedItem as Category;
            Connect(grid);
            this.Close();

        }
    }
}
