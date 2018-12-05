using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Siramatik.Classes
{
    class SqlBaglanti
    {
        public SqlConnection con = new SqlConnection("Server=192.168.43.209,1433; User Id=as; Password=123456; Initial Catalog=MyMed; Integrated Security=False;");

        /*public void Baglan()
        {
            con = new SqlConnection("server=.; Initial Catalog=dbokul;Integrated Security=SSPI");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        */
    }
}
