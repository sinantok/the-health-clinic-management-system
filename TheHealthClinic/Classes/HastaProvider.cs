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
    class HastaProvider
    {
        SqlConnection con;
        SqlCommand cmd;

        public HastaProvider() 
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection("Server=192.168.43.209,1433; User Id=as; Password=123456; Initial Catalog=MyMed; Integrated Security=False;");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        //public List<Hasta> Listele()
        //{
        //    try
        //    {
        //        List<Hasta> hastaListesi = new List<Hasta>();
        //        cmd.CommandText = "Select * From Hasta";
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Hasta hs = new Hasta();
        //            hs.Hasta_Ad = reader[1].ToString();
        //            hs.Hasta_Soyad = reader[2].ToString();
        //            hs.Hasta_TC = reader[3].ToString();
        //            hs.Hasta_Cinsiyet = reader[4].ToString();
        //            hs.Hasta_Dogum = reader[3].ToString();
        //            hs.Hasta_Doktor = reader[4].ToString();
        //            hastaListesi.Add(hs);
        //        }

        //        return hastaListesi;
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
        public ArrayList cinsiyet = new ArrayList();
        public ArrayList yas = new ArrayList();

        DateTime today =new DateTime();
        DateTime birthDay=new DateTime();
        TimeSpan yıl = new TimeSpan();
        int a;

        public void listele()
        {
            today = Convert.ToDateTime(DateTime.Now);

            isimler.Clear();
            soyisimler.Clear();
            tcler.Clear();
            cinsiyet.Clear();
            yas.Clear();

            try
            {
                cmd.CommandText = "Select * From Hasta";
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    isimler.Add(reader[1].ToString());

                    soyisimler.Add(reader[2].ToString());

                    tcler.Add(reader[3].ToString());

                    cinsiyet.Add(reader[4].ToString());

                    //yılhesaplatma:
                    birthDay = Convert.ToDateTime(reader[6]);
                    yıl = today - birthDay;
                    a = Math.Abs(yıl.Days) / 365;

                    yas.Add(a.ToString());

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

        public void Ekle(Hasta hs)
        {
            try
            {
                cmd.CommandText = "Insert Into Hasta (hasta_Ad, hasta_Soyad, hasta_TC, hasta_Cinsiyet, hasta_Doktor, hasta_Dogum) Values ('" + hs.Hasta_Ad + "','" + hs.Hasta_Soyad + "','" + hs.Hasta_TC + "','" + hs.Hasta_Cinsiyet + "','"+ hs.Hasta_Doktor +"','"+hs.Hasta_Dogum + "')";
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

        public void Guncelle(Hasta tc, Hasta yeniKisi)
        {
            try
            {
                cmd.CommandText = "Update Hasta SET hasta_Ad='" + yeniKisi.Hasta_Ad + "',hasta_Soyad='" + yeniKisi.Hasta_Soyad + "',hasta_TC='" + yeniKisi.Hasta_TC + "',hasta_Cinsiyet='" + yeniKisi.Hasta_Cinsiyet + "',hasta_Dogum='"+yeniKisi.Hasta_Dogum+"',hasta_Doktor='"+yeniKisi.Hasta_Doktor+"' Where hasta_TC='" + tc.Hasta_TC + "'";
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

        public void Sil(Hasta hs)
        {
            try
            {
                cmd.CommandText = "Delete From Hasta Where hasta_TC='" + hs.Hasta_TC + "'";
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
