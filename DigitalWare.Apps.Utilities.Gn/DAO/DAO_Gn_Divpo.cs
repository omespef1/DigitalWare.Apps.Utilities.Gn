using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Divpo
    {

        public List<Gn_Divpo> GetGnDivpo()
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PAI.PAI_CODI,                        ");
            sql.Append("PAI.PAI_NOMB,                               ");
            sql.Append("DEP.DEP_CODI,                               ");
            sql.Append("DEP.DEP_NOMB,                               ");
            sql.Append("MUN.MUN_CODI,                               ");
            sql.Append("MUN.MUN_NOMB                                ");
            sql.Append("FROM   GN_PAISE PAI                         ");
            sql.Append("INNER JOIN GN_REGIO REG                     ");
            sql.Append("ON PAI.PAI_CODI = REG.PAI_CODI              ");
            sql.Append("INNER JOIN GN_DEPAR DEP                     ");
            sql.Append("ON PAI.PAI_CODI = REG.PAI_CODI              ");
            sql.Append("AND DEP.REG_CODI = REG.REG_CODI             ");
            sql.Append("INNER JOIN GN_MUNIC MUN                     ");
            sql.Append("ON DEP.PAI_CODI = MUN.PAI_CODI              ");
            sql.Append("AND DEP.REG_CODI = MUN.REG_CODI             ");
            sql.Append("AND DEP.DEP_CODI = MUN.DEP_CODI             ");

            return new DbConnection().GetList<Gn_Divpo>(sql.ToString());

        }
    }
}
