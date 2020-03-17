using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using SevenFramework.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
  public  class DAO_Gn_Paise
    {

        public List<Gn_Paise> GetGnPaise(int emp_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT PAI_CODI, PAI_NOMB FROM GN_PAISE WHERE EMP_CODI =@EMP_CODI ");
            List<SQLParams> sQLParams= new List<SQLParams>();
            sQLParams.Add(new SQLParams("EMP_CODI", emp_codi));
            return new DbConnection().GetList<Gn_Paise>(sql.ToString(), sQLParams);
            
        }


      
    }
}
