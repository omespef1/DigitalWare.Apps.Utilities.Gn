using DigitalWare.Apps.Utilities.Gn.TO;
using Ophelia;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
   public class DAO_Gn_Empre
    {
        public List<Gn_Empre> GetGnEmpre(string usu_codi)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("  SELECT REM.EMP_CODI,                ");
            sql.Append("  EMP.EMP_NOMB                        ");
            sql.Append("  FROM   GN_REMGU REM                 ");
            sql.Append("  INNER JOIN GN_GRUSU GRU             ");
            sql.Append("  ON REM.GRU_CODI = GRU.GRU_CODI      ");
            sql.Append("  INNER JOIN GN_USUAR USU             ");
            sql.Append("  ON GRU.GRU_CODI = USU.GRU_CODI      ");
            sql.Append("  INNER JOIN GN_EMPRE EMP             ");
            sql.Append("  ON REM.EMP_CODI = EMP.EMP_CODI      ");
            sql.Append("  WHERE USU.USU_CODI = @USU_CODI     ");
            List<SQLParams> sQLParams = new List<SQLParams>();
            sQLParams.Add(new SQLParams("USU_CODI", usu_codi));
            return new DbConnection().GetList<Gn_Empre>(sql.ToString(), sQLParams);
        }

        /// <summary>
        /// Retorna toda la lista de empresas existentes
        /// </summary>
        /// <returns></returns>
        public List<Gn_Empre> GetGnEmpre()
        {
            OException o = new OException();
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT * FROM GN_EMPRE ");
                List<SQLParams> sQLParams = new List<SQLParams>();
                return new DbConnection().GetList<Gn_Empre>(sql.ToString(), sQLParams);
            }
            catch (Exception ex)
            {
                o.Throw("DAO_Gn_Empre", "GetGnEmpre", ex);
                return null;
            }
           
        }
    }
}
