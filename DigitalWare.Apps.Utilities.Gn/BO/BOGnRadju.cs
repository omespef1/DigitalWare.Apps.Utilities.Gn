using Digitalware.Apps.Utilities;
using DigitalWare.Apps.Utilities.Gn.DAO;
using DigitalWare.Apps.Utilities.Gn.TO;
using DigitalWare.Apps.Utilities.Wf.DAO;
using DigitalWare.Apps.Utilities.Wf.TO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWare.Apps.Utilities.Gn.BO
{
   public class BOGnRadju
    {
        /// <summary>
        /// Función para subir un adjunto a seven
        /// </summary>
        /// <param name="emp_codi">Código de la empresa</param>
        /// <param name="pro_codi">Codigo del programa</param>
        /// <param name="cont">Consecutivo interno del registro</param>
        /// <param name="rad_tabl">Tabla del programa</param>
        /// <param name="file">arreglo de bytes que corresponden al archivo</param>
        /// <param name="name">Nombre del archivo</param>
        /// <param name="usu_codi">Usuario seven</param>
        /// <param name="type">Tipo de adjunto , Documentos programas Seven = S , Documentos Workflow = W </param>
        /// <returns></returns>
        /// 


        public  string usu_codi = ConfigurationManager.AppSettings["usuario"];
        public Boolean newGnAdjun(int emp_codi,string pro_codi, int cont,string rad_tabl,byte[] file, string name,string usu_codi,string type="S")
        {
            try
            {
                
                var insta = DAO_Gn_Insta.GetGnInsta();
                
                
                Stream stream = new MemoryStream(file);
                string fileName = "";
                switch (type)
                {
                    case "S":

                        /*consultar si ya existe algún registro para sacar el consecutivo, sino se busca consecutivo*/
                        int rad_cont = 0;
                        Gn_Radju rad = new DAO_Gn_Radju().GetGnRadju(emp_codi, string.Concat(emp_codi, cont), pro_codi);
                        if (rad != null)
                            rad_cont = rad.rad_cont;
                        else
                        {

                            var consec = new DAO_Gn_Conse().getGnConse(emp_codi);
                            if (consec == 0)
                                throw new Exception(string.Format(
                                        "No se encontró consecutivo para la empresa {0} y código consecutivo {1}",
                                        emp_codi, "325"));
                            rad_cont = consec + 1;
                        }


                        int? r = null;

                        if (rad == null)
                        {
                            //Primero guardamos en la tabla de adjuntos
                            r = new DAO_Gn_Radju().InsertarGnRadju(new Gn_Radju()
                            {
                                rad_cont = rad_cont,
                                rad_tabl = rad_tabl,
                                pro_codi = pro_codi,
                                rad_llav = string.Concat(emp_codi, cont),
                                emp_codi = emp_codi
                            }, usu_codi);
                            if (r == null)
                                throw new Exception("Ha ocurrido un error al guardar el archivo adjunto");

                            //Actualiza el gn conse
                            //new DAO_Gn_Conse().updateGnConse(rad_cont, 325, double.Parse(string.Concat(emp_codi, cont)));
                        }

                       

                        List<Gn_Adjun> adjunts = new DAO_Gn_Adjun().GetAdjun(emp_codi, rad_cont);
                        int adj_cont = adjunts == null || !adjunts.Any() ? 1 : adjunts.Max(o => o.adj_cont) + 1;
                        Gn_Adjun adjun = new Gn_Adjun();
                        adjun.rad_cont = rad_cont;
                        adjun.adj_cont = adj_cont;
                        adjun.adj_nomb = name;
                        adjun.adj_tipo = "A";
                        adjun.aud_usua = usu_codi;
                        adjun.emp_codi = emp_codi;
                        r = new DAO_Gn_Adjun().insertGnAdju(adjun);
                        if (r == null)
                            throw new Exception("Ha ocurrido un error al guardar el archivo adjunto");
                        fileName = string.Format("{0}_{1}_{2}_{3}", rad_tabl,
                      emp_codi, rad_cont, adj_cont);
                        //System.IO.FileStream fs = new System.IO.FileStream(pathFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        //fs.CopyTo(ms);

                        if (insta.par_adju == "F")
                        {
                            Digitalware.Apps.Utilities.TO.ActionResult result = FTPManager
                                .FileUpload(stream, rad_tabl + "/" + fileName);
                            if (!result.State)
                                throw new Exception(result.Message);
                        }
                        if (insta.par_adju == "B")
                        {
                            //Consultamos si hay adjuntos para obtener el adj_cont
                            List<Gn_AdJfi> adjfi = new DAO_Gn_AdJfi().GetAdjFi(emp_codi, rad_cont);
                            adj_cont = adjfi == null || !adjfi.Any() ? 1 : adjfi.Max(j => j.adj_cont + 1);

                            //byte[] fileAdj = FTPManager.DownloadFtp(pathFile, Path.GetFileNameWithoutExtension(pathFile));
                            //  string hex = Helpers.ByteArrayToString(files[i].data);
                            new DAO_Gn_AdJfi().insertGnAdjfi(new Gn_AdJfi { adj_cont = adj_cont, adj_file = file, rad_cont = rad_cont,emp_codi = emp_codi }, usu_codi);
                        }

                        return true;
                    case "W":
                        
                        int doc_cont = 1;
                        List<Wf_Cdocu> filesExists = new DAO_Wf_Cdocu().GetWfCdocu(emp_codi, cont);
                        if (filesExists != null && filesExists.Any())
                            doc_cont = filesExists.Max(o => o.doc_cont) + 1;

                        fileName = string.Format("{0}_{1}_{2}_{3}", emp_codi,
                        cont, doc_cont, name);
                        if (insta.par_adju == "F" || insta.par_adju == "B")
                        {

                            var newWfCdocu = new DAO_Wf_Cdocu().insertWfCdocu(new Wf_Cdocu()
                            {
                                cas_cont = cont,
                                doc_cont = doc_cont,
                                doc_desc = name,
                                emp_codi = emp_codi,
                                aud_usua = usu_codi,
                                doc_blob = file
                            });

                            Digitalware.Apps.Utilities.TO.ActionResult result = FTPManager
                                .FileUpload(stream, fileName, string.Format("WORKFLOW/{0}/{1}/WF_CDOCU/", emp_codi, DateTime.Now.Year));
                            if (!result.State)
                                throw new Exception(result.Message);
                        }

                        return true;
                    default:
                        return false;
                 
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception(string.Format("Error insertando adjunto:{0}", ex.Message));               
            }
        }

        public Boolean newGnAdjun(int emp_codi, string pro_codi, long cont, string rad_tabl, byte[] file, string name, string usu_codi, string type = "S")
        {
            try
            {
                var insta = DAO_Gn_Insta.GetGnInsta();
                Stream stream = new MemoryStream(file);
                string fileName = "";
                switch (type)
                {
                    case "S":

                        /*consultar si ya existe algún registro para sacar el consecutivo, sino se busca consecutivo*/
                        int rad_cont = 0;
                        Gn_Radju rad = new DAO_Gn_Radju().GetGnRadju(emp_codi, string.Concat(emp_codi, cont), pro_codi);
                        if (rad != null)
                            rad_cont = rad.rad_cont;
                        else
                        {

                            var consec = new DAO_Gn_Conse().getGnConse(emp_codi);
                            if (consec == 0)
                                throw new Exception(string.Format(
                                        "No se encontró consecutivo para la empresa {0} y código consecutivo {1}",
                                        emp_codi, "325"));
                            rad_cont = consec + 1;
                        }


                        int? r = null;

                        if (rad == null)
                        {
                            //Primero guardamos en la tabla de adjuntos
                            r = new DAO_Gn_Radju().InsertarGnRadju(new Gn_Radju()
                            {
                                rad_cont = rad_cont,
                                rad_tabl = rad_tabl,
                                pro_codi = pro_codi,
                                rad_llav =string.Concat(emp_codi, cont),
                                emp_codi = emp_codi
                            }, usu_codi);
                            if (r == null)
                                throw new Exception("Ha ocurrido un error al guardar el archivo adjunto");

                            //Actualiza el gn conse
                            new DAO_Gn_Conse().updateGnConse(rad_cont, 325, int.Parse(string.Concat(emp_codi, cont)));
                        }
                        
                        List<Gn_Adjun> adjunts = new DAO_Gn_Adjun().GetAdjun(emp_codi, rad_cont);
                        int adj_cont = adjunts == null || !adjunts.Any() ? 1 : adjunts.Max(o => o.adj_cont) + 1;
                        Gn_Adjun adjun = new Gn_Adjun();
                        adjun.rad_cont = rad_cont;
                        adjun.adj_cont = adj_cont;
                        adjun.adj_nomb = name;
                        adjun.adj_tipo = "A";
                        adjun.aud_usua = usu_codi;
                        adjun.emp_codi = emp_codi;
                        r = new DAO_Gn_Adjun().insertGnAdju(adjun);
                        if (r == null)
                            throw new Exception("Ha ocurrido un error al guardar el archivo adjunto");
                        fileName = string.Format("{0}_{1}_{2}_{3}", rad_tabl,
                      emp_codi, rad_cont, adj_cont);
                        //System.IO.FileStream fs = new System.IO.FileStream(pathFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        //fs.CopyTo(ms);

                        if (insta.par_adju == "F")
                        {
                            Digitalware.Apps.Utilities.TO.ActionResult result = FTPManager
                                .FileUpload(stream, rad_tabl + "/" + fileName);
                            if (!result.State)
                                throw new Exception(result.Message);
                        }
                        if (insta.par_adju == "B")
                        {
                            //Consultamos si hay adjuntos para obtener el adj_cont
                            List<Gn_AdJfi> adjfi = new DAO_Gn_AdJfi().GetAdjFi(emp_codi, rad_cont);
                            adj_cont = adjfi == null || !adjfi.Any() ? 1 : adjfi.Max(j => j.adj_cont + 1);

                            //byte[] fileAdj = FTPManager.DownloadFtp(pathFile, Path.GetFileNameWithoutExtension(pathFile));
                            //  string hex = Helpers.ByteArrayToString(files[i].data);
                            new DAO_Gn_AdJfi().insertGnAdjfi(new Gn_AdJfi { adj_cont = adj_cont, adj_file = file, rad_cont = rad_cont, emp_codi = emp_codi }, usu_codi);
                        }

                        return true;
                    case "W":
                        int doc_cont = 1;
                        List<Wf_Cdocu> filesExists = new DAO_Wf_Cdocu().GetWfCdocu(emp_codi, int.Parse(cont.ToString()));
                        if (filesExists != null && filesExists.Any())
                            doc_cont = filesExists.Max(o => o.doc_cont) + 1;

                        fileName = string.Format("{0}_{1}_{2}_{3}", emp_codi,
                        cont, doc_cont, name);
                        if (insta.par_adju == "F")
                        {

                            var newWfCdocu = new DAO_Wf_Cdocu().insertWfCdocu(new Wf_Cdocu()
                            {
                                cas_cont = int.Parse( cont.ToString()),
                                doc_cont = doc_cont,
                                doc_desc = name,
                                emp_codi = emp_codi,
                                aud_usua = usu_codi
                            });

                            Digitalware.Apps.Utilities.TO.ActionResult result = FTPManager
                                .FileUpload(stream, fileName, string.Format("WORKFLOW/{0}/{1}/WF_CDOCU/", emp_codi, DateTime.Now.Year));
                            if (!result.State)
                                throw new Exception(result.Message);
                        }
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error insertando adjunto:{0}", ex.Message));
            }
        }
    }
}
