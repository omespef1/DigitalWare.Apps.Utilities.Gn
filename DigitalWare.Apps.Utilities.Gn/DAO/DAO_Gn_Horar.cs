using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Horar
    {

        public List<Gn_Horar> GetGnHorar(int cca_cont,int hor_dial)
        {
            StringBuilder sql = new StringBuilder();            
            sql.Append(" SELECT * FROM GN_HORAR  WHERE CCA_CONT=@CCA_CONT AND HOR_DIAL = @HOR_DIAL ");    
            List<SQLParams> sqlParams = new List<SQLParams>();
            sqlParams.Add(new SQLParams("CCA_CONT", cca_cont));
            sqlParams.Add(new SQLParams("HOR_DIAL", hor_dial));
            return new DbConnection().GetList<Gn_Horar>(sql.ToString(), sqlParams);

        }
    }
}
