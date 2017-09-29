using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCLM.Models
{
    public class DistributionCenter
    {
        public int Id { get; set; }
        [Display(Name="Codigo")]
        public int Code { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Inicio Proceso")]
        public TimeSpan ProcessStartTime { get; set; }
        [Display(Name = "Fin Proceso")]
        public TimeSpan ProcessEndTime { get; set; }
        [Display(Name = "Dias Bitacora")]
        public int BinnacleDays { get; set; }
    }
}