using CCLM.Models;
using CCLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Http;



namespace CCLM.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var userService = new UserService();
            var users = userService.GetAll();
            return View(users);
        }

        public ActionResult Edit(int? Id)
        {
            var userService = new UserService();
            User user = userService.Get(Id);

            var dcService = new DistributionCenterService();
            var dcs = dcService.GetAll();
            IList<SelectListItem> listItems = dcs.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code + " - " + x.Name }).ToList();

            user.DistributionCentersSelect = new SelectList(listItems, "Value", "Text", String.Empty);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            var userService = new UserService();
            User user = userService.Get(model.Id);

            var dcService = new DistributionCenterService();
            var dcs = dcService.GetAll();
            IList<SelectListItem> listItems = dcs.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code + " - " + x.Name }).ToList();

            user.DistributionCentersSelect = new SelectList(listItems, "Value", "Text", String.Empty);
            return View(model);
        }
    }
}
