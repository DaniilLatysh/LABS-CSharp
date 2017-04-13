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
using System.Windows.Shapes;
using System.Data;
using System.Data.Entity;

namespace WPF_Lab9
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private AllResullt resultIn = new AllResullt();

        private static ADOModel adoDB = new ADOModel();

        public EditWindow(AllResullt resultIn)
        {
            this.resultIn = resultIn;
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

            Manufacturer.Text = resultIn.Manufacturer;
            Model.Text = resultIn.Model;
            Count.Text = resultIn.Count.ToString();
            Price.Text = resultIn.Price.ToString();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Category sel = CategoryGrid.SelectedItem as Category;

                if (Manufacturer.Text != "Manufacturer" && Manufacturer.Text != null &&
                    Model.Text != "Model" && Model.Text != null &&
                    Count.Text != "Count" && Count.Text != null &&
                    Price.Text != "Price" && Price.Text != null)
                {

                    string manufacturer, model, nameCategory;
                    int count, price, idPart;
                    manufacturer = Manufacturer.Text;
                    model = Model.Text;
                    count = int.Parse(Count.Text);
                    price = int.Parse(Price.Text);
                    idPart = resultIn.IdPart;
                    nameCategory = sel.NameCategory;


                    await Task.Factory.StartNew(()=> EditBuilder(idPart, nameCategory, manufacturer, model, count, price));
                    Connect();
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

        private void EditBuilder(int idPart, string nameCategory, string manufdcturer, string model, int count, int price)
        {
            WH part = adoDB.WHs.Where(p => p.ID_Pard == idPart).First();
           

            part.Manufacturer = manufdcturer;
            part.Model = model;
            part.Count = count;
            part.Price = price;
            part.Categories.Add(adoDB.Categories.Where(c => c.NameCategory == nameCategory).First());
            adoDB.Entry(part).State = EntityState.Modified;
            Dispatcher.Invoke(()=> adoDB.SaveChanges());
            

        }

      
    }
}
