using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;

namespace DigitalWare.Apps.Utilities.Gn.TO
{
   public class Gn_Wslog
    {

       
        /// <summary>
        /// Guid : Identificador : Se usa para agrupar petición y respuesta
        /// </summary>
        public string web_guid { get; set; }
        /// <summary>
        /// Xml de la petición
        /// </summary>
        public string web_lxml { get; set; }
       
        
        /// <summary>
        /// Código de la transacción de seven
        /// </summary>
        public string web_tipo { get; set; }
        /// <summary>
        /// nombre del servicio web
        /// </summary>
        public string web_nomb { get; set; }
        /// <summary>
        /// Nombre del método que se está consumiendo
        /// </summary>
        public string web_meto { get; set; }
        /// <summary>
        /// Ip
        /// </summary>
        public string web_loip { get; set; }
        /// <summary>
        /// Usuario seven auditoría
        /// </summary>
        public string aud_usua { get; set; }
        /// <summary>
        /// Fecha de auditoría
        /// </summary>
        public DateTime aud_ufac { get; set; }
        /// <summary>
        /// Estado auditoría
        /// </summary>
        public string aud_esta { get; set; }
    }
}
