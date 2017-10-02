using CCLM.Models;
using CCLM.ORM;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
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

        internal User GetUserInformation_(string user_active)
        {
            return new User { Email = "wcaamal@gmail.com", FullName = "William Caamal" };
        }

        internal User GetUserInformation(string user_active)
        {
            string[] user_info = user_active.Split(new string[] { "\\" }, StringSplitOptions.None);
            string domain = "LDAP://" + user_info[0];
            string alias = user_info[1];
            string mail = "";
            string full_name = "";
            User user = new User();

            SearchResultCollection sResults = null;

            try
            {
                //modify this line to include your domain name
                string path = domain;
                //init a directory entry
                DirectoryEntry dEntry = new DirectoryEntry(path);

                //init a directory searcher
                DirectorySearcher dSearcher = new DirectorySearcher(dEntry);

                //This line applies a filter to the search specifying a username to search for
                //modify this line to specify a user name. if you want to search for all
                //users who start with k - set SearchString to "k"
                //dSearcher.Filter = "(&(objectClass=user))";
                dSearcher.Filter = "(&(objectClass=user)(samaccountname= " + alias + "*))";

                //perform search on active directory
                sResults = dSearcher.FindAll();

                //loop through results of search
                foreach (SearchResult searchResult in sResults)
                {
                    mail = GetProperty(searchResult, "mail");
                    full_name = GetProperty(searchResult, "cn");
                    if (mail.Length > 0 && full_name.Length > 0)
                    {
                        user.NickName = user_active;
                        user.Email = mail;
                        user.FullName = full_name;
                    }
                }
            }
            catch (InvalidOperationException iOe)
            {
                //
            }
            catch (NotSupportedException nSe)
            {
                //
            }
            finally
            {

                // dispose of objects used
                if (sResults != null)
                    sResults.Dispose();

            }

            return user;
        }

        public string GetProperty(SearchResult result, string property) //Meotodo para llenar el objeto con la información de Active Directory
        {
            if (result != null)
            {
                ResultPropertyValueCollection val = (ResultPropertyValueCollection)result.Properties[property][0];
                return val.ToString();
            }
            return null;
        }
    }
}