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
    public partial class HemsireKayit : Window
    {
        public MainWindow mainWindow;

        HemsireProvider hp = new HemsireProvider();
        SqlBaglanti baglanti = new SqlBaglanti();
        DataTable dt = new DataTable();
        string secili = "";
        public static string onaySifre;

        public HemsireKayit()
        {
            InitializeComponent();

            dataGrid1.SelectionMode = DataGridSelectionMode.Extended;

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
                if (txtSifre.Password.Length < 4)
                {
                    MessageBox.Show("Şifreniz 4 haneden büyük olmalı.");
                }
                else
                {
                    Hemsire yeni = new Hemsire();
                    yeni.Hemsire_TC = txtTC.Text.ToString();
                    yeni.Hemsire_Ad = txtAd.Text.ToString();
                    yeni.Hemsire_Soyad = txtSoyad.Text.ToString();
                    yeni.Hemsire_Sifre = txtSifre.Password.ToString();
                    hp.Ekle(yeni);
                    Listele();
                }
            }
        }

        void dSet()
        {
            DataColumn d1 = new DataColumn("TC NO", typeof(string));
            DataColumn d2 = new DataColumn("AD", typeof(string));
            DataColumn d3 = new DataColumn("SOYAD", typeof(string));
            dt.Columns.Add(d1);
            dt.Columns.Add(d2);
            dt.Columns.Add(d3);
        }

        void Listele()
        {
            dt.Columns.Clear();

            dSet();

            dt.Rows.Clear();

            dataGrid1.ItemsSource = null;
            dataGrid1.Items.Refresh();

            hp.listele();

            for (int i = 0; i < hp.isimler.Count; i++)
            {
                dt.Rows.Add(hp.tcler[i].ToString(), hp.isimler[i].ToString(), hp.soyisimler[i].ToString());
            }
            dataGrid1.ItemsSource = dt.DefaultView;
            foreach (var column in dataGrid1.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//insert
        {
            if (txtSifre.Password.Length < 4)
            {
                MessageBox.Show("Şifreniz 4 haneden büyük olmalı.");
            }
            else
            {
                Hemsire yeni = new Hemsire();
                yeni.Hemsire_TC = txtTC.Text.ToString();
                yeni.Hemsire_Ad = txtAd.Text.ToString();
                yeni.Hemsire_Soyad = txtSoyad.Text.ToString();
                yeni.Hemsire_Sifre = txtSifre.Password.ToString();
                hp.Ekle(yeni);
                Listele();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//update
        {
            if (txtSifre.Password.Length < 4)
            {
                MessageBox.Show("Şifreniz 4 haneden büyük olmalı.");
            }
            else
            {
                DataGrid dg = dataGrid1;
                DataRowView seciliRow = dg.SelectedItem as DataRowView;
                if (seciliRow != null)
                {
                    var ac = new OnayEkrani();
                    ac.ShowDialog();

                    baglanti.con.Open();
                    SqlCommand cmdSira = new SqlCommand("Select hemsire_Sifre from Hemsire where hemsire_TC = '" + seciliRow["TC NO"].ToString() + "'", baglanti.con);
                    SqlDataReader reader = cmdSira.ExecuteReader();
                    if (reader.Read())
                    {
                        secili = reader["hemsire_Sifre"].ToString();
                    }
                    baglanti.con.Close();

                    if (onaySifre == secili.ToString())
                    {
                        Hemsire tc = new Hemsire();
                        tc.Hemsire_TC = seciliRow["TC NO"].ToString();
                        Hemsire yenikisi = new Hemsire();
                        yenikisi.Hemsire_TC = txtTC.Text.ToString();
                        yenikisi.Hemsire_Ad = txtAd.Text.ToString();
                        yenikisi.Hemsire_Soyad = txtSoyad.Text.ToString();
                        yenikisi.Hemsire_Sifre = txtSifre.Password.ToString();
                        hp.Guncelle(tc, yenikisi);
                        Listele();
                        MessageBox.Show("İşlem başarıyla gerçekleştirildi.");
                    }
                    else
                        MessageBox.Show("İşlemi onaylarken şifrenizi eksik veya hatalı girdiniz.");
                }
                else
                {
                    MessageBox.Show("Önce güncellenecek kaydı seçmeniz gerekiyor.");
                }
            }
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Listele();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = dataGrid1;
            DataRowView seciliRow = dg.SelectedItem as DataRowView;
            if (seciliRow != null)
            {
                txtTC.Text = seciliRow["TC NO"].ToString();
                txtAd.Text = seciliRow["AD"].ToString();
                txtSoyad.Text = seciliRow["SOYAD"].ToString();

                baglanti.con.Open();
                SqlCommand cmdSira = new SqlCommand("Select hemsire_Sifre from Hemsire where hemsire_TC = '" + seciliRow["TC NO"].ToString() + "'", baglanti.con);
                SqlDataReader reader = cmdSira.ExecuteReader();
                if (reader.Read())
                {
                    txtSifre.Password = reader["hemsire_Sifre"].ToString();
                }
                baglanti.con.Close();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//delete
        {
            DataGrid dg = dataGrid1;
            DataRowView seciliRow = dg.SelectedItem as DataRowView;
            if (seciliRow != null)
            {
                var a = new OnayEkrani();
                a.ShowDialog();

                baglanti.con.Open();
                SqlCommand cmdSira = new SqlCommand("Select hemsire_Sifre from Hemsire where hemsire_TC = '" + seciliRow["TC NO"].ToString() + "'", baglanti.con);
                SqlDataReader reader = cmdSira.ExecuteReader();
                if (reader.Read())
                {
                    secili = reader["hemsire_Sifre"].ToString();
                }
                baglanti.con.Close();

                if (onaySifre == secili.ToString())
                {
                    Hemsire silinecekkisi = new Hemsire();
                    silinecekkisi.Hemsire_TC = seciliRow["TC NO"].ToString();
                    hp.Sil(silinecekkisi);
                    Listele();
                    MessageBox.Show("İşlem başarıyla gerçekleştirildi.");
                }
                else
                    MessageBox.Show("İşlemi onaylarken şifrenizi eksik veya hatalı girdiniz.");
            }
            else
                MessageBox.Show("Önce silinecek kaydı seçmeniz gerekiyor.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
