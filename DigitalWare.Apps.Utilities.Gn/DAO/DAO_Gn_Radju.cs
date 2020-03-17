using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
   public class DAO_Gn_Radju
    {
        /// <summary>
        /// Obtiene el registro encabezado de los adjuntos
        /// </summary>
        /// <param name="emp_codi">Código de empresa</param>
        /// <param name="rad_llav">Debe concatenarse el emp_codi y el consecutivo interno de la tabla</param>
        /// <param name="pro_codi">Código de programa</param>
        /// <returns></returns>
        public Gn_Radju GetGnRadju(int emp_codi, string rad_llav, string pro_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM GN_RADJU  WHERE PRO_CODI=@PRO_CODI AND RAD_LLAV=@RAD_LLAV AND EMP_CODI= @EMP_CODI ");
            List<SQLParams> sQLParams = new List<SQLParams>()
            {
                new SQLParams("EMP_CODI",emp_codi),
                new SQLParams("RAD_LLAV",rad_llav),
                new SQLParams("PRO_CODI",pro_codi)
            };
            return new DbConnection().Get<Gn_Radju>(sql.ToString(), sQLParams);
        }

        public int? InsertarGnRadju(Gn_Radju radju,string usu_codi)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO GN_RADJU ");
            sql.Append(" (AUD_ESTA,AUD_USUA,AUD_UFAC,EMP_CODI,RAD_CONT,PRO_CODI,RAD_LLAV ,RAD_TABL )");
            sql.Append(" VALUES(@AUD_ESTA,@AUD_USUA,@AUD_UFAC,@EMP_CODI,@RAD_CONT ,@PRO_CODI,@RAD_LLAV ,@RAD_TABL) ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("AUD_ESTA", "A"));
            parametros.Add(new SQLParams("AUD_USUA", "Seven"));
            parametros.Add(new SQLParams("AUD_UFAC", date));
            parametros.Add(new SQLParams("EMP_CODI", radju.emp_codi)); //pendiente          
            parametros.Add(new SQLParams("RAD_TABL", radju.rad_tabl));
            parametros.Add(new SQLParams("PRO_CODI", radju.pro_codi));
            parametros.Add(new SQLParams("RAD_LLAV", radju.rad_llav));
            parametros.Add(new SQLParams("RAD_CONT", radju.rad_cont));
            return new DbConnection().Insert(sql.ToString(), false, parametros);
        }
    }
}
