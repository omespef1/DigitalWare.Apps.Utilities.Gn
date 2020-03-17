using DigitalWare.Apps.Utilities.Gn.DAO;
using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.BO
{
  public  class BOGnDivpo
    {


        public TOTransaction<List<Gn_Divpo>> GetGnDivpo()
        {
            try
            {
                var result = new DAO_Gn_Divpo().GetGnDivpo();
                return new TOTransaction<List<Gn_Divpo>>() { ObjTransaction = result, Retorno = 0, TxtError = "" };
            }
            catch (Exception ex)
            {

                return new TOTransaction<List<Gn_Divpo>>() { ObjTransaction = null, TxtError = ex.Message, Retorno = 1 };
            }

        }
    }
}
