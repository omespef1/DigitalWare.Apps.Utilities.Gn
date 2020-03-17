using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Diasn
    {

        public Gn_Diasn GetGnDiasn(DateTime dia_notr, string dia_tipo, int cca_cont)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("   SELECT *                                                        ");
            sql.Append("   FROM   GN_DIASN G                                               ");
            sql.Append("   WHERE G.CCA_CONT = @CCA_CONT                                           ");
            sql.Append("   AND CONVERT(NVARCHAR, G.DIA_NOTR, 103) = @DIA_NOTR  ");
            sql.Append("   AND G.DIA_TIPO = @DIA_TIPO                                           ");
            List<SQLParams> sQLParams = new List<SQLParams>();
            sQLParams.Add(new SQLParams("CCA_CONT", cca_cont));
            sQLParams.Add(new SQLParams("DIA_TIPO", dia_tipo));
            sQLParams.Add(new SQLParams("DIA_NOTR", dia_notr.ToString("dd/MM/yyyy")));
            return new DbConnection().Get<Gn_Diasn>(sql.ToString(),sQLParams);
        }

        public List<Gn_Diasn> GetGnDiasn(int cca_cont)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM GN_DIASN  WHERE CCA_CONT=@CCA_CONT ");
            List<SQLParams> sQLParams = new List<SQLParams>();
            sQLParams.Add(new SQLParams("CCA_CONT", cca_cont));
            return new DbConnection().GetList<Gn_Diasn>(sql.ToString(), sQLParams);
        }
    }
}
