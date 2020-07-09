using AutoMapper;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.IO;
using System.Web.Security;
using System.Configuration;

using RALProject.ApplicationService.DTOs;
using RALProject.ApplicationService.ServiceContract;
using RALProject.Common.Logger;
using RALProject.Web.ActionFilters;
using RALProject.Web.ViewModels;
using Newtonsoft.Json;

namespace RALProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRALServices _rALServices;
        private readonly IMapper _mapper;
        public HomeController
        (
            IRALServices rALServices,
            IMapper mapper
        )
        {
            if (rALServices == null) throw new ArgumentNullException("RALServices");
            if (mapper == null) throw new ArgumentNullException("Mapper");

            _rALServices = rALServices;
            _mapper = mapper; 
        }
        //
        // GET: /RAL/

        public ActionResult Index(string dbname)
        {
            Session["AppServerName"] = dbname;
            return View();
        }

    }
}
