using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Param
    {
        public Gn_Param GetGnParam(int emp_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM GN_PARAM                                                                                    ");
            sql.Append("INNER JOIN GN_MONED ON GN_MONED.MON_CODI = GN_PARAM.MON_CODI    ");
            sql.Append("WHERE EMP_CODI = @EMP_CODI                                                                                ");
            List<SQLParams> sQLParams = new List<SQLParams>();
            sQLParams.Add(new SQLParams("EMP_CODI", emp_codi));
            return new DbConnection().Get<Gn_Param>(sql.ToString(), sQLParams);
        }
    }
}
