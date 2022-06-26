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
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            dgServiceEdit.ItemsSource = bdEntities.GetContext().Service.ToList();
        }
        public void UpdateDg()
        {
            dgServiceEdit.ItemsSource = null;
            dgServiceEdit.Items.Clear();
            using (var db = new bdEntities())
            {
                dgServiceEdit.ItemsSource = db.Service.ToList();
            }

        }
        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            AddServicePage a = new AddServicePage(null);
            Windows.ServiceEditWindow.main.AdminFrame.Navigate(a);
        }

        private void BtnForDeleteService_Click(object sender, RoutedEventArgs e)
        {
            using (bdEntities db = new bdEntities())
            {
                Service s = (sender as Button).DataContext as Service;
                var comp = db.Service.FirstOrDefault(p => p.ID == s.ID);
                db.Service.Remove(comp);
                db.SaveChanges();
                UpdateDg();
                MessageBox.Show("Данные удалены");
            }
        }

        private void BtnForEditService_Click(object sender, RoutedEventArgs e)
        {
            AddServicePage a = new AddServicePage(this, (sender as Button).DataContext as Service);
            Windows.ServiceEditWindow.main.AdminFrame.Navigate(a);
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.AdminMenuWindow a = new Windows.AdminMenuWindow();
            a.Show();
            Windows.ServiceEditWindow.main.Hide();
        }
    }
}
