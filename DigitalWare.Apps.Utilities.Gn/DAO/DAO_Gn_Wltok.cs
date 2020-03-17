using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Wltok
    {

        public static int? insertGnPtows(Gn_Wltok param)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append("   INSERT INTO GN_WLTOK        ");
            sql.Append("   (                           ");
            sql.Append("   WLT_CONT,                   ");
            sql.Append("   WLT_TOKE,                   ");
            sql.Append("   PTO_NOMB,                   ");
            sql.Append("   AUD_UFAC,                   ");
            sql.Append("   AUD_USUA,                   ");
            sql.Append("   AUD_ESTA                    ");
            sql.Append("   )                           ");
            sql.Append("   VALUES                      ");
            sql.Append("   (                           ");
            sql.Append("   @WLT_CONT,                  ");
            sql.Append("   @WLT_TOKE,                  ");
            sql.Append("   @PTO_NOMB,                  ");
            sql.Append("   @AUD_UFAC,                  ");
            sql.Append("   @AUD_USUA,                  ");
            sql.Append("   @AUD_ESTA                   ");
            sql.Append("   )                           ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("WLT_CONT",param.wlt_cont.ToString()));
            parametros.Add(new SQLParams("WLT_TOKE",param.wlt_toke));
            parametros.Add(new SQLParams("PTO_NOMB",param.pto_nomb));
            parametros.Add(new SQLParams("AUD_UFAC",DateTime.Now));
            parametros.Add(new SQLParams("AUD_USUA",param.aud_usua));
            parametros.Add(new SQLParams("AUD_ESTA",param.aud_esta));

            return new DbConnection().Insert(sql.ToString(), false, parametros);
        }


        public int GetWltok(string token, string serviceName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("  SELECT COUNT(WLT.WLT_TOKE)                                                  ");
            sql.Append("  FROM GN_WLTOK WLT WHERE WLT.PTO_NOMB = @PTO_NOMB AND WLT.WLT_TOKE = @WLT_TOKE     ");
            List<SQLParams> sQLParams = new List<SQLParams>();
            sQLParams.Add(new SQLParams("PTO_NOMB", serviceName));
            sQLParams.Add(new SQLParams("WLT_TOKE", token));
            var result = new DbConnection().ExecuteScalar(sql.ToString(), sQLParams);
            return result == null ? 0 : (int)result;
        }

    }
}
