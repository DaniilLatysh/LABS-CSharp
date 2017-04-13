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
using System.Data;
using System.Data.Entity;
using System.Xml.Linq;

namespace WPF_Lab9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public class AllResullt
    {
        public int IdPart { get; set; }
        public string CategoryName { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }

    }

    public partial class MainWindow : Window
    {
        private static ADOModel adoDB;
        private AllResullt selectItem;

        public MainWindow()
        {
            InitializeComponent();
            Connect();
        }

        private void Connect()
        {
            MainGrid.Items.Clear();
            adoDB = new ADOModel();
            IEnumerable<WH> allLine = adoDB.WHs.Include(p => p.Categories);

            foreach (var a in allLine)
            {
                List<string> str = a.Categories.Select(c => c.NameCategory).ToList();

                foreach (string s in str)
                {
                    AllResullt res = new AllResullt();
                    res.IdPart = a.ID_Pard;
                    res.Manufacturer = a.Manufacturer;
                    res.Model = a.Model;
                    res.Count = a.Count;
                    res.Price = a.Price;
                    res.CategoryName = s;
                    MainGrid.Items.Add(res);
                }
            }

        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Items.Clear();
            Connect();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddPart addPart = new AddPart();
            addPart.Show();

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        protected void Delete()
        {
            try
            {
                selectItem = MainGrid.SelectedItem as AllResullt;

                if (selectItem != null && selectItem.CategoryName != null)
                {
                    WH delPart = adoDB.WHs.Where(o => o.Model == selectItem.Model).First();
                    Category delcat = adoDB.Categories.Where(c => c.NameCategory == selectItem.CategoryName).First();
                    delcat.WHs.Remove(delPart);
                    adoDB.Entry(delcat).State = EntityState.Modified;
                    adoDB.WHs.Remove(delPart);
                    adoDB.SaveChanges();
                    Connect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Category_Button_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow catWin = new CategoryWindow(MainGrid);
            catWin.Show();
        }

        private void Wharehouse_Button_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Items.Clear();

            adoDB = new ADOModel();
            IEnumerable<WH> allLine = adoDB.WHs;

            foreach (var a in allLine)
            {
                List<string> str = a.Categories.Select(c => c.NameCategory).ToList();
                AllResullt res = new AllResullt();
                res.Manufacturer = a.Manufacturer;
                res.Model = a.Model;
                res.Count = a.Count;
                res.Price = a.Price;
                foreach (string s in str)
                {
                    res.CategoryName = s;
                }
                MainGrid.Items.Add(res);
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            selectItem = MainGrid.SelectedItem as AllResullt;
            if (selectItem != null)
            {

                EditWindow edit = new EditWindow(selectItem);
                edit.Show();

            }
            Connect();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEnumerable<Category> categoryXML = adoDB.Categories.ToList();
                XDocument doc = new XDocument(new XElement("Categories",
                    from a in categoryXML
                    select
                    new XElement("Category",
                    new XElement("NameCategory", a.NameCategory))));

                doc.Save("Category.xml");
                MessageBox.Show("Comlite!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument doc = XDocument.Load("Category.xml");
                foreach (XElement a in doc.Root.Elements())
                {
                    Category cat = new Category();
                    int w = 0;

                    foreach (XElement tr in a.Elements())
                    {
                        if (tr.Name == "NameCategory")
                        {
                            cat.NameCategory = tr.Value;
                        }
                        w = adoDB.Categories.Count(i => i.NameCategory == cat.NameCategory);
                        if (w == 0)
                        {
                            adoDB.Categories.Add(cat);
                        }
                    }
                }
                adoDB.SaveChanges();
                Connect();
                MessageBox.Show("Comlite!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
