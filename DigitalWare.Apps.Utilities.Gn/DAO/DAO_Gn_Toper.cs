using DigitalWare.Apps.Utilities.Gn.TO;
using Ophelia;
using SevenFramework.DataBase;
using SevenFramework.DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Toper
    {
        public static Gn_Toper GetGnToper(List<SQLParams> sQLParams)
        {
            string sql = DBHelper.SelectQueryString<Gn_Toper>(sQLParams);
            return new DbConnection().Get<Gn_Toper>(sql, sQLParams);
        }

       

    }
}
