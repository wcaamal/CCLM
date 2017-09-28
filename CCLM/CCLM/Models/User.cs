using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCLM.Models
{
    public class User
    {
        
        public int Id { get; set; }
        [Display(Name = "NickName")]
        public string NickName { get; set; }
        [Display(Name = "Nombre")]
        public string FullName { get; set; }

        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+(\.[^@\s]+)+$", ErrorMessage = "Correo Inválido")]
        public string Email { get; set; }

        [Display(Name="Admin")]
        public bool IsFull { get; set; }

        [Display(Name = "Cedis Asignados")]
        public int DistributionCentersAmount { get; set; }
        //public List<DistributionCenter> DistributionCenters { get; set; }

        [Required, Display(Name="Centros de Distribucion")]
        public List<int> DistributionCentersSelected { get; set; }

        public SelectList DistributionCentersSelect { get; set; }

    }
}