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
    /// Логика взаимодействия для DeletedWindow.xaml
    /// </summary>
    public partial class DeletedWindow : Window
    {
        public DeletedWindow()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btn_No_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
