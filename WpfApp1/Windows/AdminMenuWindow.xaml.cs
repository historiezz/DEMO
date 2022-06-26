using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfApp1.Models;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminMenuWindow.xaml
    /// </summary>
    public partial class AdminMenuWindow : Window
    {
        private Employees _sotrudnik;
        private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Reports");
        private readonly string TemplateFileName = path + @"\shablon.docx";
        Random random = new Random();
        int s = 0, m = 0, ch = 0;
        public AdminMenuWindow(Employees sotrudnik)
        {
            InitializeComponent();
            lblTime.Content = " 0 : 0 : 0 ";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            this._sotrudnik = sotrudnik;

            dgEnterHistory.ItemsSource = bdEntities.GetContext().Employees.ToList();
            using (var db = new bdEntities())
            {
                var user = bdEntities.GetContext().Employees.ToArray();
                foreach (Employees emp in user)
                {
                    CbVariableSort.Items.Add(emp.Name + " " + emp.Surname + " " + emp.Patronymic);
                }

                var service = bdEntities.GetContext().Service.ToArray();
                foreach (Service emp in service)
                {
                    CbChooseService.Items.Add(emp.Name);
                }
            }
        }
        public AdminMenuWindow()
        {
            InitializeComponent();
            dgEnterHistory.ItemsSource = bdEntities.GetContext().Employees.ToList();
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
        private void BnReport_Click(object sender, RoutedEventArgs e)
        {
            SpWord.Visibility = Visibility.Visible;
            TbPath.Visibility = Visibility.Visible;
            ExportBTN.Visibility = Visibility.Visible;
            dgEnterHistory.Visibility = Visibility.Hidden;
            SpChooseEmpl.Visibility = Visibility.Hidden;
        }

        private void BnEnterHistory_Click(object sender, RoutedEventArgs e)
        {
            dgEnterHistory.Visibility = Visibility.Visible;
            SpChooseEmpl.Visibility = Visibility.Visible;
            SpWord.Visibility = Visibility.Hidden;
            TbPath.Visibility = Visibility.Hidden;
            ExportBTN.Visibility = Visibility.Hidden;
        }
        public void RefrehsDatagrid()
        {
            using (var db = new bdEntities())
            {
                var currentName = CbVariableSort.SelectedValue.ToString().Split(' ');
                var nameemp = currentName[0];
                var surnameEmp = currentName[1];
                var currentemp = db.Employees.Where(p => p.Name == nameemp && p.Surname == surnameEmp).ToList();
                dgEnterHistory.ItemsSource = currentemp;
            }
        }
        private void CbVariableSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefrehsDatagrid();
        }

        private void ExportBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new bdEntities())
            {
                var values = db.Order.SelectMany(z => z.Orders_Services,
               (z, y) => new { z, y }).Select(zy => new
               {
                   Time = zy.z.Time,
                   Name = zy.y.Service.Name
               }).Where(p => p.Name == CbChooseService.SelectedValue.ToString()).OrderBy(p => p.Time);

                string time = "";
                var duration = 10;
                var i = 0;
                int[] array = { 10, 30, 40, 60 };

                foreach (var val in values)
                {
                    i = random.Next(0, 3);
                    duration = array[i];
                    time += val.Time + " | " + duration + "^l";
                }
                var WordApp = new Word.Application();
                WordApp.Visible = false;
                var WordDocument = WordApp.Documents.Open(TemplateFileName);
                ReplaceWord("{name}", time, WordDocument);
                WordDocument.SaveAs2(path + @"\result1.docx");
                TbPath.Text = "Файл сохранён в " + path + @"\result1.docx";
            }
        }
        private void ReplaceWord(string stubToReplace, string text, Word.Document wordDoc)
        {
            var range = wordDoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }
        private void BnExpendableMaterials_Click(object sender, RoutedEventArgs e)
        {
            ServiceEditWindow editWindow = new ServiceEditWindow();
            editWindow.Show();
            this.Hide();
        }

        private void BnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
