using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DontCoreApiAuthantaication.Data; 

namespace DontCoreApiAuthantaication.Controllers
{
    public class ConfirmController : Controller
    {
        private readonly UserContext _Context;

        public ConfirmController(UserContext context) {
            _Context = context;

        }
        [Route("Confirm/Confirm/{id}")]
        [HttpGet("{id}")]
        public ActionResult Confirm(User reginfo)
        {

            ViewBag.regID = reginfo.ID;
            return View();
        }
        [Route("Confirm/RegisterConfirm")]
        [HttpPost]
        public JsonResult RegisterConfirm(int RegID)
        {
            User user = _Context.Users.Where(X => X.ID == 38).FirstOrDefault();

            _Context.SaveChanges();
            var msg = "Your Email Is Verified!";
            return Json(msg);

        }


    }
}