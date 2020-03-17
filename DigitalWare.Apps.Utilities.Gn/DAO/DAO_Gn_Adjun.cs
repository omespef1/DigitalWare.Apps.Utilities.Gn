using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
  public  class DAO_Gn_Adjun
    {
        public int? insertGnAdju(Gn_Adjun adjun)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO GN_ADJUN ");
            sql.Append(" (EMP_CODI,RAD_CONT,ADJ_CONT,ADJ_NOMB,AUD_ESTA,AUD_USUA,AUD_UFAC,ADJ_TIPO) ");
            sql.Append(" VALUES (@EMP_CODI,@RAD_CONT,@ADJ_CONT,@ADJ_NOMB,@AUD_ESTA,@AUD_USUA,@AUD_UFAC,@ADJ_TIPO) ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("EMP_CODI", adjun.emp_codi));
            parametros.Add(new SQLParams("RAD_CONT", adjun.rad_cont));
            parametros.Add(new SQLParams("ADJ_CONT", adjun.adj_cont));
            parametros.Add(new SQLParams("ADJ_NOMB", adjun.adj_nomb)); //pendiente          
            parametros.Add(new SQLParams("AUD_ESTA", "A"));
            parametros.Add(new SQLParams("AUD_USUA", adjun.aud_usua));
            parametros.Add(new SQLParams("AUD_UFAC", DateTime.Now));
            parametros.Add(new SQLParams("ADJ_TIPO", adjun.adj_tipo));           
            return new DbConnection().Insert(sql.ToString(),false,parametros);
        }

        public List<Gn_Adjun> GetAdjun(int emp_codi, int rad_cont)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM   GN_ADJUN ");
            sql.Append(" WHERE  EMP_CODI  = @EMP_CODI  AND RAD_CONT     = @RAD_CONT");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("EMP_CODI", emp_codi));
            parametros.Add(new SQLParams("RAD_CONT", rad_cont));            
            return new DbConnection().GetList<Gn_Adjun>(sql.ToString(), parametros);
        }
    }
}
