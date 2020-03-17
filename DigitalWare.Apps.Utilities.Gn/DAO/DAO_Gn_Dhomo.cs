using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using SevenFramework.DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
  public  class DAO_Gn_Dhomo
    {

        public static List<Gn_Dhomo> GetGnDhomo(List<SQLParams> sQLParams)
        {
            StringBuilder sql = new StringBuilder();

              sql.Append("  SELECT GN_DHOMO.DHO_VHOM,                   ");
              sql.Append("  GN_DHOMO.DHO_NHOM,                          ");
              sql.Append("  GN_DHOMO.DHO_VAL1                           ");
              sql.Append("  FROM   GN_HOMOL                             ");
              sql.Append("  INNER JOIN GN_DHOMO                         ");
              sql.Append("  ON GN_DHOMO.EMP_CODI = GN_HOMOL.EMP_CODI    ");
              sql.Append("  AND GN_HOMOL.HOM_CONT = GN_DHOMO.HOM_CONT   ");
              sql.Append("  WHERE GN_HOMOL.EMP_CODI = @EMP_CODI         ");
              sql.Append("  AND GN_HOMOL.SIS_CODI = @SIS_CODI           ");
              sql.Append("  AND DSI_CODI = @DSI_CODI                      ");
            return new DbConnection().GetList<Gn_Dhomo>(sql.ToString(), sQLParams);
        }
    }
}
