using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1.Models;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для SalesmanMenuWindow.xaml
    /// </summary>
    public partial class SalesmanMenuWindow : Window
    {
        private Employees _sotrudnik;
        private Client selecteClient = new Client();
        private Service selectedService = new Service();
        int s = 0, m = 0, ch = 0;
        public SalesmanMenuWindow()
        {
            InitializeComponent();
            Grid.SetRow(panel, 2);
            Grid.SetColumn(panel, 0);
            Grid.SetColumnSpan(panel, 3);
        }
        public SalesmanMenuWindow(Employees sotrudnik)
        {
            InitializeComponent();
            lblTime.Content = " 0 : 0 : 0 ";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            this._sotrudnik = sotrudnik;

            var clients = bdEntities.GetContext().Client.ToList();
            var services = bdEntities.GetContext().Service.ToList();
            LVServices.ItemsSource = services;
            LVClients.ItemsSource = clients;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            s += 1;
            if (s == 60)
            {
                s = 0;
                m += 1;
            }
            if (m == 60)
            {
                m = 0;
                ch += 1;
            }
            lblTime.Content = ch.ToString() + " : " + m.ToString() + " : " + s.ToString();
            if (m == 5 && s == 0)
            {
                MessageBox.Show("Осталось пять минут до завершения работы!");
                return;
            }
            if (m == 10)
            {
                MessageBox.Show("Время работы закончено!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        double shirina = 2.5;
        public void Strih(string c)
        {
            AddR(c[0], 7, false);
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.Black, VerticalAlignment = VerticalAlignment.Top });
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.White, VerticalAlignment = VerticalAlignment.Top });
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.Black, VerticalAlignment = VerticalAlignment.Top });
            switch (c[0])
            {
                case '0': { StrihA(c[1]); StrihA(c[2]); StrihA(c[3]); StrihA(c[4]); StrihA(c[5]); StrihA(c[6]); break; }
                case '1': { StrihA(c[1]); StrihA(c[2]); StrihB(c[3]); StrihA(c[4]); StrihB(c[5]); StrihB(c[6]); break; }
                case '2': { StrihA(c[1]); StrihA(c[2]); StrihB(c[3]); StrihB(c[4]); StrihA(c[5]); StrihB(c[6]); break; }
                case '3': { StrihA(c[1]); StrihA(c[2]); StrihB(c[3]); StrihB(c[4]); StrihB(c[5]); StrihA(c[6]); break; }
                case '4': { StrihA(c[1]); StrihB(c[2]); StrihA(c[3]); StrihA(c[4]); StrihB(c[5]); StrihB(c[6]); break; }
                case '5': { StrihA(c[1]); StrihB(c[2]); StrihB(c[3]); StrihA(c[4]); StrihA(c[5]); StrihB(c[6]); break; }
                case '6': { StrihA(c[1]); StrihB(c[2]); StrihB(c[3]); StrihB(c[4]); StrihA(c[5]); StrihA(c[6]); break; }
                case '7': { StrihA(c[1]); StrihB(c[2]); StrihA(c[3]); StrihB(c[4]); StrihA(c[5]); StrihB(c[6]); break; }
                case '8': { StrihA(c[1]); StrihB(c[2]); StrihA(c[3]); StrihB(c[4]); StrihB(c[5]); StrihA(c[6]); break; }
                case '9': { StrihA(c[1]); StrihB(c[2]); StrihB(c[3]); StrihA(c[4]); StrihB(c[5]); StrihA(c[6]); break; }
            }
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.White, VerticalAlignment = VerticalAlignment.Top }); 
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.Black, VerticalAlignment = VerticalAlignment.Top });
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.White, VerticalAlignment = VerticalAlignment.Top });
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.Black, VerticalAlignment = VerticalAlignment.Top });
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.White, VerticalAlignment = VerticalAlignment.Top });
            StrihC(c[7]);
            StrihC(c[8]);
            StrihC(c[9]);
            StrihC(c[10]);
            StrihC(c[11]);
            StrihC(c[12]);
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.Black, VerticalAlignment = VerticalAlignment.Top });
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.White, VerticalAlignment = VerticalAlignment.Top });
            panel.Children.Add(new Rectangle() { Height = 98, Width = shirina * 1, Fill = Brushes.Black, VerticalAlignment = VerticalAlignment.Top });
        }
        public void StrihA(char c)
        {
            switch (c) {
                case '0': { AddRect(c, 3, 2, 1, 1); break; }
                case '1': { AddRect(c, 2, 2, 2, 1); break; }
                case '2': { AddRect(c, 2, 1, 2, 2); break; }
                case '3': { AddRect(c, 1, 4, 1, 1); break; }
                case '4': { AddRect(c, 1, 1, 3, 2); break; }
                case '5': { AddRect(c, 1, 2, 3, 1); break; }
                case '6': { AddRect(c, 1, 1, 1, 4); break; }
                case '7': { AddRect(c, 1, 3, 1, 2); break; }
                case '8': { AddRect(c, 1, 2, 1, 3); break; }
                case '9': { AddRect(c, 3, 1, 1, 2); break; }
            }
        }

        public void StrihB(char c)
        {
            switch (c) {
                case '0': { AddRect(c, 1, 1,2,3); break; }
                case '1': { AddRect(c, 1, 2,2,2); break; }
                case '2': { AddRect(c, 2, 2,1,2); break; }
                case '3': { AddRect(c, 1, 1,4,1); break; }
                case '4': { AddRect(c, 2, 3,1,1); break; }
                case '5': { AddRect(c, 1, 3,2,1); break; }
                case '6': { AddRect(c, 4, 1,1,1); break; }
                case '7': { AddRect(c, 2, 1,3,1); break; }
                case '8': { AddRect(c, 3, 1,2,1); break; }
                case '9': { AddRect(c, 2, 1,1,3); break; }
            }
        }
        public void StrihC(char c)
        {
            switch (c) {
                case '0': { AddRectC(c, 1, 1, 2, 3); break; }
                case '1': { AddRectC(c, 1, 2, 2, 2); break; }
                case '2': { AddRectC(c, 2, 2, 1, 2); break; }
                case '3': { AddRectC(c, 1, 1, 4, 1); break; }
                case '4': { AddRectC(c, 2, 3, 1, 1); break; }
                case '5': { AddRectC(c, 1, 3, 2, 1); break; }
                case '6': { AddRectC(c, 4, 1, 1, 1); break; }
                case '7': { AddRectC(c, 2, 1, 3, 1); break; }
                case '8': { AddRectC(c, 3, 1, 2, 1); break; }
                case '9': { AddRectC(c, 2, 1, 1, 3); break; }
            }
        }
        public void AddRect(char c, int P1, int SH1, int P2, int SH2)
        {
            StackPanel s = new StackPanel();
            StackPanel s2 = new StackPanel() { Orientation = Orientation.Horizontal };
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * P1, Fill = Brushes.White });
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * SH1, Fill = Brushes.Black });
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * P2, Fill = Brushes.White });
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * SH2, Fill = Brushes.Black });
            s.Children.Add(s2);
            s.Children.Add(new Label() { Content = c, FontSize = 15 });
            panel.Children.Add(s);
        }
        public void AddRectC(char c, int P1, int SH1, int P2, int SH2)
        {
            StackPanel s = new StackPanel();
            StackPanel s2 = new StackPanel() { Orientation = Orientation.Horizontal };
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * SH2, Fill = Brushes.Black });
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * P2, Fill = Brushes.White });
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * SH1, Fill = Brushes.Black });
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * P1, Fill = Brushes.White });
            s.Children.Add(s2);
            s.Children.Add(new Label() { Content = c, FontSize = 15 });
            panel.Children.Add(s);
        }
        public void AddR(char c, int sh, bool IsBlack)
        {
            StackPanel s = new StackPanel();
            StackPanel s2 = new StackPanel() { Orientation = Orientation.Horizontal };
            s2.Children.Add(new Rectangle() { Height = 91.4, Width = shirina * sh, Fill = IsBlack? Brushes.Black : Brushes.White });
            s.Children.Add(s2);
            s.Children.Add(new Label() { Content = c, FontSize = 15 });
            panel.Children.Add(s);

        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            tbText.Visibility = Visibility.Visible;
            tbOrderNumb.Visibility = Visibility.Visible;
            var orders = bdEntities.GetContext().Order.ToList();
            using (var db = new bdEntities())
            {
                foreach (var order in db.Order)
                {
                    int last = (from m in db.Order select m.ID).ToList().Last();
                    tbOrderNumb.Text = (last + 1).ToString();
                }
            }
        }

        private void BtnChooseClient_Click(object sender, RoutedEventArgs e)
        {
            tbText.Visibility = Visibility.Hidden;
            tbOrderNumb.Visibility = Visibility.Hidden;
            BtnPrintStrih.Visibility = Visibility.Hidden;
            panel.Visibility = Visibility.Hidden;
            SpSearch.Visibility = Visibility.Visible;
            SpClient.Visibility = Visibility.Visible;
            SpService.Visibility = Visibility.Hidden;
            SpNewClient.Visibility = Visibility.Hidden;
            textTB.Text = "Поиск клиента";
        }

        private void BtnChooseService_Click(object sender, RoutedEventArgs e)
        {
            tbText.Visibility = Visibility.Hidden;
            tbOrderNumb.Visibility = Visibility.Hidden;
            BtnPrintStrih.Visibility = Visibility.Hidden;
            panel.Visibility = Visibility.Hidden;
            SpSearch.Visibility = Visibility.Visible;
            SpClient.Visibility = Visibility.Hidden;
            SpService.Visibility = Visibility.Visible;
            SpNewClient.Visibility = Visibility.Hidden;
            textTB.Text = "Поиск услуги";
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SpClient.Visibility == Visibility.Visible)
            {
                var clients = bdEntities.GetContext().Client.ToList();
                using (var db = new bdEntities())
                {
                    foreach (var client in db.Client)
                    {
                        if (TbSearch.Text.ToLower().Contains(client.Surname.ToLower()))
                            clients = clients.Where(p => p.Surname.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
                        if (TbSearch.Text.ToLower().Contains(client.Name.ToLower()))
                            clients = clients.Where(p => p.Name.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
                        if (TbSearch.Text.ToLower().Contains(client.Patronymic.ToLower()))
                            clients = clients.Where(p => p.Patronymic.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
                    }
                }
                LVClients.ItemsSource = clients;
            }
            if (SpService.Visibility == Visibility.Visible)
            {
                var services = bdEntities.GetContext().Service.ToList();
                services = services.Where(p => p.Name.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
                LVServices.ItemsSource = services;
            }
        }

        private void BtnAddNewClient_Click(object sender, RoutedEventArgs e)
        {
            SpSearch.Visibility = Visibility.Hidden;
            SpClient.Visibility = Visibility.Hidden;
            SpNewClient.Visibility = Visibility.Visible;
        }

        private void BtnToAddNewClient_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new bdEntities())
            {
                string fio = TBClientFIO.Text;
                string[] words = fio.Split(' ');
                string surname = words[0];
                string name = words[1];
                string patronymic = words[2];
                string passport = TBClientPassport.Text;
                string[] word = passport.Split(' ');
                string seria = word[0];
                string number = word[1];
                string date = TBClientBirthdate.Text;
                string[] dates = date.Split('.');
                int year = int.Parse(dates[0]);
                int month = int.Parse(dates[1]);
                int days = int.Parse(dates[2]);
                if (surname != "" && TBClientCode.Text != "")
                {
                    Client client = new Client();
                    client.Code = TBClientCode.Text;
                    client.Surname = surname;
                    client.Name = name;
                    client.Patronymic = patronymic;
                    client.Passport_Seria = seria;
                    client.Passport_Number = number;
                    DateTime dateTime = new DateTime(year, month, days);
                    client.Birtdate = dateTime;
                    client.Address = TBClientAddress.Text;
                    client.Email = TBClientEmail.Text;
                    client.Password = TBClientPassword.Text;
                    db.Client.Add(client);
                    db.SaveChanges();
                    MessageBox.Show(String.Format("Клиент {0} успешно добавлен", client.Surname));
                }
                else
                    MessageBox.Show("Пожалуйста заполните все поля!");
                SpNewClient.Visibility = Visibility.Hidden;
                SpSearch.Visibility = Visibility.Visible;
                SpClient.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new bdEntities())
            {
                var curOrder = new Order();
                foreach (var order in db.Order)
                {
                    if (order.ID == int.Parse(tbOrderNumb.Text))
                        curOrder = order;
                }
                curOrder = db.Order.Find(curOrder.ID);
                curOrder.Client_Code = selecteClient.Code;
                curOrder.Status = "Новая";
                db.SaveChanges();
                MessageBox.Show("Клиент успешно добавлен!");
            }
        }

        private void LVClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clients = bdEntities.GetContext().Client.ToList();
            var selectedclient= clients;
            selectedclient = selectedclient.Where(p => p.Equals(LVClients.SelectedItem)).ToList();
            using (var db = new bdEntities())
            {
                foreach (var item in db.Client)
                {
                    if (selectedclient.Any(p => p.Code.Contains(item.Code)))
                        selecteClient = item;
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var services = bdEntities.GetContext().Service.ToList();
            var selectedservice = services;
            selectedservice = selectedservice.Where(p => p.Equals(LVServices.SelectedItem)).ToList();
            using (var db = new bdEntities())
            {
                foreach (var item in db.Service)
                {
                    if (selectedservice.Any(p => p.ID.Equals(item.ID)))
                        selectedService = item;
                }
            }

            using (var db = new bdEntities())
            {
                var curOrder = new Order();
                foreach (var order in db.Order)
                {
                    if (order.ID == int.Parse(tbOrderNumb.Text))
                        curOrder = order;
                }
                Orders_Services ordserv = new Orders_Services();
                ordserv.ID_Order = curOrder.ID;
                ordserv.ID_Service = selectedService.ID;
                foreach (var ord_ser in db.Orders_Services)
                {
                    if (ordserv.ID_Order == ord_ser.ID_Order && ordserv.ID_Service == ord_ser.ID_Service)
                    {
                        MessageBox.Show("Данная услуга уже добавлена! Пожалуйста выберите другую.");
                        return;
                    }
                }
                db.Orders_Services.Add(ordserv);
                db.SaveChanges();
                MessageBox.Show("Услуга успешно добавлена!");
            }
        }

        private void BtnPrintStrih_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Распечатываем элемент Canvas");
            }
        }


        private void tbOrderNumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                panel.Visibility = Visibility.Visible;
                int ordernumb = int.Parse(tbOrderNumb.Text);
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string year = dateTime.Year.ToString();
                var date = dateTime.Day.ToString() + dateTime.Month.ToString() + year.Substring(2).ToString();
                var time = dateTime.Hour.ToString() + dateTime.Minute.ToString();
                Random rnd = new Random();
                var rent = rnd.Next(2, 9);
                var code = rnd.Next(1, 6);
                var strihkod = ordernumb + date + time + rent + code;
                if (strihkod.Length < 13)
                    strihkod += rnd.Next(1, 6);
                Strih(strihkod);
                BtnPrintStrih.Visibility = Visibility.Visible;
                BtnChooseClient.Visibility = Visibility.Visible;
                BtnChooseService.Visibility = Visibility.Visible;

                Order order = new Order();
                using (var db = new bdEntities())
                {
                    foreach (var ordr in db.Order)
                    {
                        if (ordr.ID == int.Parse(tbOrderNumb.Text))
                            return;
                    }
                    order.ID = int.Parse(tbOrderNumb.Text);
                    order.Creation_Date = dateTime.Day.ToString() + "." + dateTime.Month.ToString() + "." + year.ToString();
                    order.Time = dateTime.Hour.ToString() + ":" + dateTime.Minute.ToString();
                    order.Rental_Time = rent * 60;
                    db.Order.Add(order);
                    db.SaveChanges();
                }
            }
        }
    }
}
