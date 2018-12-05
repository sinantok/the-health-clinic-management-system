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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Siramatik
{

    public partial class MainWindow : Window
    {
        public DoktorGiris doktorGiris;
        public DoktorKayit doktorKayit;
        public HastaDuzenle hastaDuzenle;
        public OnayEkrani onayEkrani;
        public HastaKabul hastaKabul;
        public HastaKayit hastaKayit;
        public HemsireGiris hemsireGiris;
        public HemsireKayit hemsireKayit;
        public MonitorEkrani monitörEkrani;
        public Poliklinik poliklinik;
        public YeniSifre yeniSifre;

        public static string fBilgisi;

        public MainWindow()
        {
            InitializeComponent();

            doktorGiris = new DoktorGiris(); doktorKayit = new DoktorKayit(); hastaDuzenle = new HastaDuzenle();
            onayEkrani = new OnayEkrani(); hastaKabul = new HastaKabul(); hastaKayit = new HastaKayit();
            hemsireGiris = new HemsireGiris(); hemsireKayit = new HemsireKayit();
            monitörEkrani = new MonitorEkrani(); poliklinik = new Poliklinik(); yeniSifre = new YeniSifre();

            doktorGiris.mainWindow = this; doktorKayit.mainWindow = this; hastaDuzenle.mainWindow = this;
            onayEkrani.mainWindow = this; hastaKabul.mainWindow = this; hastaKayit.mainWindow = this;
            hemsireGiris.mainWindow = this; hemsireKayit.mainWindow = this; monitörEkrani.mainWindow = this;
            poliklinik.mainWindow = this; yeniSifre.mainWindow = this;
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
        public static MainWindow ab;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = new DoktorGiris();
            a.Show();
            //this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var a = new HemsireGiris();
            a.Show();
            //this.Hide();
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
