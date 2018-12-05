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
    /// <summary>
    /// HastaKabul.xaml etkileşim mantığı
    /// </summary>
    public partial class HastaKabul : Window
    {
        public MainWindow mainWindow;
        //instance
        HastaProvider hp = new HastaProvider();
        DoktorProvider dp = new DoktorProvider();
        SiraProvider sp = new SiraProvider();
        SqlBaglanti baglanti = new SqlBaglanti();

        DataTable dt = new DataTable();

        public static string HastaTcTut;//güncelleme ekranı için oluşturulmuş tcno tutan değişkeni

        string metin1;
        string metin2;
        string doktorTC;
        int sira = 0;

        public HastaKabul()
        {
            InitializeComponent();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc1);//esc
            this.PreviewKeyDown += new KeyEventHandler(HandleEnter1);//enter
            this.PreviewKeyDown += new KeyEventHandler(HandleNew);//ctrl+n
            this.PreviewKeyDown += new KeyEventHandler(HandleUpdate);//ctrl+u
        }

       

        void dSet()
        {
            DataColumn d1 = new DataColumn("TC NO", typeof(string));
            DataColumn d2 = new DataColumn("AD", typeof(string));
            DataColumn d3 = new DataColumn("SOYAD", typeof(string));
            DataColumn d4 = new DataColumn("CİNSİYET", typeof(string));
            DataColumn d5 = new DataColumn("YAS", typeof(string));
            dt.Columns.Add(d1);
            dt.Columns.Add(d2);
            dt.Columns.Add(d3);
            dt.Columns.Add(d4);
            dt.Columns.Add(d5);
        }

        void doktorListele()//combobox içerisine doktorları atama
        {
            cmbDoktor.Items.Clear();
            dp.listele();
            for (int i = 0; i < dp.isimler.Count; i++)
            {
                cmbDoktor.Items.Add(dp.isimler[i] + " " + dp.soyisimler[i]).ToString();
            }
        }

        public void Listele() //Datagrid listeleme işlemi için metot. 
        {
            dt.Columns.Clear();

            dSet();

            dt.Rows.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.Items.Refresh();

            hp.listele();
            for (int i = 0; i < hp.isimler.Count; i++)
            {
                dt.Rows.Add(hp.tcler[i].ToString(), hp.isimler[i].ToString(), hp.soyisimler[i].ToString(), hp.cinsiyet[i].ToString(), hp.yas[i].ToString());
            }
            dataGrid1.ItemsSource = dt.DefaultView;
            foreach (var column in dataGrid1.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//insert
        {
            var a = new HastaKayit();
            a.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//update
        {
            HastaTcTut = txtTC.Text.ToString();
            if (HastaTcTut.ToString() == "")
            {
                MessageBox.Show("Lütfen düzenlenecek hastanın TC numarasını giriniz");
            }
            else
            {
                var a = new HastaDuzenle();
                a.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//sıra ver
        {
            if (txtTC.Text == "")
            {
                MessageBox.Show("Sıra vermek için önce hasta seçiniz");
            }
            else
            {


                //AYNI HASTA SIRA TABLOSUNA BİRDEN FAZLA KAYIT EDİLMEMELİ!!!
                string Metin = cmbDoktor.Text;

                int bosluk = 0;
                for (int i = 0; i < Metin.Length; i++)
                {
                    if (Metin.Substring(i, 1) == " ")
                    {
                        bosluk++;
                    }
                }

                if (bosluk == 1)
                {
                    char[] ayrac = { ' ' };
                    string[] kelimeler = Metin.Split(ayrac);

                    metin1 = kelimeler[0];
                    metin2 = kelimeler[1];
                }
                else if (bosluk == 2)
                {
                    char[] ayrac = { ' ' };
                    string[] kelimeler = Metin.Split(ayrac, 3);

                    metin1 = kelimeler[0] + " " + kelimeler[1];
                    metin2 = kelimeler[2];
                }

                MessageBox.Show(metin1 + " " + metin2);

                dp.listele2(metin1, metin2);
                doktorTC = dp.doktorTc;

                baglanti.con.Open();
                SqlCommand cmdSira = new SqlCommand("Select hasta_Sira from Sira where doktor_TC = '" + doktorTC + "'", baglanti.con);
                SqlDataReader reader = cmdSira.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        sira = Convert.ToInt32(reader["hasta_Sira"]);
                    }
                }
                else
                    sira = 0;

                baglanti.con.Close();

                sira = sira + 1;

                Sira yeni = new Sira();
                yeni.Hasta_TC = txtTC.Text.ToString();
                yeni.Doktor_TC = doktorTC.ToString();
                yeni.Hasta_Sira = sira;
                yeni.Hasta_Durum = "Bekliyor";
                sp.Ekle(yeni);
                MessageBox.Show("İşlemi başarılı");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbOncelik.Items.Add("Öncelik Yok");
            cmbOncelik.Items.Add("65 Yaş Üstü");
            cmbOncelik.Items.Add("Hamile");

            doktorListele();
            Listele();
            cmbOncelik.Text = "Öncelik Yok";
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)//datagride tıklandığında tc txtsine seçilş olanı atama
        {
            DataGrid dg = dataGrid1;
            DataRowView seciliRow = dg.SelectedItem as DataRowView;
            if (seciliRow != null)
            {
                HastaTcTut = seciliRow["TC NO"].ToString();
                txtTC.Text = seciliRow["TC NO"].ToString();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//search
        { }

        private void txtTC_TextChanged(object sender, TextChangedEventArgs e)//textSearch
        {
            if (txtTC.Text == "")
            {
                Listele();
            }
            else
            {
                DataTable dt = new DataTable();
                DataColumn d1 = new DataColumn("TC NO", typeof(string));
                DataColumn d2 = new DataColumn("AD", typeof(string));
                DataColumn d3 = new DataColumn("SOYAD", typeof(string));
                DataColumn d4 = new DataColumn("CİNSİYET", typeof(string));
                DataColumn d5 = new DataColumn("YAS", typeof(string));
                dt.Columns.Add(d1);
                dt.Columns.Add(d2);
                dt.Columns.Add(d3);
                dt.Columns.Add(d4);
                dt.Columns.Add(d5);

                dataGrid1.ItemsSource = null;
                SqlCommand cmd = new SqlCommand("Select * From Hasta where hasta_TC Like '%" + txtTC.Text + "%'", baglanti.con);
                baglanti.con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add(reader["hasta_TC"], reader["hasta_Ad"], reader["hasta_Soyad"], reader["hasta_Cinsiyet"], reader["hasta_Dogum"]);
                    cmbDoktor.Text = reader["hasta_Doktor"].ToString();     
                }
                dataGrid1.ItemsSource = dt.DefaultView;
                baglanti.con.Close();
                foreach (var column in dataGrid1.Columns)
                {
                    column.MinWidth = column.ActualWidth;
                    column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }
        }

        private void geri(object sender, RoutedEventArgs e)//geri butonu
        {
            var a = new HemsireGiris();
            a.Show();
            this.Close();
        }

        private void HandleEsc1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (MessageBox.Show("Çıkış yapmak istiyor musunuz?", "Çıkış Ekranı", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var a = new HemsireGiris();
                    a.Show();
                    this.Close();
                }
            }
        }
        private void HandleEnter1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtTC.Text == "")
                {
                    MessageBox.Show("Sıra vermek için Önce hasta seçiniz");
                }
                string Metin = cmbDoktor.Text;

                int bosluk = 0;
                for (int i = 0; i < Metin.Length; i++)
                {
                    if (Metin.Substring(i, 1) == " ")
                    {
                        bosluk++;
                    }
                }

                if (bosluk == 1)
                {
                    char[] ayrac = { ' ' };
                    string[] kelimeler = Metin.Split(ayrac);

                    metin1 = kelimeler[0];
                    metin2 = kelimeler[1];
                }
                else if (bosluk == 2)
                {
                    char[] ayrac = { ' ' };
                    string[] kelimeler = Metin.Split(ayrac, 3);

                    metin1 = kelimeler[0] + " " + kelimeler[1];
                    metin2 = kelimeler[2];
                }

                MessageBox.Show(metin1 + " " + metin2);

                dp.listele2(metin1, metin2);
                doktorTC = dp.doktorTc;

                baglanti.con.Open();
                SqlCommand cmdSira = new SqlCommand("Select hasta_Sira from Sira where doktor_TC = '" + doktorTC + "'", baglanti.con);
                SqlDataReader reader = cmdSira.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        sira = Convert.ToInt32(reader["hasta_Sira"]);
                    }
                }
                else
                    sira = 0;

                baglanti.con.Close();

                sira = sira + 1;

                Sira yeni = new Sira();
                yeni.Hasta_TC = txtTC.Text.ToString();
                yeni.Doktor_TC = doktorTC.ToString();
                yeni.Hasta_Sira = sira;
                yeni.Hasta_Durum = "Bekliyor";
                sp.Ekle(yeni);
                MessageBox.Show("İşlemi başarılı");

            }
        }
        private void HandleNew(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control)
            {
                var a = new HastaKayit();
                a.ShowDialog();
            }
        }

        private void HandleUpdate(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.U)
            {
                HastaTcTut = txtTC.Text.ToString();
                if (HastaTcTut.ToString() == "")
                {
                    MessageBox.Show("Lütfen düzenlenecek hastanın TC numarasını giriniz");
                }
                else
                {
                    var a = new HastaDuzenle();
                    a.Show();
                }
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
