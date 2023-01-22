using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public static ObservableCollection<DateBasee.Product> products { get; set; }
        public static DateBasee.Product constProd = new DateBasee.Product();
        public AddPage()
        {
            InitializeComponent();
            var compList = bd_connection.connection.Company.ToList();
            var typeList = bd_connection.connection.TypePencil.ToList();
            cb_company.ItemsSource = compList;
            cb_company.DisplayMemberPath = "Name";

            cb_type.ItemsSource = typeList;
            cb_type.DisplayMemberPath = "Name";
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tb_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            constProd.Name = tb_name.Text;
            constProd.Description = tb_description.Text;
            constProd.AddDate = DateTime.Now;
            constProd.TypeId = (cb_type.SelectedItem as DateBasee.TypePencil).Id;
            constProd.CompanyId = (cb_company.SelectedItem as DateBasee.Company).Id;

            bd_connection.connection.Product.Add(constProd);
            bd_connection.connection.SaveChanges();
            products = new ObservableCollection<DateBasee.Product>(bd_connection.connection.Product.ToList());
            NavigationService.Navigate(new RedactionPage(products.Last()));
        }

        private void btn_newphoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                constProd.Photo = File.ReadAllBytes(openFile.FileName);
                img_prod.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }
    }
}
