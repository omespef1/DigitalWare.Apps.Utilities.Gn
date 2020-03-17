using DigitalWare.Apps.Utilities.Gn.DAO;
using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.BO
{
   public class BOGnArbol
    {

        public string GenerateIDArbol(int emp_codi,int nar_codi,int tar_codi,string arb_padr)
        {
            try
            {
                string result = "";
                string max = "";
                 int levelLong =   int.Parse( new DAO_Gn_Narbo().GetLongLevel(emp_codi,nar_codi,tar_codi).ToString());
                List<Gn_Arbol> hijos = new DAO_Gn_Arbol().ObtenerHijos(emp_codi, tar_codi, arb_padr);
                if (hijos == null || !hijos.Any())
                  return  max = Utils.AddChractersToWord(arb_padr, levelLong - arb_padr.Length,'0', "right");
                else
                    max = hijos.OrderByDescending(x => x.arb_codi).FirstOrDefault().arb_codi;
                var initLength = max.Length;               
                int isLosingZeros = initLength -  double.Parse(max).ToString().Length;               
                if (initLength < levelLong)
                    max = Utils.AddChractersToWord(max.ToString(), levelLong, '0', "right");
                if (initLength > levelLong)
                    throw new Exception(string.Format("No se puede auto generar consecutivo de arbol. El arbol con consecutivo más grande tiene una longitud {1} mayor de la que requiere el nivel actual {0}", nar_codi, initLength));

                if (isLosingZeros > 0)
                    result = Utils.AddChractersToWord((double.Parse(max) + 1).ToString(), isLosingZeros, '0', "left");
                else
                    result = (double.Parse(max) + 1).ToString();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Error generando arb_codi, asegúrese de que todos códigos de arbol asociados a los hijos del arbol {0} sean numéricos. Error :{1}", arb_padr, ex.Message));
            }
        }
    }
}
