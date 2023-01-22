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

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        public ShowWindow()
        {
            InitializeComponent();
            frame_auto_reg.NavigationService.Navigate(new AutorizationPage());
        }
    }
}
