using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using SevenFramework.DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
     public  class DAO_Gn_Terce
    {
        public static Gn_Terce GetGnTerce(List<SQLParams> sQLParams)
        {
            string sql = DBHelper.SelectQueryString<Gn_Terce>(sQLParams);
            return new DbConnection().Get<Gn_Terce>(sql,sQLParams);
        }
    }
}
