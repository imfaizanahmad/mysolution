using MRM.Business.Services;
using MRM.Database.GenericUnitOfWork;
using MRM.Database.Model;
using MRM.Models;
using MRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace MRM.Controllers
{
    public class APIController : Controller
    {
        private MasterCampaignServices _masterCampaignServices = null;
        private ChildCampaignServices _childCampaignServices = null;
        private TacticCampaignServices _tacticCampaignServices = null;
        private BusinessGroupServices _businessGroupServices = null;
        private BusinessLineServices _businessLineServices = null;
        private SegmentServices _segmentServices = null;
        private IndustryServices _industryServices = null;

        public APIController()
        {
            _masterCampaignServices = new MasterCampaignServices();
            _childCampaignServices = new ChildCampaignServices();
            _tacticCampaignServices = new TacticCampaignServices();
            _businessGroupServices = new BusinessGroupServices();
            _businessLineServices = new BusinessLineServices();
            _segmentServices = new SegmentServices();
            _industryServices = new IndustryServices();
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


        //Get Business Group Hierarchy
        public JsonResult GetBGHierarchy()
        {
            BGHierarchy BGHierarchy = new BGHierarchy();
            try
            {

                BGHierarchy.BGHierarchyLst = (from bgroup in _businessGroupServices.GetBG()
                                     join bline in _businessLineServices.GetBusinessLine()
                                     on bgroup.Id equals bline.BusinessGroups.Id
                                     select
                                     new BGHierarchy.BGHierarchyList
                                     {
                                          BGId = bgroup.Id,
                                          BGName=bgroup.Name, 
                                          BLId=bline.Id,
                                          BLName =bline.Name
                                      }).ToList();

                BGHierarchy.IsSuccess = true;
            }
            catch (Exception ex)
            {
                BGHierarchy.IsSuccess = false;
            }
            return Json(BGHierarchy.BGHierarchyLst, JsonRequestBehavior.AllowGet);
        }

        //Get SegmentHierarchy
        public JsonResult GetSegmentHierarchy()
        {
            SegmentHierarchy SegmentHierarchy = new SegmentHierarchy();
            try
            {
                SegmentHierarchy.SegmentHierarchyLst = (from seg in _segmentServices.GetSegment()
                                                        join Industry in _industryServices.GetIndustry()
                                              on seg.Id equals Industry.Segments.Id
                                              select
                                              new SegmentHierarchy.SegmentHierarchyList
                                              {
                                                  SegmentId = seg.Id,
                                                  SegmentName = seg.Name,
                                                  IndustryId = Industry.Id,
                                                  IndustryName = Industry.Name
                                              }).ToList();

                SegmentHierarchy.IsSuccess = true;
            }
            catch (Exception ex)
            {
                SegmentHierarchy.IsSuccess = false;
            }
            return Json(SegmentHierarchy.SegmentHierarchyLst, JsonRequestBehavior.AllowGet);
        }

        //Get MCampaign
        public JsonResult MCampaign(int? BusinessGroupId,int?MasterId,int?SegmentId)
        {
          
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                if (BusinessGroupId==0 && MasterId == 0 && SegmentId==0)
                {
                    dropDownResponse.List = _masterCampaignServices.MasterCampaignTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                else
                {
                    dropDownResponse.List = _masterCampaignServices.GetMasterCampaignForApi().
                                              Where(x =>
                                               (x.Id != 0 && x.Id.Equals(MasterId))
                                               ||
                                               (x.BusinessGroups.Any(y=>y.Id == BusinessGroupId.Value))
                                               ||
                                               (x.Segments.Any(s => s.Id==SegmentId.Value))
                                              )
                                              .Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
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




        public JsonResult BusinessGroupList()
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                dropDownResponse.List = _businessGroupServices.BusinessGroupTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BusinessLineList()
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                dropDownResponse.List = _businessLineServices.BusinessLineTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SegmentList()
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                dropDownResponse.List =_segmentServices.SegmentTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndustryList()
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                dropDownResponse.List = _industryServices.IndustryTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CompleteAfterEndDatePass()
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                _tacticCampaignServices.CompleteAfterEndDatePass();
                dropDownResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                dropDownResponse.IsSuccess = false;
                dropDownResponse.Message = ex.Message;
            }
            return Json(dropDownResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteLastyearVisited()
        {
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                _tacticCampaignServices.DeleteLastyearVisited();
                _childCampaignServices.DeleteLastyearVisited();
                _masterCampaignServices.DeleteLastyearVisited();
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
