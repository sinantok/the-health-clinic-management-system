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
using System.Data;
using System.Windows.Threading;

namespace Siramatik
{
    public partial class Poliklinik : Window
    {
        public MainWindow mainWindow;

        public Poliklinik()
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
                    var a = new DoktorGiris();
                    a.Show();
                    this.Close();
                }
            }
        }

        public static DispatcherTimer timer1 = new DispatcherTimer();

        HastaProvider hp = new HastaProvider();
        DoktorProvider dp = new DoktorProvider();
        SiraProvider sp = new SiraProvider();
        DataTable dt = new DataTable();
        SqlBaglanti baglanti = new SqlBaglanti();
        int hasta_Sayisi;
        string hasta_Tc;
        public static MonitorEkrani mEkrani;

        void dSet()
        {
            DataColumn d1 = new DataColumn("SIRA NO", typeof(string));
            DataColumn d2 = new DataColumn("TC NO", typeof(string));
            DataColumn d3 = new DataColumn("AD", typeof(string));
            DataColumn d4 = new DataColumn("SOYAD", typeof(string));
            DataColumn d5 = new DataColumn("DURUM", typeof(string));

            dt.Columns.Add(d1);
            dt.Columns.Add(d2);
            dt.Columns.Add(d3);
            dt.Columns.Add(d4);
            dt.Columns.Add(d5);
        }

        void hasta_Sirala()//DataGrid içerisine hastaları sıralama.
        {
            hasta_Sayisi = 0;
            dt.Columns.Clear();

            dSet();

            dt.Rows.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.Items.Refresh();

            sp.listele2(DoktorGiris.DoktorTcTut);
            for (int i = 0; i < sp.tcHasta.Count; i++)
            {
                hasta_Tc = sp.tcHasta[0].ToString();
                dt.Rows.Add((i+1).ToString(), sp.tcHasta[i].ToString(), sp.h_Isimler[i].ToString(), sp.h_Soyisimler[i].ToString(), sp.durumlar[i].ToString());
                hasta_Sayisi++;
            }
            dataGrid1.ItemsSource = dt.DefaultView;
            foreach (var column in dataGrid1.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//hastayı içeriye alıp durumları güncellenecek
        {
            if (dataGrid1.Items == null)
            {
                MessageBox.Show("Hatalı İşlem!/nSistemde hasta bulunmamaktadır.");
            }
            else
            {
                timer1.Stop();//işlemlerin karışmaması için durdurulması gerekiyor

                baglanti.con.Open();
                SqlCommand cmdUpdate = new SqlCommand("Update Sira SET hasta_Durum='Muayene Oluyor' Where hasta_TC='" + hasta_Tc + "'", baglanti.con);
                cmdUpdate.ExecuteNonQuery();
                baglanti.con.Close();
                hasta_Sirala();

                if (mEkrani!=null)
                {
                    mEkrani.sec();
                }
               
                DataGrid dg = dataGrid1;
                DataRowView seciliRow;
                dataGrid1.SelectedIndex = 0;
                seciliRow = dg.SelectedItem as DataRowView;
                if (seciliRow != null)
                {
                    namee.Text = seciliRow["AD"].ToString();
                    lastNamee.Text = seciliRow["SOYAD"].ToString();

                    SqlCommand cmd = new SqlCommand("Select * From Hasta where hasta_Tc='" + seciliRow["TC NO"].ToString() + "'", baglanti.con);
                    baglanti.con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        birthDayy.Text = reader["hasta_Dogum"].ToString();
                        sex.Text = reader["hasta_Cinsiyet"].ToString();
                    }
                    baglanti.con.Close();
                }
                dataGrid1.SelectedIndex = -1;
                timer1.Start();//Zamanlayıcı tekrardan başlıyor
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbDoktor.Text = DoktorGiris.doctor;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//ekrana yansıt
        {
            //Screen[] ekranlar = System.Windows.Forms.Screen.AllScreens;
            //if (Screen.AllScreens.Length > 1)
            //{
            //    //yeni from
            //    frm = new form2();
            //    // Önemli !
            //    frm.StartPosition = FormStartPosition.Manual;
            //    // İkinci Monitörü tanımla
            //    Screen screen = GetSecondaryScreen();
            //    // İkinci formun location tanımla
            //    frm.Location = screen.WorkingArea.Location;
            //    // fullscreen yap
            //    frm.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
            //    // formu aç
            mEkrani = new MonitorEkrani();
            mEkrani.Show();
            mEkrani.hastaSirala();
            //    frm.Show(this);
            //}

            //public Screen GetSecondaryScreen()
            //{
            //    if (Screen.AllScreens.Length == 1)
            //    {
            //        return null;
            //    }
            //    foreach (Screen screen in Screen.AllScreens)
            //    {
            //        if (screen.Primary == false)
            //        {
            //            return screen;
            //        }
            //    }
            //    return null;
            //}


        }

        public void timersay(object sender, EventArgs e)
        {
            hasta_Sirala();
            hastaSayisiLabel.Content = "Bekleyen Toplam Hasta Sayısı: " + hasta_Sayisi.ToString();
            dataGrid1.SelectedIndex = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//muayene olan hasta silinecek
        {
            if (dataGrid1.Items == null)
            {
                MessageBox.Show("Hatalı İşlem!/nSistemde hasta bulunmamaktadır.");
            }
            else
            {
                timer1.Stop();

                baglanti.con.Open();
                SqlCommand cmdSil = new SqlCommand("Delete From Sira Where hasta_TC='" +hasta_Tc + "'", baglanti.con);
                cmdSil.ExecuteNonQuery();
                baglanti.con.Close();
                hasta_Sirala();

                if (mEkrani!=null)
                {
                    mEkrani.hastaSirala();
                    mEkrani.temizle();
                }
               
                timer1.Start();
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)//monitöre yansıt
        {
            //Screen[] ekranlar = System.Windows.Forms.Screen.AllScreens;
            //if (Screen.AllScreens.Length > 1)
            //{
            //    //yeni from
            //    frm = new form2();
            //    // Önemli !
            //    frm.StartPosition = FormStartPosition.Manual;
            //    // İkinci Monitörü tanımla
            //    Screen screen = GetSecondaryScreen();
            //    // İkinci formun location tanımla
            //    frm.Location = screen.WorkingArea.Location;
            //    // fullscreen yap
            //    frm.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
            //    // formu aç
            //    frm.Show(this);
            //}

            //public Screen GetSecondaryScreen()
            //{
            //    if (Screen.AllScreens.Length == 1)
            //    {
            //        return null;
            //    }
            //    foreach (Screen screen in Screen.AllScreens)
            //    {
            //        if (screen.Primary == false)
            //        {
            //            return screen;
            //        }
            //    }
            //    return null;
            //}
           
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//BAŞLAT
        {
            hasta_Sirala();
            timer1.IsEnabled = true;
            timer1.Interval = TimeSpan.FromSeconds(5);
            timer1.Start();
            timer1.Tick += timersay;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//DURDUR
        {
            timer1.Stop();

            dt.Columns.Clear();
            dt.Rows.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.Items.Refresh();
            if (mEkrani != null)
            { mEkrani.Close(); }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void geri(object sender, RoutedEventArgs e)
        {
            var a = new DoktorGiris();
            a.Show();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (mEkrani != null)
            {
                mEkrani.Close();
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
