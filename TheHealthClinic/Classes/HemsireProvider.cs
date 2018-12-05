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
    class HemsireProvider
    {
        SqlConnection con;
        SqlCommand cmd;

        public HemsireProvider()
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection("Server=192.168.43.209,1433; User Id=as; Password=123456; Initial Catalog=MyMed; Integrated Security=False;");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        //public List<Hemsire> hemsireListesi = new List<Hemsire>();

        //public List<Hemsire> Listele()
        //{
        //    try
        //    {
        //        List<Hemsire> hemsireListesi = new List<Hemsire>();
        //        cmd.CommandText = "Select * From Hemsire";
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Hemsire h = new Hemsire();
        //            h.Hemsire_Ad = reader[1].ToString();                   

        //            h.Hemsire_Soyad = reader[2].ToString();

        //            h.Hemsire_TC = reader[3].ToString();


        //            h.Hemsire_Sifre = reader[4].ToString();

        //            hemsireListesi.Add(h);
        //        }

        //        return hemsireListesi;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }
        //}
        public ArrayList isimler = new ArrayList();
        public ArrayList soyisimler = new ArrayList();
        public ArrayList tcler = new ArrayList();
        public ArrayList sifreler = new ArrayList();

        public void listele()
        {
            isimler.Clear();
            soyisimler.Clear();
            tcler.Clear();
            sifreler.Clear();
            try
            { 
                cmd.CommandText = "Select * From Hemsire";
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

        public void Ekle(Hemsire h)
        {
            try
            {
                cmd.CommandText = "Insert Into Hemsire (hemsire_Ad, hemsire_Soyad, hemsire_TC, hemsire_Sifre) Values ('" + h.Hemsire_Ad.ToString() + "','" + h.Hemsire_Soyad.ToString() + "','" + h.Hemsire_TC.ToString() + "','" + h.Hemsire_Sifre.ToString() + "')";
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
        public void sGuncelleme(Hemsire yenisifre, Hemsire tc)
        {
            try
            {
                cmd.CommandText = "Update Hemsire SET hemsire_Sifre='" + yenisifre.Hemsire_Sifre.ToString() + "' Where hemsire_TC='" + tc.Hemsire_TC.ToString() + "'";
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
        public void Guncelle(Hemsire tc, Hemsire yeniKisi)
        {
            try
            {
                cmd.CommandText = "Update Hemsire SET hemsire_Ad='" + yeniKisi.Hemsire_Ad + "',hemsire_Soyad='" + yeniKisi.Hemsire_Soyad + "',hemsire_TC='" + yeniKisi.Hemsire_TC + "',hemsire_Sifre='" + yeniKisi.Hemsire_Sifre + "' Where hemsire_TC='" + tc.Hemsire_TC + "'";
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

        public void Sil(Hemsire silineceKisi)
        {
            try
            {
                cmd.CommandText = "Delete From Hemsire Where hemsire_TC = '" + silineceKisi.Hemsire_TC + "'";
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
