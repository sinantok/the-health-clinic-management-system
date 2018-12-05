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
using System.Data.SqlClient;
using Siramatik.Classes;
using System.Collections;
using System.Data;

namespace Siramatik
{

    public partial class HastaKayit : Window
    {
        public MainWindow mainWindow;
        SqlBaglanti baglanti = new SqlBaglanti();
        HastaProvider hp = new HastaProvider();
        DoktorProvider dp = new DoktorProvider();

        public HastaKayit()
        {
            InitializeComponent();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc1);
            this.PreviewKeyDown += new KeyEventHandler(HandleSave);
        }

        private void HandleEsc1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (MessageBox.Show("Çıkış yapmak istiyor musunuz?", "Çıkış Ekranı", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void HandleSave(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                if (txtTc.Text == "" || txtAd.Text == "" || txtSoyad.Text == "")
                {
                    MessageBox.Show("HATA!\nLütfen blgileri eksiksiz giriniz.");
                }
                else
                {
                    Hasta yeni = new Hasta();
                    yeni.Hasta_TC = txtTc.Text.ToString();
                    yeni.Hasta_Ad = txtAd.Text.ToString();
                    yeni.Hasta_Soyad = txtSoyad.Text.ToString();
                    yeni.Hasta_Dogum = dateDogum.Text.ToString();
                    yeni.Hasta_Cinsiyet = cmbCinsiyet.Text.ToString();
                    yeni.Hasta_Doktor = cmbDoktor.Text.ToString();
                    hp.Ekle(yeni);
                    MessageBox.Show("Kayıt işlemi başarılı.");
                    HemsireGiris.hg.Listele();//hasta kabuldeki listeyi yenilemek
                    this.Close();
                }
            }
        }

       

        void doktorListele()
        {
            cmbDoktor.Items.Clear();
            dp.listele();
            for (int i = 0; i < dp.isimler.Count; i++)
            {
                cmbDoktor.Items.Add(dp.isimler[i] + " " + dp.soyisimler[i]).ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//INSERT
        {
            if (txtTc.Text==""||txtAd.Text==""||txtSoyad.Text=="")
            {
                MessageBox.Show("HATA!\nLütfen bilgileri eksiksiz giriniz.");
            }
            else
            {
                Hasta yeni = new Hasta();
                yeni.Hasta_TC = txtTc.Text.ToString();
                yeni.Hasta_Ad = txtAd.Text.ToString();
                yeni.Hasta_Soyad = txtSoyad.Text.ToString();
                yeni.Hasta_Dogum = dateDogum.Text.ToString();
                yeni.Hasta_Cinsiyet = cmbCinsiyet.Text.ToString();
                yeni.Hasta_Doktor = cmbDoktor.Text.ToString();
                hp.Ekle(yeni);
                MessageBox.Show("Kayıt işlemi başarılı.");
                HemsireGiris.hg.Listele();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            cmbCinsiyet.Items.Add("Erkek");
            cmbCinsiyet.Items.Add("Kadın");
            doktorListele();
        }

        private void Window_Closed(object sender, EventArgs e)
        {


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
