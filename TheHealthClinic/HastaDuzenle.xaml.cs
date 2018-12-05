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
    public partial class HastaDuzenle : Window
    {
        public MainWindow mainWindow;

        public HastaDuzenle()
        {
            InitializeComponent();


            this.PreviewKeyDown += new KeyEventHandler(HandleEsc1);
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

        HastaProvider hp = new HastaProvider();
        SqlBaglanti baglanti = new SqlBaglanti();
        DoktorProvider dp = new DoktorProvider();

        void doktorListele()//combobox içerisine doktorları atama
        {
            cmbDoktor.Items.Clear();
            dp.listele();
            for (int i = 0; i < dp.isimler.Count; i++)
            {
                cmbDoktor.Items.Add(dp.isimler[i] + " " + dp.soyisimler[i]).ToString();
            }
        }

        void Listele()//hasta bilileri sırala
        {
            SqlCommand cmd = new SqlCommand("Select * From Hasta where hasta_Tc='" + HastaKabul.HastaTcTut.ToString() + "'", baglanti.con);
            baglanti.con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtTc.Text = reader["hasta_Tc"].ToString();
                txtAd.Text = reader["hasta_Ad"].ToString();
                txtSoyad.Text = reader["hasta_Soyad"].ToString();
                cmbDoktor.Text = reader["hasta_Doktor"].ToString();
                cmbCinsiyet.Text = reader["hasta_Cinsiyet"].ToString();
                dateDogum.Text = reader["hasta_Dogum"].ToString();
            }
            baglanti.con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//delete
        {
            if (txtTc.Text.ToString() != "")
            {
                Hasta silinecekkisi = new Hasta();
                silinecekkisi.Hasta_TC = txtTc.Text.ToString();
                hp.Sil(silinecekkisi);
                HemsireGiris.hg.Listele();
                this.Close();
                MessageBox.Show("İşlem başarıyla gerçekleştirildi.");
            }
            else
            {
                MessageBox.Show("Silenecek hastanın bilgilerini giriniz.");
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//update
        {
            if (txtTc.Text.ToString() != "")
            {
                Hasta tc = new Hasta();
                tc.Hasta_TC = txtTc.Text;
                Hasta yenikisi = new Hasta();
                yenikisi.Hasta_TC = txtTc.Text.ToString();
                yenikisi.Hasta_Ad = txtAd.Text.ToString();
                yenikisi.Hasta_Soyad = txtSoyad.Text.ToString();
                yenikisi.Hasta_Cinsiyet = cmbCinsiyet.Text.ToString();
                //yenikisi.Hasta_Dogum = Convert.ToDateTime(dateDogum.Text.ToString());
                yenikisi.Hasta_Doktor = cmbDoktor.Text.ToString();
                hp.Guncelle(tc, yenikisi);

                HemsireGiris.hg.Listele();

                this.Close();
                MessageBox.Show("İşlem başarıyla gerçekleştirildi.");
            }
            else
            {
                MessageBox.Show("Güncellenecek hastanın bilgilerini giriniz");
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTc.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbCinsiyet.Text = "";
            cmbDoktor.Text = "";
            dateDogum.Text = "";

            cmbCinsiyet.Items.Add("Erkek");
            cmbCinsiyet.Items.Add("Kadın");

            doktorListele();
            Listele();
            
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
