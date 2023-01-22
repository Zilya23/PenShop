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
using System.Collections.ObjectModel;

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public static ObservableCollection<DateBasee.User> users { get; set; }
        public RegistrationPage()
        {
            InitializeComponent();

            cbType.ItemsSource = bd_connection.connection.TypeClient.ToList();
            cbType.DisplayMemberPath = "Name";
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if(CorrectPass())
            {
                var new_user = new DateBasee.User();
                var new_client = new DateBasee.Client();

                new_user.Login = tb_login.Text;
                new_user.Password = tb_password.Text;
                bd_connection.connection.User.Add(new_user);
                bd_connection.connection.SaveChanges();

                users = new ObservableCollection<DateBasee.User>(bd_connection.connection.User.ToList());

                new_client.FIO = tb_FIO.Text.Trim();

                new_client.NumberPhone = tb_phone.Text;
                new_client.Email = tb_email.Text;
                new_client.AddDate = DateTime.Now;
                new_client.UserId = users.Last().Id;
                new_client.TypeClient.Id = (cbType.SelectedItem as DateBasee.TypeClient).Id;

                bd_connection.connection.Client.Add(new_client);
                bd_connection.connection.SaveChanges();

                MessageBox.Show("All OK");
                NavigationService.GoBack();
            }
        }

        public bool CorrectPass()
        {
            users = new ObservableCollection<DateBasee.User>(bd_connection.connection.User.ToList());
            bool login_unic = true;
            foreach (var i in users)
            {
                if(i.Login == tb_login.Text)
                {
                    login_unic = false;
                }
            }

            if (login_unic)
            {
                string pas = tb_password.Text;
                bool buk = false;
                bool cif = false;
                bool spec = false;
                foreach (var i in pas)
                {
                    if (Char.IsLetter(i))
                    {
                        buk = true;
                    }
                }

                foreach (var i in pas)
                {
                    if (Char.IsNumber(i))
                    {
                        cif = true;
                    }
                }

                foreach (var i in pas)
                {
                    if (i == '!' || i == '@' || i == '#' || i == '$' || i == '%' || i == '^')
                    {
                        spec = true;
                    }
                }

                if (pas.Length > 6 && buk && cif && spec)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать буквы, цифры и спец.символы");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Такой логин уже существует, придумайте новый");
                return false;
            }
        }

        private void tb_phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
