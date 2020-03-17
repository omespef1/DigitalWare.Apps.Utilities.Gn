using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Wslog
    {

        public int SetGnWslog(Gn_Wslog log)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("   INSERT INTO GN_WSLOG    ");
            sql.Append("   (                       ");         
            sql.Append("   WEB_GUID,               ");
            sql.Append("   WEB_LXML,               ");
            sql.Append("   WEB_TIPO,               ");
            sql.Append("   WEB_NOMB,               ");
            sql.Append("   WEB_METO,               ");
            sql.Append("   WEB_LOIP,               ");
            sql.Append("   AUD_USUA,               ");
            sql.Append("   AUD_UFAC,               ");
            sql.Append("   AUD_ESTA                ");
            sql.Append("   )                       ");
            sql.Append("   VALUES                  ");
            sql.Append("   (                       ");          
            sql.Append("   @WEB_GUID,              ");
            sql.Append("   @WEB_LXML,              ");
            sql.Append("   @WEB_TIPO,              ");
            sql.Append("   @WEB_NOMB,              ");
            sql.Append("   @WEB_METO,              ");
            sql.Append("   @WEB_LOIP,               ");
            sql.Append("   @AUD_USUA,              ");
            sql.Append("   @AUD_UFAC,              ");
            sql.Append("   @AUD_ESTA               ");
            sql.Append("   )                       ");
            List<SQLParams> sQLParams = new List<SQLParams>();          
            sQLParams.Add(new SQLParams("WEB_GUID", log.web_guid));
            sQLParams.Add(new SQLParams("WEB_LXML", log.web_lxml));
            sQLParams.Add(new SQLParams("WEB_TIPO", log.web_tipo));
            sQLParams.Add(new SQLParams("WEB_NOMB", log.web_nomb));
            sQLParams.Add(new SQLParams("WEB_METO", log.web_meto));
            sQLParams.Add(new SQLParams("AUD_USUA", log.aud_usua));
            sQLParams.Add(new SQLParams("AUD_UFAC", log.aud_ufac));
            sQLParams.Add(new SQLParams("AUD_ESTA", log.aud_esta));
            sQLParams.Add(new SQLParams("WEB_LOIP", log.web_loip));
            return new DbConnection().Insert(sql.ToString(), false, sQLParams);

        }
    }
}
