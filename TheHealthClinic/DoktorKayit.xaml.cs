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
using System.Collections.ObjectModel;

namespace Siramatik
{
    public partial class DoktorKayit : Window
    {
        public MainWindow mainWindow;

        DoktorProvider dp = new DoktorProvider();
        SqlBaglanti baglanti = new SqlBaglanti();
        DataTable dt = new DataTable();

        string secili = "";

        public static string onaySifresi;//onay ekranından şifreyi tutmak için

        public DoktorKayit()
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
                if (txtSifre.Password.Length < 4)
                {
                    MessageBox.Show("Şifreniz 4 haneden büyük olmalı.");
                }
                else
                {
                    Doktor yeni = new Doktor();
                    yeni.Doktor_TC = txtTC.Text.ToString();
                    yeni.Doktor_Ad = txtAd.Text.ToString();
                    yeni.Doktor_Soyad = txtSoyad.Text.ToString();
                    yeni.Doktor_Sifre = txtSifre.Password.ToString();
                    dp.Ekle(yeni);
                    Listele();
                }
            }
        }

        void dSet()
        {
            dt.Columns.Clear();
            DataColumn d1 = new DataColumn("TC NO", typeof(string));
            DataColumn d2 = new DataColumn("AD", typeof(string));
            DataColumn d3 = new DataColumn("SOYAD", typeof(string));
            dt.Columns.Add(d1);
            dt.Columns.Add(d2);
            dt.Columns.Add(d3);
        }

        void Listele() //Datagrid listeleme işlemi için metot. 
        {
            dt.Columns.Clear();

            dSet();

            dt.Rows.Clear();

            dataGrid1.ItemsSource = null;
            dataGrid1.Items.Refresh();

            dp.listele();
            for (int i = 0; i < dp.isimler.Count; i++)
            {
                dt.Rows.Add(dp.tcler[i].ToString(), dp.isimler[i].ToString(), dp.soyisimler[i].ToString());
            }

            dataGrid1.ItemsSource = dt.DefaultView;
            foreach (var column in dataGrid1.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//geri
        {
            this.Close();
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
                    SqlCommand cmdSira = new SqlCommand("Select doktor_Sifre from Doktor where doktor_TC = '" + seciliRow["TC NO"].ToString() + "'", baglanti.con);
                    SqlDataReader reader = cmdSira.ExecuteReader();
                    if (reader.Read())
                    {
                        secili = reader["doktor_Sifre"].ToString();
                    }
                    baglanti.con.Close();

                    if (onaySifresi == secili.ToString())
                    {
                        Doktor tc = new Doktor();
                        tc.Doktor_TC = seciliRow["TC NO"].ToString();
                        Doktor yenikisi = new Doktor();
                        yenikisi.Doktor_TC = txtTC.Text.ToString();
                        yenikisi.Doktor_Ad = txtAd.Text.ToString();
                        yenikisi.Doktor_Soyad = txtSoyad.Text.ToString();
                        yenikisi.Doktor_Sifre = txtSifre.Password.ToString();
                        dp.Guncelle(tc, yenikisi);
                        Listele();
                        MessageBox.Show("İşlem başarıyla gerçekleştirildi.");
                    }
                    else
                    {
                        MessageBox.Show("İşlemi onaylarken şifrenizi eksik veya hatalı girdiniz.");
                    }
                }
                else
                    MessageBox.Show("Önce güncellenecek kaydı seçmeniz gerekiyor.");
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//insert
        {
            if (txtSifre.Password.Length < 4)
            {
                MessageBox.Show("Şifreniz 4 haneden büyük olmalı.");
            }
            else
            {
                Doktor yeni = new Doktor();
                yeni.Doktor_TC = txtTC.Text.ToString();
                yeni.Doktor_Ad = txtAd.Text.ToString();
                yeni.Doktor_Soyad = txtSoyad.Text.ToString();
                yeni.Doktor_Sifre = txtSifre.Password.ToString();
                dp.Ekle(yeni);
                Listele();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//delete
        { 
            DataGrid dg = dataGrid1;
            DataRowView seciliRow = dg.SelectedItem as DataRowView;
            if (seciliRow != null)
            {
                var ab = new OnayEkrani();
                ab.ShowDialog();

                baglanti.con.Open();
                SqlCommand cmdSira = new SqlCommand("Select doktor_Sifre from Doktor where doktor_TC = '" + seciliRow["TC NO"].ToString() + "'", baglanti.con);
                SqlDataReader reader = cmdSira.ExecuteReader();
                if (reader.Read())
                {
                    secili = reader["doktor_Sifre"].ToString();
                }
                baglanti.con.Close();

                if (onaySifresi == secili.ToString())
                {
                    Doktor silinecekkisi = new Doktor();
                    silinecekkisi.Doktor_TC = seciliRow["TC NO"].ToString();
                    dp.Sil(silinecekkisi);
                    Listele();
                    MessageBox.Show("İşlem başarıyla gerçekleştirildi.");
                }
                else
                {
                    MessageBox.Show("İşlemi onaylarken şifrenizi eksik veya hatalı girdiniz.");
                }
            }
            else
                MessageBox.Show("Önce silinecek kaydı seçmeniz gerekiyor.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Listele();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)//dataGrid Seçili olma event'i
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView seciliRow = dg.SelectedItem as DataRowView;
            if (seciliRow != null)
            {
                txtTC.Text = seciliRow["TC NO"].ToString();
                txtAd.Text = seciliRow["AD"].ToString();
                txtSoyad.Text = seciliRow["SOYAD"].ToString();

                baglanti.con.Open();
                SqlCommand cmdSira = new SqlCommand("Select doktor_Sifre from Doktor where doktor_TC = '" + seciliRow["TC NO"].ToString() + "'", baglanti.con);
                SqlDataReader reader = cmdSira.ExecuteReader();
                if (reader.Read())
                {
                    txtSifre.Password = reader["doktor_Sifre"].ToString();
                }
                baglanti.con.Close();
            }
        }

        private void txtSifre_TextChanged(object sender, TextChangedEventArgs e)
        {}

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                System.Diagnostics.Process.Start("~/Helper/helperr.chm");
            }
        }
    }

    
}
