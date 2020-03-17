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
    public class DAO_Gn_Usuar
    {

        public Gn_Usuar GetGnUsuar(string usu_codi, string usu_idpk)
        {
            List<SQLParams> sqlParams = new List<SQLParams>();
            sqlParams.Add(new SQLParams("USU_CODI", usu_codi));
            sqlParams.Add(new SQLParams("USU_IDPK", usu_idpk));
            string sql = DBHelper.SelectQueryString<Gn_Usuar>(sqlParams);
            return new DbConnection().Get<Gn_Usuar>(sql, sqlParams);
        }

        public Gn_Usuar GetGnUsuar(string usu_codi)
        {
            List<SQLParams> sqlParams = new List<SQLParams>();
            sqlParams.Add(new SQLParams("USU_CODI", usu_codi));            
            string sql = DBHelper.SelectQueryString<Gn_Usuar>(sqlParams);
            return new DbConnection().Get<Gn_Usuar>(sql, sqlParams);
        }
        public int updatePassword(string usu_codi, string usu_idpk)
        {
            List<SQLParams> sqlParams = new List<SQLParams>();
            sqlParams.Add(new SQLParams("USU_CODI", usu_codi));
            sqlParams.Add(new SQLParams("USU_IDPK", usu_idpk));
            sqlParams.Add(new SQLParams("USU_FCLA", DateTime.Now));
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE GN_USUAR SET USU_IDPK = @USU_IDPK , USU_FCLA = @USU_FCLA WHERE USU_CODI = @USU_CODI");
            return new DbConnection().Update(sql.ToString(), sqlParams);
        }
    }
}
