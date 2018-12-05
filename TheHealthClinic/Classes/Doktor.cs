using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siramatik.Classes
{
    class Doktor
    {
        int doktor_Id;
        public int Doktor_Id
        {
            get { return doktor_Id; }
            set { doktor_Id = value; }
        }


        string doktor_Ad;
        public string Doktor_Ad
        {
            get { return doktor_Ad; }
            set { doktor_Ad = value; }
        }


        string doktor_Soyad;
        public string Doktor_Soyad
        {
            get { return doktor_Soyad; }
            set { doktor_Soyad = value; }
        }


        string doktor_TC;
        public string Doktor_TC
        {
            get { return doktor_TC; }
            set { doktor_TC = value; }
        }
        string doktor_Sifre;
        public string Doktor_Sifre
        {
            get { return doktor_Sifre; }
            set { doktor_Sifre = value; }
        }
    }
}
