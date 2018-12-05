using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siramatik.Classes
{
    class Sira
    {
        string hasta_TC;
        public string Hasta_TC
        {
            get { return hasta_TC; }
            set { hasta_TC = value; }
        }

        string doktor_TC;
        public string Doktor_TC
        {
            get { return doktor_TC; }
            set { doktor_TC = value; }
        }

        int hasta_Sira;
        public int Hasta_Sira
        {
            get { return hasta_Sira; }
            set { hasta_Sira = value; }
        }

        string hasta_Durum;
        public string Hasta_Durum
        {
            get { return hasta_Durum; }
            set { hasta_Durum = value; }
        }

    }
}
