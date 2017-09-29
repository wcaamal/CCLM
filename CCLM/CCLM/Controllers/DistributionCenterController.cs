using CCLM.Models;
using CCLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Http;

namespace CCLM.Controllers
{
    public class DistributionCenterController : Controller
    {
        public ActionResult Index()
        {
            var dsService = new DistributionCenterService();
            var ds = dsService.GetAll();
            return View(ds);
        }

        public ActionResult Create()
        {
            var model = new DistributionCenter();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DistributionCenter model)
        {

            var dsService = new DistributionCenterService();
            dsService.Create(model);
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? Id)
        {
            var dsService = new DistributionCenterService();
            var model = dsService.Get(Id);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DistributionCenter model)
        {

            var dsService = new DistributionCenterService();
            dsService.Update(model.Id, model);

            return RedirectToAction("Index");
        }
    }
}
