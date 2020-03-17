using DigitalWare.Apps.Utilities.Gn.DAO;
using DigitalWare.Apps.Utilities.Gn.TO;
using SevenFramework.TO;
using System;


namespace DigitalWare.Apps.Utilities.Gn.BO
{
  public  class BOGnPtows
    {

        private string serviceName { get; set; }
        DAO_Gn_Ptows doa = new DAO_Gn_Ptows();
        public BOGnPtows(string _serviceName)
        {
            serviceName = _serviceName;
        }

        public TOTransaction Authenticate (LoginRequest login)
        {

            try
            {
                if (login == null)
                    throw new Exception("Credenciales vacías.Verifique por favor.");                
                bool isCredentialValid = new DAO_Gn_Ptows().GetGn_Dptow(login.Username,login.Password, serviceName) !=null;
                if (!isCredentialValid)
                    throw new Exception("Credenciales no válidas");              
                return new TOTransaction() { Retorno = 0, TxtError = "" };
            }
            catch (Exception ex)
            {
                return new TOTransaction() { Retorno = 1, TxtError = ex.Message };             
            }
          
        }

        public Gn_Ptows GetPtows()
        {
            try
            {
                var result = new DAO_Gn_Ptows().GetGn_Ptows(serviceName);
                if (result == null)
                    throw new Exception("No se encontró parametrización del programa SGNPTOWS");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
