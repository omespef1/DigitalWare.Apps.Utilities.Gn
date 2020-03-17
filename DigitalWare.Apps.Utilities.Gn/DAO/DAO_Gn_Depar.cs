using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
   public class DAO_Gn_Depar
    {
        public List<Gn_Depar> GetGnDepar(int emp_codi,int pai_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT DEP_CODI, DEP_NOMB FROM GN_DEPAR WHERE EMP_CODI = @EMP_CODI AND PAI_CODI= @PAI_CODI");
            List<SQLParams> sQLParams = new List<SQLParams>();
            sQLParams.Add(new SQLParams("EMP_CODI", emp_codi));
            sQLParams.Add(new SQLParams("PAI_CODI", pai_codi));
            return new DbConnection().GetList<Gn_Depar>(sql.ToString(), sQLParams);
        }
    }
}
