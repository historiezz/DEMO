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
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        private Service _e;
        public ServicePage _cs = new ServicePage();
        public AddServicePage(ServicePage _c, Service e = null)
        {
            InitializeComponent();
            _e = e;
            _cs = _c;
            if (e != null)
            {
                NameTB.Text = _e.Name;
                PriceTB.Text = Convert.ToString(_e.Price);
                CodeTB.Text = _e.Code;
                AddBtn.Content = "Изменить";
            }
            else
                AddBtn.Content = "Добавить";
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new bdEntities())
            {
                if (!String.IsNullOrEmpty(NameTB.Text) && int.TryParse(PriceTB.Text, out int number) && !String.IsNullOrEmpty(CodeTB.Text))
                {
                    if (_e != null)
                    {
                        var service = db.Service.FirstOrDefault(p => p.ID == _e.ID);
                        service.Price = number;
                        service.Name = NameTB.Text;
                        service.Code = CodeTB.Text;
                        db.SaveChanges();
                        MessageBox.Show("Данные изменены");
                        Windows.ServiceEditWindow.main.AdminFrame.Navigate(new ServicePage());
                    }
                    else
                    {
                        Service s = new Service()
                        { Name = NameTB.Text, Price = number, Code = CodeTB.Text };
                        db.Service.Add(s);
                        db.SaveChanges();
                        MessageBox.Show("Данные добавлены");
                        Windows.ServiceEditWindow.main.AdminFrame.Navigate(new ServicePage());
                    }
                }
                else
                {
                    MessageBox.Show("Корректно введите все поля");
                }

            }
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.AdminMenuWindow a = new Windows.AdminMenuWindow();
            a.Show();
            Windows.ServiceEditWindow.main.Hide();
        }
    }
}
