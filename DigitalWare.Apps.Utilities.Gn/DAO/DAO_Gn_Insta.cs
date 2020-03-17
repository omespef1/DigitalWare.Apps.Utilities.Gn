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
   public class DAO_Gn_Insta
    {
        public static Gn_Insta GetGnInsta()
        {
            List<SQLParams> sqlparams = new List<SQLParams>();                 
            string sql = DBHelper.SelectQueryString<Gn_Insta>();
            return new DbConnection().Get<Gn_Insta>(sql);
        }
    }
}
