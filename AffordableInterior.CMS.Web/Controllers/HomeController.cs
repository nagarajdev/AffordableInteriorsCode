using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoySol.CMS.ContentServices;
using RoySol.CMS.ContentServices.Contracts;

namespace AffordableInterior.CMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// ContentService
        /// </summary>
        public IContentSerivecsDataContract _ContentService = null;

        /// <summary>
        /// HomeController constructor
        /// </summary>
        /// <param name="service"></param>
        public HomeController(IContentSerivecsDataContract services)
        {
            this._ContentService = services;

        }


        public ActionResult Index()
        {

            return View();
        }

    }
}
