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
    public class RALController : Controller
    {
        private readonly IRALServices _rALServices;
        private readonly IMapper _mapper;

        public RALController
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPartialStore(string sorting_order = "", string filter = "", int value = 0)
        {
            try
            {
                IEnumerable<StoreModel> store = new List<StoreModel>();

                StoreDto newStore = new StoreDto
                {
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                if (filter == "")
                {
                    store = _mapper.Map<IEnumerable<StoreDto>, IEnumerable<StoreModel>>
                        (_rALServices.GetStore(newStore)).Take(50);

                    return PartialView("~/Views/RAL/_PartialStore.cshtml", store);
                }
                else
                {
                    if (filter == "Store")
                    {
                        store = _mapper.Map<IEnumerable<StoreDto>, IEnumerable<StoreModel>>
                        (
                            _rALServices.GetStore(newStore)
                        ).Where(a => a.store >= value).OrderBy(n => n.store);

                        return PartialView("~/Views/RAL/_PartialStore.cshtml", store);
                    }
                    else if (filter == "Region")
                    {
                        store = _mapper.Map<IEnumerable<StoreDto>, IEnumerable<StoreModel>>
                        (
                            _rALServices.GetStore(newStore)
                        ).Where(a => a.region >= value).OrderBy(n => n.region);

                        return PartialView("~/Views/RAL/_PartialStore.cshtml", store);
                    }
                    else
                    {
                        store = _mapper.Map<IEnumerable<StoreDto>, IEnumerable<StoreModel>>
                        (
                            _rALServices.GetStore(newStore)
                        ).Where(a => a.district >= value).OrderBy(n => n.district);

                        return PartialView("~/Views/RAL/_PartialStore.cshtml", store);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
                throw;
            }
        }
        public ActionResult GetStoreByID(int value = 0)
        {
            try
            {
                IEnumerable<StoreModel> store = new List<StoreModel>();

                StoreDto newStore = new StoreDto
                {
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                store = _mapper.Map<IEnumerable<StoreDto>, IEnumerable<StoreModel>>
                (
                    _rALServices.GetStore(newStore)
                ).Where(a => a.store == value).OrderBy(n => n.store);


                return Json(store);
                   
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
                throw;
            }
        }
        public ActionResult GetVendorByID(int value = 0)
        {
            try
            {
                IEnumerable<VendorModel> vendor = new List<VendorModel>();

                VendorDto newVendor = new VendorDto
                {
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                vendor = _mapper.Map<IEnumerable<VendorDto>, IEnumerable<VendorModel>>
                (
                    _rALServices.GetVendor(newVendor)
                ).Where(a => a.vendorNumber == value).OrderBy(n => n.vendorNumber);


                return Json(vendor);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
                throw;
            }
        }
        public ActionResult GetPOByID(int value = 0)
        {
            try
            {
                IEnumerable<POModel> po = new List<POModel>();

                PODto newPO = new PODto
                {
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                po = _mapper.Map<IEnumerable<PODto>, IEnumerable<POModel>>
                (
                    _rALServices.PODataAll(newPO)
                ).Where(a => a.pONumber == value).OrderBy(n => n.pONumber);


                return Json(po);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
                throw;
            }
        }
        public ActionResult GetPartialVendor(string value = "")
        {
            try
            {
                IEnumerable<VendorModel> store = new List<VendorModel>();

                VendorDto newVendor = new VendorDto
                {
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                if (value == "")
                {
                    store = _mapper.Map<IEnumerable<VendorDto>, IEnumerable<VendorModel>>
                        (_rALServices.GetVendor(newVendor)).Take(50);

                    return PartialView("~/Views/RAL/_PartialVendor.cshtml", store);
                }
                else
                {
                    store = _mapper.Map<IEnumerable<VendorDto>, IEnumerable<VendorModel>>
                    (
                        _rALServices.GetVendor(newVendor)
                    ).Where(a => a.mnemonic.ToLower().Contains(value.ToLower().ToString()))
                    .OrderBy(n => n.mnemonic);

                    return PartialView("~/Views/RAL/_PartialVendor.cshtml", store);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
                throw;
            }
        }

        public ActionResult GetPartialPurchaseOrder(int value = 0)
        {
            try
            {
                IEnumerable<POModel> store = new List<POModel>();

                PODto newPO = new PODto
                {
                    login_dto = new LoginDto
                    {
                        servername = Session["servername"].ToString(),
                        username = Session["username"].ToString(),
                        password = Session["password"].ToString(),
                        dBname = Session["databasename"].ToString()
                    },
                };

                if (value == 0)
                {
                    store = _mapper.Map<IEnumerable<PODto>, IEnumerable<POModel>>
                        (_rALServices.PODataAll(newPO)).Take(50);

                    return PartialView("~/Views/RAL/_PartialPO.cshtml", store);
                }
                else
                {
                    store = _mapper.Map<IEnumerable<PODto>, IEnumerable<POModel>>
                    (
                        _rALServices.PODataAll(newPO)
                    ).Where(a => a.vendorNumber >= value).OrderBy(n => n.vendorNumber);

                    return PartialView("~/Views/RAL/_PartialPO.cshtml", store);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "System Error: " + ex.Message;
                throw;
            }
        }
    }
}
