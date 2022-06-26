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
using WpfApp1.Models;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServiceEditWindow.xaml
    /// </summary>
    public partial class ServiceEditWindow : Window
    {
        public static ServiceEditWindow main = new ServiceEditWindow();
        public ServiceEditWindow()
        {
            InitializeComponent();
            main = this;
            AdminFrame.Navigate(new Pages.ServicePage());
        }
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            Pages.AddServicePage a = new Pages.AddServicePage(null, (sender as Button).DataContext as Service);

            main.AdminFrame.Navigate(a);
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuWindow a = new AdminMenuWindow();
            a.Show();
            this.Hide();
        }
    }
}
