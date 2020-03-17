using SevenFramework.DataBase;
using SevenFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
  public  class DAO_Gn_Narbo
    {

        public object GetLongLevel(int emp_codi,int nar_codi,int tar_codi)
        {
            try
            {
               
                StringBuilder sql = new StringBuilder();
                sql.Append("  SELECT SUM(NAR_LONG)       ");
                sql.Append("  FROM   GN_NARBO        ");
                sql.Append("  WHERE  EMP_CODI = @EMP_CODI    ");
                sql.Append("  AND NAR_CODI <= @NAR_CODI        ");
                sql.Append("  AND TAR_CODI = @TAR_CODI         ");
                List<SQLParams> parametros = new List<SQLParams>();
                parametros.Add(new SQLParams("EMP_CODI", emp_codi));
                parametros.Add(new SQLParams("TAR_CODI", tar_codi));
                parametros.Add(new SQLParams("NAR_CODI", nar_codi));
                object data = new DbConnection().ExecuteScalar(sql.ToString(), parametros);

                if (data == null)
                    throw new Exception("No se encontraron registros");
                return data;
            }
            catch (Exception ex)
            {
                ExceptionManager.Throw("DAO_Gn_Narbo", "GetLongLevel", ex);
                return 0;
            }
          
        }
    }
}
