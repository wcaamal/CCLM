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
                        IsFull = user.is_full.Value,
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
                var userEntity = Entities.users.FirstOrDefault(x => x.id == Id.Value);
                if (userEntity == null)
                    throw new Exception("Usuario no encontrado");
                return new User
                {
                    Id = userEntity.id,
                    Email = userEntity.email,
                    FullName = userEntity.full_name,
                    NickName = userEntity.nickname,
                    IsFull = userEntity.is_full.Value,
                    DistributionCentersSelected = userEntity.distribution_centers.Select(y => y.id).ToList(),
                    DistributionCentersAmount = userEntity.distribution_centers.Count(),
                    //DistributionCenters = userEntity.distribution_centers.Select(y => new DistributionCenter { Id = y.id, Code = y.code.Value, Name = y.name }).ToList()
                };
            }
        }

        internal void Update(int id, User model)
        {
            using(var Entities = new CCLMEntities())
            {
                
                var userEntity = Entities.users.FirstOrDefault(x => x.id == id);
                if (userEntity == null)
                    throw new Exception("Usuario no encontrado");

                var ids=userEntity.distribution_centers.Select(x => x.id).ToList();
                var oldDsEntities = Entities.distribution_centers.Where(x => ids.Contains(x.id));

                foreach (var ds in oldDsEntities)
                    userEntity.distribution_centers.Remove(ds);

                var newDsEntities = Entities.distribution_centers.Where(x => model.DistributionCentersSelected.Contains(x.id));
                foreach (var ds in newDsEntities)
                    userEntity.distribution_centers.Add(ds);

                Entities.SaveChanges();
                
            }
        }

        internal void Create(User model)
        {
            using (var Entities = new CCLMEntities())
            {

                Entities.users.Add(new users
                {
                    nickname = model.NickName,
                    full_name = model.FullName,
                    email = model.Email,
                    is_full = model.IsFull,
                    distribution_centers = GenerateDS(Entities, model.DistributionCentersSelected)
                });
                Entities.SaveChanges();

            }
        }

        private ICollection<distribution_centers> GenerateDS(CCLMEntities Entities, List<int> DSIds)
        {
            return Entities.distribution_centers.Where(x => DSIds.Contains(x.id)).ToList();
        }
    }
}