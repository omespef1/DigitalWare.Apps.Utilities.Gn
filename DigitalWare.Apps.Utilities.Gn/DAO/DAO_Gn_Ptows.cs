using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Ptows
    {

        public int? insertGnPtows(Gn_Ptows param)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append("   INSERT INTO GN_PTOWS        ");
            sql.Append("   (                           ");
            sql.Append("   PTO_GUID,                   ");
            sql.Append("   PTO_NOMB,                   ");
            sql.Append("   PTO_TTOK,                   ");
            sql.Append("   PTO_UTOK,                   ");
            sql.Append("   AUD_UFAC,                   ");
            sql.Append("   AUD_USUA,                   ");
            sql.Append("   AUD_ESTA                    ");
            sql.Append("   )                           ");
            sql.Append("   VALUES                      ");
            sql.Append("   (                           ");
            sql.Append("   @PTO_GUID,                  ");
            sql.Append("   @PTO_NOMB,                  ");
            sql.Append("   @PTO_TTOK,                  ");
            sql.Append("   @PTO_UTOK,                  ");
            sql.Append("   @AUD_UFAC,                  ");
            sql.Append("   @AUD_USUA,                  ");
            sql.Append("   @AUD_ESTA                   ");
            sql.Append("   )                           ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("PTO_GUID", param.pto_guid));
            parametros.Add(new SQLParams("PTO_NOMB", param.pto_nomb));
            parametros.Add(new SQLParams("PTO_TTOK", param.pto_ttok));
            parametros.Add(new SQLParams("PTO_UTOK", param.pto_utok)); //pendiente          
            parametros.Add(new SQLParams("AUD_UFAC", "A"));
            parametros.Add(new SQLParams("AUD_USUA", param.aud_usua));
            parametros.Add(new SQLParams("AUD_ESTA", DateTime.Now));
            return new DbConnection().Insert(sql.ToString(), false, parametros);
        }


       

        public Gn_Ptows GetGn_Ptows(string pto_nomb)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT GP.PTO_GUID, GP.PTO_NOMB, GP.PTO_TTOK, GP.PTO_UTOK FROM GN_PTOWS GP WHERE PTO_NOMB = @PTO_NOMB");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("PTO_NOMB", pto_nomb));
            return new DbConnection().Get<Gn_Ptows>(sql.ToString(), parametros);
        }

        public List<Gn_Ptows> GetGn_Dptow(string dpt_usua,string dpt_pass,string pto_nomb)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT DPT.DPT_CLIE,                             ");              
            sql.Append("DPT.DTP_USUA,                                    ");
            sql.Append("DPT.DTP_PASS,                                     ");
            sql.Append("PTO.PTO_NOMB ,PTO.PTO_GUID ,PTO.PTO_TTOK, PTO.PTO_UTOK                                   ");
            sql.Append("FROM   GN_PTOWS PTO                              ");
            sql.Append("INNER JOIN GN_DPTOW DPT                          ");
            sql.Append("ON PTO.PTO_NOMB = DPT.PTO_NOMB                   ");
            sql.Append("WHERE PTO.PTO_NOMB = @PTO_NOMB  AND DPT.DTP_USUA = @DTP_USUA AND DPT.DTP_PASS = @DTP_PASS    ");
            List<SQLParams> parametros = new List<SQLParams>();
            parametros.Add(new SQLParams("DTP_USUA", dpt_usua));
            parametros.Add(new SQLParams("DTP_PASS", dpt_pass));
            parametros.Add(new SQLParams("PTO_NOMB", pto_nomb));
            return new DbConnection().GetMasterDetail<Gn_Ptows, Gn_Dptow>(sql.ToString(), parametros);
        }
    }
}
