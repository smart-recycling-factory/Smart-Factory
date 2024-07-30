using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactory.Helpers
{
    class Common
    {
        public static readonly string CONNSTRING = "Data Source=localhost;Initial Catalog=smart_factory;Persist Security Info=True;User ID=sa;Encrypt=False;" +
                                                   "Password = mssql_p@ss";

        public static Boolean IsLogined = false;

        public static String LoginedId = String.Empty;

        public static int LoginIdx = -1;
    }
}
