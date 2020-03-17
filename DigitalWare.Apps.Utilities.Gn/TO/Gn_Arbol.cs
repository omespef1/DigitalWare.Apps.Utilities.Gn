using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.TO
{
  public  class Gn_Arbol
    {
        public int arb_cont { get; set; }
        public string arb_nomb { get; set; }
        public string arb_movi { get; set; }
        public string arb_codi { get; set; }

        public int emp_codi { get; set; }       
        public int tar_codi { get; set; }
        public int nar_codi { get; set; }
        public string arb_padr { get; set; }     
        public string arb_acti { get; set; }
        public string arb_coan { get; set; }
        public int cas_cont { get; set; }
        public string arb_cvaf { get; set; }
        public string arb_noco { get; set; }
        public string arb_cpan { get; set; }
      

    }


    public class GetGnArbolRequest
    {
        /// <summary>
        /// Código de empresa
        /// </summary>
       public int emp_codi { get; set; }
        /// <summary>
        /// Tipo de árbol
        /// </summary>
      public  int tar_codi { get; set; }
        /// <summary>
        /// Código anterior
        /// </summary>

      public  string arb_coan { get; set; }
    }

    public class GetGnArbolResponse
    {
        public int emp_codi { get; set; }
        public string arb_codi { get; set; }
        public string arb_nomb { get; set; }
        public string arb_coan { get; set; }
        public string arb_cpan { get; set; }
    }
}
