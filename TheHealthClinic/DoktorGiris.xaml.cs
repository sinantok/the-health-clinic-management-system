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

namespace Siramatik
{

    public partial class DoktorGiris : Window
    {
        public MainWindow mainWindow;

        public static string doctor;
        public static string DoktorTcTut;
        public static Poliklinik dGiris;
        Classes.SqlBaglanti baglanti = new Classes.SqlBaglanti();

        public DoktorGiris()
        {
            InitializeComponent();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc1);
            this.PreviewKeyDown += new KeyEventHandler(HandleEnter1);
            this.PreviewKeyDown += new KeyEventHandler(HandleNew);//ctrl+n
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

        private void HandleEnter1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    baglanti.con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from Doktor where doktor_TC='" + txtTC.Text + "' and doktor_Sifre='" + txtSifre.Password + "'", baglanti.con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        doctor = reader["doktor_Ad"].ToString() + " " + reader["doktor_Soyad"].ToString();
                        DoktorTcTut = txtTC.Text;
                        dGiris = new Poliklinik();
                        dGiris.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("TC No veya şifreyi yanlış girdiniz!");
                    }
                }

                catch (Exception)
                {
                    throw;
                }

                finally
                {
                    if (baglanti.con != null)
                    {
                        baglanti.con.Close();
                    }
                }
            }
        }

        private void HandleNew(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                var ac = new DoktorKayit();
                ac.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//giriş
        {
            try
            {
                baglanti.con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Doktor where doktor_TC='" + txtTC.Text + "' and doktor_Sifre='" + txtSifre.Password + "'", baglanti.con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    doctor = reader["doktor_Ad"].ToString() + " " + reader["doktor_Soyad"].ToString();
                    DoktorTcTut = txtTC.Text;
                    dGiris = new Poliklinik();
                    dGiris.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC No veya şifreyi yanlış girdiniz!");
                }
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (baglanti.con != null)
                {
                    baglanti.con.Close();
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//yenikayıt
        {
            var ac = new DoktorKayit();
            ac.ShowDialog();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)//şifremi unuttum
        {
            MainWindow.fBilgisi = "Doktor";
            var ac = new YeniSifre();
            ac.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSifre.Password= "";
            txtTC.Text = "";
        }

        private void geri(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
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
