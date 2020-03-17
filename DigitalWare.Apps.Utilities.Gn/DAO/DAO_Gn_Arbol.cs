using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.DataBase;
using SevenFramework.DataBase.Utils;
using SevenFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.DAO
{
    public class DAO_Gn_Arbol
    {
        /// <summary>
        /// Consulta arboles, si el arb_cont es mayor a 0, el tipo de arbol es omitido.
        /// </summary>
        /// <param name="emp_codi">Código de empresa</param>
        /// <param name="tar_codi">Tipo de arbol</param>
        /// <param name="arb_codi">codigo de arbol</param>
        /// <param name="arb_cont">consecutivo interno arbol</param>
        /// <returns></returns>
        public Gn_Arbol GetGnArbol(int emp_codi, int tar_codi, string arb_codi = "", int arb_cont = 0)
        {
            List<SQLParams> sqlparams = new List<SQLParams>();
            sqlparams.Add(new SQLParams("EMP_CODI", emp_codi));
            if (!string.IsNullOrEmpty(arb_codi))
            {
                sqlparams.Add(new SQLParams("ARB_CODI", arb_codi));
                sqlparams.Add(new SQLParams("TAR_CODI", tar_codi));
            }
            if (arb_cont > 0)
                sqlparams.Add(new SQLParams("ARB_CONT", arb_cont));
            string sql = DBHelper.SelectQueryString<Gn_Arbol>(sqlparams);
            return new DbConnection().Get<Gn_Arbol>(sql, sqlparams);

        }
        /// <summary>
        /// Busca arbol por código anterior
        /// </summary>
        /// <param name="emp_codi"></param>
        /// <param name="tar_codi"></param>
        /// <param name="arb_coan"></param>
        /// <returns></returns>
        public Gn_Arbol GetGnArbolByArbCoan(int emp_codi, int tar_codi, string arb_coan)
        {
            List<SQLParams> sqlparams = new List<SQLParams>();
            sqlparams.Add(new SQLParams("EMP_CODI", emp_codi));
            if (!string.IsNullOrEmpty(arb_coan))
            {
                sqlparams.Add(new SQLParams("ARB_COAN", arb_coan));
                sqlparams.Add(new SQLParams("TAR_CODI", tar_codi));
            }
            string sql = DBHelper.SelectQueryString<Gn_Arbol>(sqlparams);
            return new DbConnection().Get<Gn_Arbol>(sql, sqlparams);

        }
        /// <summary>
        /// Busca cualquier codigo  de arbol, los parámetros enviados son los que se agregan a la clausua WHERE, por tanto deben de ser campos de la tabla GN_ARBOL
        /// </summary>
        /// <param name="emp_codi"></param>
        /// <param name="tar_codi"></param>
        /// <param name="arb_coan"></param>
        /// <returns></returns>
        public Gn_Arbol GetGnArbol(List<SQLParams> sqlparams)
        {
            string sql = DBHelper.SelectQueryString<Gn_Arbol>(sqlparams);
            return new DbConnection().Get<Gn_Arbol>(sql, sqlparams);

        }

        //public Gn_Arbol GetChilds(int emp_codi, int tar_codi, int arb_padr)
        //{
        //    List<SQLParams> sqlparams = new List<SQLParams>();
        //    sqlparams.Add(new SQLParams("EMP_CODI", emp_codi));
        //    if (string.IsNullOrEmpty(arb_codi))
        //    {
        //        sqlparams.Add(new SQLParams("ARB_CODI", arb_codi));
        //        sqlparams.Add(new SQLParams("TAR_CODI", tar_codi));
        //    }
        //    if (arb_cont > 0)
        //        sqlparams.Add(new SQLParams("ARB_CONT", arb_cont));
        //    string sql = DBHelper.SelectQueryString<Gn_Arbol>(sqlparams);
        //    return new DbConnection().Get<Gn_Arbol>(sql);
        //}

        public Gn_Arbol GetGnArbol(int emp_codi, int arb_cont)
        {
            try
            {
                List<SQLParams> sqlparams = new List<SQLParams>();
                sqlparams.Add(new SQLParams("EMP_CODI", emp_codi));
                sqlparams.Add(new SQLParams("ARB_CONT", arb_cont));
                string sql = DBHelper.SelectQueryString<Gn_Arbol>(sqlparams);
                return new DbConnection().Get<Gn_Arbol>(sql, sqlparams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Throw(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        public List<Gn_Arbol> ObtenerHijos(int emp_codi, int tar_codi, string arb_codi)
        {

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * ");
                sql.Append(" FROM GN_ARBOL ");
                sql.Append(" WHERE EMP_CODI = @P_EMP_CODI AND ARB_PADR = @P_ARB_PADR AND TAR_CODI = @P_TAR_CODI ");

                List<SQLParams> oParameter = new List<SQLParams>
                {
                    new SQLParams("P_EMP_CODI", emp_codi),
                    new SQLParams("P_TAR_CODI", tar_codi),
                    new SQLParams("P_ARB_PADR", arb_codi)
                };
                return new DbConnection().GetList<Gn_Arbol>(sql.ToString(), oParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.Throw("DAO_Gn_Arbol", "ObtenerHijos", ex);
                return null;
            }
        }

        public List<GetGnArbolResponse> GetGnArbol(GetGnArbolRequest arbol)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("  SELECT *                        ");
                sql.Append("  FROM   GN_ARBOL                 ");
                sql.Append("  WHERE  EMP_CODI = @EMP_CODI     ");
                sql.Append("  AND ARB_ACTI = 'A'              ");
                sql.Append("  AND TAR_CODI = @TAR_CODI  AND ARB_MOVI='S'       ");
                if (!string.IsNullOrEmpty(arbol.arb_coan) && arbol.arb_coan != "0")
                    sql.Append("  AND ARB_COAN = @ARB_COAN        ");

                List<SQLParams> oParameter = new List<SQLParams>
                {
                    new SQLParams("EMP_CODI", arbol.emp_codi),
                    new SQLParams("TAR_CODI", arbol.tar_codi),                    
                };

                if (!string.IsNullOrEmpty(arbol.arb_coan) && arbol.arb_coan!="0")
                    oParameter.Add(new SQLParams("ARB_COAN", arbol.arb_coan));
                    return new DbConnection().GetList<GetGnArbolResponse>(sql.ToString(), oParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.Throw("DAO_Gn_Arbol", "GetGnArbol", ex);
                return null;
            }
        }
    }
}
