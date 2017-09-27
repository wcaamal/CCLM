using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCLM.Models
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public List<DistributionCenter> DistributionCenters { get; set; }
    }
}