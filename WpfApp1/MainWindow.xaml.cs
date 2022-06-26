using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using WpfApp1.Models;
using WpfApp1.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int _counter = 0;
        public static bool entercaptcha = false;
        public MainWindow()
        {
            InitializeComponent();
            TbLog.Text = "fedorov@namecomp.ru";
            pwdTbPass.Password = "8ntwUp";
        }
        /// <summary>
        /// Скрытие и раскрытие пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BnVisiblePassword_Click(object sender, RoutedEventArgs e)
        {
            if (pwdTbPass.Visibility == Visibility.Visible)
            {
                pwdTbPass.Visibility = Visibility.Hidden;
                TbPass.Text = pwdTbPass.Password;
            }
            else
            {
                pwdTbPass.Visibility = Visibility.Visible;
                pwdTbPass.Password = TbPass.Text;
            }
        }

        private void BnAuth_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new bdEntities())
            {
                foreach (Employees sotrudnik in db.Employees)
                {
                    if (sotrudnik.Login == TbLog.Text && sotrudnik.Password == ((pwdTbPass.Visibility == Visibility.Visible) ? pwdTbPass.Password : TbPass.Text))
                    {
                        _counter = 0;
                        MessageBox.Show(String.Format("Добро пожаловать, {0}!", sotrudnik.Name));
                        sotrudnik.Last_Entry = DateTime.Now;
                        sotrudnik.Entry_Type = "Успешно";
                        if (sotrudnik.Post == "Продавец")
                        {
                            SalesmanMenuWindow salesmanMenuWindow = new SalesmanMenuWindow(sotrudnik);
                            salesmanMenuWindow.Show();
                            this.Visibility = Visibility.Hidden;
                            break;
                        }
                        else if (sotrudnik.Post == "Администратор")
                        {
                            AdminMenuWindow adminMenuWindow = new AdminMenuWindow(sotrudnik);
                            adminMenuWindow.Show();
                            this.Visibility = Visibility.Hidden;
                            break;
                        }
                        else if (sotrudnik.Post == "Старший смены")
                        {
                            SupervisorMenuWindow supervisorMenuWindow = new SupervisorMenuWindow(sotrudnik);
                            supervisorMenuWindow.Show();
                            this.Visibility = Visibility.Hidden;
                            break;
                        }
                    }
                    else if ((sotrudnik.Login != TbLog.Text && sotrudnik.Password == ((pwdTbPass.Visibility == Visibility.Visible) ? pwdTbPass.Password : TbPass.Text)) || sotrudnik.Login == TbLog.Text && sotrudnik.Password != ((pwdTbPass.Visibility == Visibility.Visible) ? pwdTbPass.Password : TbPass.Text))
                    {
                        MessageBox.Show("Логин или пароль введены неверно");
                        sotrudnik.Last_Entry = DateTime.Now;
                        sotrudnik.Entry_Type = "Не успешно";
                        bdEntities.GetContext().SaveChanges();
                        _counter++;
                        break;
                    }
                }
                db.SaveChanges();
                if (_counter == 2)
                {
                    CaptchaWindow captchaWindow = new CaptchaWindow(this);
                    captchaWindow.Show();
                    this.Visibility = Visibility.Hidden;
                }
                if (entercaptcha == true)
                {
                    BnAuth.IsEnabled = false;
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += timer_Tick;
                    timer.Start();
                }
            }
        }

        int s = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            s += 1;
            if (s > 10)
            {
                BnAuth.IsEnabled = true;
            }
        }
    }
}
