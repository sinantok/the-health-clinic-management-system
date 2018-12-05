using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Siramatik.Classes
{
    class SiraProvider
    {
        SqlConnection con;
        SqlCommand cmd;

        public SiraProvider()
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection("Server=192.168.43.209,1433; User Id=as; Password=123456; Initial Catalog=MyMed; Integrated Security=False;");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        public ArrayList tcHasta = new ArrayList();
        public ArrayList siralar = new ArrayList();
        public ArrayList tcDoktor = new ArrayList();
        public ArrayList durumlar = new ArrayList();
        public ArrayList h_Isimler = new ArrayList();
        public ArrayList h_Soyisimler = new ArrayList();

        public void listele()//hepsini sırala
        {
            tcHasta.Clear();
            tcDoktor.Clear();
            siralar.Clear();
            durumlar.Clear();
            try
            {
                cmd.CommandText = "Select * From Sira";
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tcHasta.Add(reader[0].ToString());
                    tcHasta.Add(reader[1].ToString());
                    siralar.Add(reader[2].ToString());
                    durumlar.Add(reader[3].ToString());
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

        public void listele2(String tc)//doktor tc'sine göre sırala
        {
            tcHasta.Clear();
            siralar.Clear();
            durumlar.Clear();
            h_Isimler.Clear();
            h_Soyisimler.Clear();
            try
            {
                cmd.CommandText = "Select h.hasta_Ad, h.hasta_Soyad, h.hasta_TC, s.hasta_Sira, s.doktor_TC, s.hasta_Durum From Hasta as h Inner Join Sira as s on s.hasta_TC = h.hasta_TC where s.doktor_TC = '" + tc + "'";
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    siralar.Add(reader[3].ToString());
                    tcHasta.Add(reader[2].ToString());
                    h_Isimler.Add(reader[0].ToString());
                    h_Soyisimler.Add(reader[1].ToString());
                    durumlar.Add(reader[5].ToString());              
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

        public void Ekle(Sira s)
        {
            try
            {
                cmd.CommandText = "Insert Into Sira (hasta_TC, doktor_TC, hasta_Sira, hasta_Durum) Values (" + s.Hasta_TC + ",'" + s.Doktor_TC + "','" + s.Hasta_Sira + "','"+s.Hasta_Durum+"')";
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

        public void Sil(Sira s)
        {
            try
            {
                cmd.CommandText = "Delete From Sira Where hasta_TC=" + s.Hasta_TC + "";
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
