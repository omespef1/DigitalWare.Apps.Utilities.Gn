using Digitalware.Apps.Utilities;
using DigitalWare.Apps.Utilities.Gn.DAO;
using DigitalWare.Apps.Utilities.Gn.TO;
using DWTools;
using SevenFramework.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.BO
{
    public class BOGnUsuar
    {

        public TOTransaction GnUsuarAutenticate(string usu_codi, string usu_idpk)
        {

            try
            {
                usu_idpk = Encrypta.EncriptarClave(usu_idpk);
                Gn_Usuar usuar = new DAO_Gn_Usuar().GetGnUsuar(usu_codi, usu_idpk);
                if (usuar == null)
                    return new SevenFramework.TO.TOTransaction() { Retorno = 1, TxtError = "Autenticación inválida" };
                return new TOTransaction() { Retorno = 0, TxtError = "" };
            }
            catch (Exception ex)
            {
                return new TOTransaction() { TxtError = ex.Message,Retorno=1 };
            }

        }

        public TOTransaction<Gn_Usuar> GetGnUsuar(string usu_codi)
        {

            try
            {             
                Gn_Usuar usuar = new DAO_Gn_Usuar().GetGnUsuar(usu_codi);
                if (usuar == null)
                    return new SevenFramework.TO.TOTransaction<Gn_Usuar>() { Retorno = 1, TxtError = "No se encontraron usuarios",ObjTransaction=null };
                return new TOTransaction<Gn_Usuar>() { Retorno = 0, TxtError = "", ObjTransaction=usuar};
            }
            catch (Exception ex)
            {
                return new TOTransaction<Gn_Usuar>() { TxtError = ex.Message, Retorno = 1 ,ObjTransaction=null};
            }

        }

        public bool containsLetters(string chars)
        {
           
            foreach(char c in chars)
            {
                if (char.IsLetter(c))
                    return true;
            }
            return false;

        }
        public TOTransaction SetNewPassword(string usu_codi, string usu_idpk)
        {
            try
            {

                var insta =  DAO_Gn_Insta.GetGnInsta();
                if (insta.par_ccln == "S")
                {
                    if (!containsLetters(usu_idpk))
                        throw new Exception("Su clave debe contener letras y números");
                    if(!usu_idpk.Any(char.IsDigit))
                        throw new Exception("Su clave debe contener letras y números");
                }
                if(usu_idpk.Length< insta.par_nmin)
                    throw new Exception(string.Format( "Su clave debe contener al menos {0} caracteres",insta.par_nmin));
                if (usu_idpk.Length > insta.par_nmax)
                    throw new Exception(string.Format("Su clave debe contener máximo {0} caracteres", insta.par_nmax));

                usu_idpk = Encrypta.EncriptarClave(usu_idpk);
                int update = new DAO_Gn_Usuar().updatePassword(usu_codi, usu_idpk);
                if (update == 0)
                    throw new Exception("No se actualizó ningún registro");
                return new TOTransaction() { Retorno = 0, TxtError = "" };
            }
            catch (Exception ex)
            {
                return new TOTransaction() { Retorno = 1, TxtError = ex.Message };
            }
        }

        public void SendMailForgetPasswordToUser(string usu_codi,string token,string link)
        {       
           

            Gn_Insta insta = DAO_Gn_Insta.GetGnInsta();
            if (insta == null)
                throw new Exception("Parámetros de instalación no definidos");
           
            var transact = GetGnUsuar(usu_codi);
            if (transact.Retorno == 0)
            {
                if (!IsValidMail(transact.ObjTransaction.usu_corr))
                    throw new Exception("No ha sido posible enviar el código de verificación porque el email asociado al usuario no es válido");

                    //Crear cuerpo del correo

                    StringBuilder mail = new StringBuilder();
                mail.Append(string.Format("<b>Señor Usuario {0}</b> <br>",transact.ObjTransaction.usu_codi));
                mail.Append(string.Format("Usted ha iniciado un proceso de cambio de contraseña, el cual podrá continuar en el siguiente  <a href='{0}?token={1}' target='_blank'>link</a> ", link,token));
                mail.Append(string.Format("<br><b>Si el hipervínculo no funciona correctamente, copie y pegue la siguiente Url en su navegador:</b> <br> {0}?token={1}", link,token));               
                mail.Append("<br> Si no ha iniciado este proceso por favor ignore este mail");


                //Enviar mail
                List<string> mails = new List<string>()
                {
                    transact.ObjTransaction.usu_corr
                };
                var envio =  MailHelper.SendMail(mails.ToArray(), "CAMBIO DE CONTRASEÑA SELFSERVICE", mail.ToString(), insta.par_mail, insta.par_host, insta.par_mail, insta.par_clma, int.Parse( insta.par_pcor), insta.par_ussl == "S", true);
            
                //Retornar mail al cual  se envió el token
            }
            else
            {
                throw new Exception(string.Format("El usuario {0} no existe", usu_codi));
            }

        }


        public bool IsValidMail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }



    }





}
