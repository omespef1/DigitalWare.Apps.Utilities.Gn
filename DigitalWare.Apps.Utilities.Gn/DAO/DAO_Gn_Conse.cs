using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
  public  class DAO_Gn_Conse
    {
        public int getGnConse(int emp_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT COALESCE(MAX(RAD_CONT),0) RAD_CONT ");
            sql.Append(" FROM   GN_RADJU ");
            sql.Append(" WHERE  EMP_CODI  = @EMP_CODI  ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("EMP_CODI", emp_codi));
            DataSet data = new DbConnection().GetDataSet(sql.ToString(), parametros);
            var result = data.Tables[0].Rows[0]["RAD_CONT"];
            if (result == null)
                return 0;
            return int.Parse(result.ToString());
        }
        public int updateGnConse(int emp_codi ,int con_codi, double con_disp)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("  UPDATE GN_CONSE ");
            sql.Append("   SET   CON_DISP = @CON_DISP  	 ");
            sql.Append("   WHERE EMP_CODI = @EMP_CODI AND CON_CODI= @CON_CODI ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("EMP_CODI", emp_codi));
            parametros.Add(new SQLParams("CON_DISP", con_disp));
            parametros.Add(new SQLParams("CON_CODI", con_codi));
            return new DbConnection().Update(sql.ToString(), parametros);
        
        }
    }
}
