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

namespace Siramatik
{
    
    public partial class OnayEkrani : Window
    {
        public MainWindow mainWindow;

        public OnayEkrani()
        {
            InitializeComponent();

            DoktorKayit.onaySifresi = "";
            HemsireKayit.onaySifre = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtSifre.Text.Length < 4)
            {
                MessageBox.Show("Şifreniz 4 haneden büyük olmalı.");
            }
            else
            {
                DoktorKayit.onaySifresi = txtSifre.Text.ToString();
                HemsireKayit.onaySifre = txtSifre.Text.ToString();
                this.Hide();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                System.Diagnostics.Process.Start("~/Helper/helperr.chm");

            }
        }
    }
}
