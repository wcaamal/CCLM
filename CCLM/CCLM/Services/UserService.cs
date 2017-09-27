using CCLM.Models;
using CCLM.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCLM.Services
{
    public class UserService
    {
        internal List<User> GetAll()
        {
            using (var Entities = new CCLMEntities())
            {
                List<User> users = new List<User>();
                foreach(var user in Entities.users)
                {
                    users.Add(new User
                    {
                        Id = user.id,
                        Email = user.email,
                        FullName = user.full_name,
                        NickName = user.nickname,
                        DistributionCentersSelected = user.distribution_centers.Select(y => y.id).ToList(),
                        DistributionCentersAmount = user.distribution_centers.Count(),
                        //DistributionCenters=user.distribution_centers.Select(y => new DistributionCenter { Id = y.id, Code = y.code.Value, Name = y.name }).ToList()
                    });
                }
                return users;
            }
        }

        internal User Get(int? Id)
        {
            using (var Entities = new CCLMEntities())
            {
                var userEntity = Entities.users.Where(x => x.id == Id.Value).FirstOrDefault();
                if (userEntity == null)
                    throw new Exception("Usuario no encontrado");
                return new User
                {
                    Id = userEntity.id,
                    Email = userEntity.email,
                    FullName = userEntity.full_name,
                    NickName = userEntity.nickname,
                    DistributionCentersSelected = userEntity.distribution_centers.Select(y => y.id).ToList(),
                    DistributionCentersAmount = userEntity.distribution_centers.Count(),
                    //DistributionCenters = userEntity.distribution_centers.Select(y => new DistributionCenter { Id = y.id, Code = y.code.Value, Name = y.name }).ToList()
                };
            }
        }
    }
}