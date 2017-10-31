using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hwDAL;
namespace hwAPI.Controllers
{
    public class hwController : Controller
    {
        [HttpGet]
        public JsonResult GetMessage()
        {
            hwDataAccess dal = new hwDataAccess();
            string message = dal.GetMessage();
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
