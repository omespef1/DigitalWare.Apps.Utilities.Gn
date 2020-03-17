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
  public  class DAO_Gn_Items
    {

        public static List<Gn_Items> GetGnItems(int ite_cont,int tit_cont,string ite_codi)
        {
            List<SQLParams> sQLParams = new List<SQLParams>(); 
            if(!string.IsNullOrEmpty(ite_codi))
            sQLParams.Add(new SQLParams("ITE_CODI", ite_codi));
            if(ite_cont>0)
            sQLParams.Add(new SQLParams("ITE_CONT", ite_cont));
            if(tit_cont>0)
            sQLParams.Add(new SQLParams("TIT_CONT", tit_cont));
            string sql = DBHelper.SelectQueryString<Gn_Items>(sQLParams);
            sql = sql.Replace("@", ":");
            return new DbConnection().GetList<Gn_Items>(sql, sQLParams);
        }
    }
}
