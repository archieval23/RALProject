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
    public class LoginController : Controller
    {
        private readonly IRALServices _rALServices;
        private readonly IMapper _mapper;

        public LoginController
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
        // GET: /Login/

        public ActionResult Index()
        {
            var login = new LoginModel();
            login.business_unit_List = _mapper.Map<IEnumerable<BusinessUnitDto>, IEnumerable<BusinessUnitModel>>
                    (_rALServices.BusinessUnitAll());

            return View(login);
        }


        public JsonResult Login(LoginModel model)
        {
            Guid reportId = Guid.NewGuid();
            var bu = _rALServices.BusinessUnitById(Convert.ToInt32(model.servername));
            
            Session["servername"] = bu.jda_ip_address;
            Session["username"] = model.username;
            Session["password"] = model.password;
            Session["databasename"] = bu.jda_library;
            Session["reportId"] = reportId;

            LoginDto newLogin = new LoginDto
            {
                servername = Session["servername"].ToString(),
                username = Session["username"].ToString(),
                password = Session["password"].ToString(),
                dBname = Session["databasename"].ToString()
            };

            if (_rALServices.GetLoginByConnectionString(newLogin) == true)
            {
                DisposeDependency();
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        responseText = "Success",
                        redirectToUrl = Url.Action("Index", "Home", new { dbname = bu.code + " - " + bu.jda_ip_address + " - " + bu.jda_library })
                    }
                };
            }
               
            else
            {
                DisposeDependency();
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        responseText = "Failed",
                        //responseText = "Success",
                        redirectToUrl = Url.Action("Index", "Login")
                        //redirectToUrl = Url.Action("Index", "Home", new { dbname = bu.code + " - " + bu.jda_ip_address + " - " + bu.jda_library })
                    }
                };
            }
                
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        private void DisposeDependency()
        {
            _rALServices.Dispose();
        }
    }
}
