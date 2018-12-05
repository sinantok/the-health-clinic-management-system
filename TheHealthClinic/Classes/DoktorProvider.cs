using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace Siramatik.Classes
{
    class DoktorProvider
    {
        SqlConnection con;
        SqlCommand cmd;

        public DoktorProvider()
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection("Server=192.168.43.209,1433; User Id=as; Password=123456; Initial Catalog=MyMed; Integrated Security=False;");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        /*public List<Doktor> Listele()
        {

            try
            {
                List<Doktor> doktorListesi = new List<Doktor>();
                cmd.CommandText = "Select * From Doktor";
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Doktor d = new Doktor();
                    d.Doktor_Ad = reader[1].ToString();
                    d.Doktor_Soyad = reader[2].ToString();
                    d.Doktor_TC = reader[3].ToString();
                    d.Doktor_Sifre = reader[4].ToString();
                    doktorListesi.Add(d);
                }

                return doktorListesi;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        */

        public ArrayList isimler = new ArrayList();
        public ArrayList soyisimler = new ArrayList();
        public ArrayList tcler = new ArrayList();
        public ArrayList sifreler = new ArrayList();

        public void listele()//genel listeleme
        {
            isimler.Clear();
            soyisimler.Clear();
            tcler.Clear();
            sifreler.Clear();
            try
            {
                cmd.CommandText = "Select * From Doktor";
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isimler.Add(reader[1].ToString());
                    soyisimler.Add(reader[2].ToString());
                    tcler.Add(reader[3].ToString());
                    sifreler.Add(reader[4].ToString());
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public string doktorTc;

        public void listele2(string ad, string soyad)//isim soyisme göre doktor tcsini almak
        {
            try
            {
                cmd.CommandText = "Select doktor_TC from Doktor where doktor_Ad = '" + ad + "' and doktor_Soyad = '" + soyad + "'";
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    doktorTc = reader["doktor_TC"].ToString();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void Ekle(Doktor d)
        {
            try
            {
                cmd.CommandText = "Insert Into Doktor (doktor_Ad,doktor_Soyad,doktor_TC,doktor_Sifre) Values ('" + d.Doktor_Ad + "','" + d.Doktor_Soyad + "','" + d.Doktor_TC + "','" + d.Doktor_Sifre + "')";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        public void sGuncelleme(Doktor yenisifre, Doktor tc)//şifre güncelleme
        {
            try
            {
                cmd.CommandText = "Update Doktor SET doktor_Sifre='" + yenisifre.Doktor_Sifre.ToString() + "' Where doktor_TC=" + tc.Doktor_TC.ToString() + "";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        public void Guncelle(Doktor tc, Doktor yeniKisi)//genel güncelleme
        {
            try
            {
                cmd.CommandText = "Update Doktor SET doktor_Ad='" + yeniKisi.Doktor_Ad + "',doktor_Soyad='" + yeniKisi.Doktor_Soyad + "',doktor_TC='" + yeniKisi.Doktor_TC + "',doktor_Sifre='" + yeniKisi.Doktor_Sifre + "' Where doktor_TC='" + tc.Doktor_TC + "'";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public void Sil(Doktor d)//tc primarry key olduğu için onu baz alarak silme işlemi yapılıyor..
        {
            try
            {
                cmd.CommandText = "Delete From Doktor Where doktor_TC='" + d.Doktor_TC + "'";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
    }
}
