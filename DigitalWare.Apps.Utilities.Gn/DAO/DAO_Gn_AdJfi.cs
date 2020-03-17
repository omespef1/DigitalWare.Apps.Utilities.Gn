using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
   public class DAO_Gn_AdJfi
    {

        public List<Gn_AdJfi> GetAdjFi(int emp_codi, int rad_cont)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM   GN_ADJFI ");
            sql.Append("WHERE  EMP_CODI = @EMP_CODI AND RAD_CONT  = @RAD_CONT ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("EMP_CODI ", emp_codi));
            parametros.Add(new SQLParams("RAD_CONT", rad_cont));          
            return new DbConnection().GetList<Gn_AdJfi>( sql.ToString(),parametros);
        }

        public int insertGnAdjfi(Gn_AdJfi adjfi,string usu_codi)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO GN_ADJFI( ");
            sql.Append(" EMP_CODI,RAD_CONT,ADJ_CONT,ADJ_FILE,AUD_ESTA,AUD_USUA,AUD_UFAC)");
            sql.Append(" VALUES( ");
            sql.Append(" @EMP_CODI,@RAD_CONT,@ADJ_CONT,@ADJ_FILE,@AUD_ESTA,@AUD_USUA,@AUD_UFAC) ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("EMP_CODI", adjfi.emp_codi));
            parametros.Add(new SQLParams("RAD_CONT", adjfi.rad_cont));
            parametros.Add(new SQLParams("ADJ_CONT", adjfi.adj_cont));
            parametros.Add(new SQLParams("ADJ_FILE", adjfi.adj_file)); //pendiente          
            parametros.Add(new SQLParams("AUD_ESTA", "A"));
            parametros.Add(new SQLParams("AUD_USUA", usu_codi));
            parametros.Add(new SQLParams("AUD_UFAC", DateTime.Now));           
            return new DbConnection().Insert(sql.ToString(),false, parametros);
        }
    }
}
