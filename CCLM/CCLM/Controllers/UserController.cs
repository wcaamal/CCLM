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
            userService.Update(model.Id, model);

            var dcService = new DistributionCenterService();
            var dcs = dcService.GetAll();
            IList<SelectListItem> listItems = dcs.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code + " - " + x.Name }).ToList();
            model.DistributionCentersSelect = new SelectList(listItems, "Value", "Text", String.Empty);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var model = new User();
            model.DistributionCentersSelected = new List<int>();
            var dcService = new DistributionCenterService();
            var dcs = dcService.GetAll();
            IList<SelectListItem> listItems = dcs.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code + " - " + x.Name }).ToList();
            model.DistributionCentersSelect = new SelectList(listItems, "Value", "Text", String.Empty);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(User model)
        {

            var userService = new UserService();
            userService.Create(model);
            var dcService = new DistributionCenterService();
            var dcs = dcService.GetAll();
            IList<SelectListItem> listItems = dcs.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code + " - " + x.Name }).ToList();
            model.DistributionCentersSelect = new SelectList(listItems, "Value", "Text", String.Empty);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetUserData(string nn)
        {
            var userService = new UserService();
            var user = userService.GetUserInformation(nn);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}
