using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        private MainWindow window;
        public CaptchaWindow(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            GenerateCaptcha();
        }
        public void GenerateCaptcha()
        {
            TbCaptcha.IsReadOnly = true;
            String allowchar = " ";
            String pwd = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            string temp = " ";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            TbCaptcha.Text = pwd;
        }
        private void BnGenerateCaptcha_Click(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }

        private void BnSend_Click(object sender, RoutedEventArgs e)
        {
            if(TbTrueCaptcha.Text == TbCaptcha.Text)
            {
                MainWindow._counter = 0;
                MainWindow.entercaptcha = true;
                this.window.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Капча введена неверно. Повторите попытку.");
                GenerateCaptcha();
            }
        }
    }
}
