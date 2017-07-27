using MRM.Business.Services;
using MRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MRM.Controllers
{
    public class APIController : Controller
    {
        private MasterCampaignServices _masterCampaignServices = null;
        private ChildCampaignServices _childCampaignServices = null;
        private TacticCampaignServices _tacticCampaignServices = null;

        public APIController()
        {
            _masterCampaignServices = new MasterCampaignServices();
            _childCampaignServices = new ChildCampaignServices();
            _tacticCampaignServices = new TacticCampaignServices();
        }

        // GET: API
        public JsonResult MasterCampaignList(string key)
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    dropDownResponse.List = _masterCampaignServices.MasterCampaignTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                else
                {
                    dropDownResponse.List = _masterCampaignServices.MasterCampaignTable().Where(x => x.Name.Contains(key) && !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubCampaignList(int? masterId)
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                if (masterId == null || masterId <= 0)
                {
                    dropDownResponse.List = _childCampaignServices.ChildCampaignTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                else
                {
                    dropDownResponse.List = _childCampaignServices.ChildCampaignTable().Where(x => x.MasterCampaigns.Id == masterId && !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TacticCampaignList(int? subCampaignId)
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                if (subCampaignId == null || subCampaignId <= 0)
                {
                    dropDownResponse.List = _tacticCampaignServices.TacticCampaignTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                else
                {
                    dropDownResponse.List = _tacticCampaignServices.TacticCampaignTable().Where(x => x.ChildCampaigns.Id == subCampaignId && !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }

    }

    
}
