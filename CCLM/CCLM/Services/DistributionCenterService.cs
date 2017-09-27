using CCLM.Models;
using CCLM.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCLM.Services
{
    public class DistributionCenterService
    {
        internal List<DistributionCenter> GetAll()
        {
            using (var Entities = new CCLMEntities())
            {
                return Entities.distribution_centers.Select(x => new DistributionCenter { Id = x.id, Code = x.code.Value, Name = x.name }).ToList();
            }
        }
    }
}