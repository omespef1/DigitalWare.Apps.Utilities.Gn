using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenFramework.DataBase;

namespace DigitalWare.Apps.Utilities.Gn.TO
{
  public  class Gn_Ptows
    {

        public string pto_guid { get; set; }
        [Key]
        public string pto_nomb { get; set; }
        public int pto_ttok { get; set; }
        public short pto_utok { get; set; }
        public DateTime aud_ufac { get; set; }
        public string aud_usua { get; set; }
        public string aud_esta { get; set; }
        [NotMapped]
        [TableDetail]       
        public List<Gn_Dptow> detalle { get; set; }
    }
}
