using System;
using System.IO;
using Microsoft.Win32;
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

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для RedactionPage.xaml
    /// </summary>
    public partial class RedactionPage : Page
    {
        public static DateBasee.Product constProd;
        public RedactionPage(DateBasee.Product n)
        {
            InitializeComponent();
            constProd = n;
            this.DataContext = constProd;
            tb_id.Text = n.Id.ToString();
            tb_name.Text = n.Name;
            tb_description.Text = n.Description;

            var compList = bd_connection.connection.Company.ToList();
            var typeList = bd_connection.connection.TypePencil.ToList();
            cb_company.ItemsSource = compList;
            cb_company.DisplayMemberPath = "Name";
            cb_company.SelectedItem = compList.Where(c => c.Id == constProd.Company.Id).FirstOrDefault();

            cb_type.ItemsSource = typeList;
            cb_type.DisplayMemberPath = "Name";
            cb_type.SelectedItem = typeList.Where(c => c.Id == constProd.TypePencil.Id).FirstOrDefault();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPage(ListPage.user));
        }

        private void btn_delite_Click(object sender, RoutedEventArgs e)
        {
            DeletedWindow del = new DeletedWindow();

            if(del.ShowDialog() == true)
            {
                constProd.Deleted = true;
                bd_connection.connection.SaveChanges();
            }
            NavigationService.Navigate(new ListPage(ListPage.user));
        }

        private void tb_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0) && e.Text != "-")
            {
                e.Handled = true;
            }
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

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            constProd.Name = tb_name.Text;
            constProd.Description = tb_description.Text;
            constProd.AddDate = DateTime.Now;
            bd_connection.connection.SaveChanges();
            NavigationService.Navigate(new ListPage(ListPage.user));
        }
    }
}
