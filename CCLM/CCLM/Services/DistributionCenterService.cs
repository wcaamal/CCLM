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
                return Entities.distribution_centers.Select(x => new DistributionCenter
                {
                    Id = x.id,
                    Code = x.code.Value,
                    Name = x.name,
                    ProcessEndTime = x.process_end_time.Value,
                    ProcessStartTime = x.process_start_time.Value,
                    BinnacleDays = x.binnacle_days.Value,
                    MailSuccess = x.mail_success.Value
                }).ToList();
            }
        }

        internal void Create(DistributionCenter model)
        {
            using (var Entities = new CCLMEntities())
            {

                Entities.distribution_centers.Add(new distribution_centers
                {
                    code = model.Code,
                    name = model.Name,
                    process_start_time = model.ProcessStartTime,
                    process_end_time = model.ProcessEndTime,
                    binnacle_days = model.BinnacleDays,
                    mail_success = model.MailSuccess
                });
                Entities.SaveChanges();

            }
        }

        internal DistributionCenter Get(int? Id)
        {
            using (var Entities = new CCLMEntities())
            {
                var dsEntity = Entities.distribution_centers.FirstOrDefault(x => x.id == Id.Value);
                if (dsEntity == null)
                    throw new Exception("Sitio no encontrado");
                return new DistributionCenter
                {
                    Id = dsEntity.id,
                    Code = dsEntity.code.Value,
                    Name = dsEntity.name,
                    ProcessStartTime = dsEntity.process_start_time.Value,
                    ProcessEndTime = dsEntity.process_end_time.Value,
                    BinnacleDays = dsEntity.binnacle_days.Value,
                    MailSuccess = dsEntity.mail_success.Value
                };
            }
        }

        internal void Update(int Id, DistributionCenter model)
        {
            using (var Entities = new CCLMEntities())
            {

                var dsEntity = Entities.distribution_centers.FirstOrDefault(x => x.id == Id);
                if (dsEntity == null)
                    throw new Exception("Sitio no encontrado");

                dsEntity.name = model.Name;
                dsEntity.process_start_time=model.ProcessStartTime;
                dsEntity.process_end_time=model.ProcessEndTime;
                dsEntity.binnacle_days=model.BinnacleDays;
                dsEntity.mail_success = model.MailSuccess;

                Entities.SaveChanges();

            }
        }
    }
}