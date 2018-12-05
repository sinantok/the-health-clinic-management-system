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
using Siramatik.Classes;

namespace Siramatik
{

    public partial class YeniSifre : Window
    {
        public MainWindow mainWindow;

        public YeniSifre()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.fBilgisi == "Hemsire")
            {
                if (txtSifre.Text.Length < 4)
                {
                    MessageBox.Show("Şifreniz 4 haneden büyük olmalı.");
                }
                else
                {
                    if (txtSifre.Text.ToString() == txtSifreT.Text.ToString())
                    { 
                        HemsireProvider hp = new HemsireProvider();
                        Hemsire tc = new Hemsire();
                        tc.Hemsire_TC = txtTC.Text.ToString();
                        Hemsire yenisifre = new Hemsire();
                        yenisifre.Hemsire_Sifre = txtSifre.Text;
                        hp.sGuncelleme(yenisifre, tc);

                        MessageBox.Show("İşlem başarıyla gerçekleştirildi");
                        this.Close();
                    }
                    else
                        MessageBox.Show("Şifreler birbiriyle uyuşmuyor");
                }
            }
            else if (MainWindow.fBilgisi == "Doktor")
            {
                if (txtSifre.Text.ToString() == txtSifreT.Text.ToString())
                {
                    DoktorProvider hp = new DoktorProvider();

                    Doktor tc = new Doktor();
                    tc.Doktor_TC = txtTC.Text.ToString();
                    Doktor yenisifre = new Doktor();
                    yenisifre.Doktor_Sifre = txtSifre.Text;
                    hp.sGuncelleme(yenisifre, tc);

                    MessageBox.Show("İşlem başarıyla gerçekleştirildi");
                    this.Close();
                }
                else
                    MessageBox.Show("Şifreler birbiriyle uyuşmuyor");
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
