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
using System.Windows.Threading;
using System.Data;

namespace Siramatik
{
    public partial class MonitorEkrani : Window
    {
        public MainWindow mainWindow;

        public MonitorEkrani()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        SiraProvider sp = new SiraProvider();

        void dSet()
        {
            DataColumn d1 = new DataColumn("SIRA NO", typeof(string));
            DataColumn d2 = new DataColumn("AD", typeof(string));
            DataColumn d3 = new DataColumn("SOYAD", typeof(string));
            dt.Columns.Add(d1);
            dt.Columns.Add(d2);
            dt.Columns.Add(d3);
        }

        public void hastaSirala()//DataGrid içerisine hastaları sıralama.
        {
            dt.Columns.Clear();

            dSet();

            dt.Rows.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.Items.Refresh();

            sp.listele2(DoktorGiris.DoktorTcTut);
            for (int i = 0; i < sp.tcHasta.Count; i++)
            {
                dt.Rows.Add((i + 1).ToString(), sp.h_Isimler[i].ToString(), sp.h_Soyisimler[i].ToString());
            }
            dataGrid1.ItemsSource = dt.DefaultView;
            foreach (var column in dataGrid1.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }

        }

        public void sec()
        {
            DataGrid dg = dataGrid1;
            DataRowView seciliRow;

            dataGrid1.SelectedIndex = 0;
            seciliRow = dg.SelectedItem as DataRowView;
            if (seciliRow != null)
            {
                LblHasta.Content = seciliRow["AD"].ToString() + " " + seciliRow["SOYAD"].ToString();
            }

            dataGrid1.SelectedIndex = 1;
            seciliRow = dg.SelectedItem as DataRowView;
            if (seciliRow != null)
            {
                LblSıradaki.Content = seciliRow["AD"].ToString() + " " + seciliRow["SOYAD"].ToString();
            }

            dataGrid1.SelectedIndex = 0;//daima 1. satırı seçili halde bırakmak için

            //DataGridCell cell = Datagrid.GetCell(dataGrid1, 0, 4);
            //cell.IsEnabled = false;
            //LblHasta.Content = cell.Content as TextBlock;

            //DataGridCell cell1 = Datagrid.GetCell(dataGrid1, 3, 4);
            //cell.IsEnabled = false;
            //TextBlock tb = cell.Content as TextBlock;
            //MessageBox.Show(tb.Text.ToString());
            //MessageBox.Show(tb.Text.ToString());


        }

        public void temizle()
        {
            LblHasta.Content = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hastaSirala();
            lblDoktor.Content = DoktorGiris.doctor;
        }
    }
}
