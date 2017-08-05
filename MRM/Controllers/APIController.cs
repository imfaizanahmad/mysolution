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


        /// <summary>
        /// Get Business Group Hierarchy
        /// </summary>
        /// <returns>
        /// It returns business group hierarchy
        /// </returns>
        /// <Written By>
        /// Faizan Ahmad
        /// </Written>
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

        /// <summary>
        /// Get SegmentHierarchy
        /// </summary>
        /// <returns>
        /// It returns segmnent hierarchy
        /// </returns>
        /// <Written By>
        /// Faizan Ahmad
        /// </Written>
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


        /// <summary>
        ///  Get MCampaign
        /// </summary>
        /// <param name="BusinessGroupId"></param>
        /// <param name="MasterCampaignId"></param>
        /// <param name="SegmentId"></param>
        /// <returns></returns>
        /// <Written By>
        /// Faizan Ahmad
        /// </Written>
        public JsonResult GetMCampaign(string BusinessGroupId,string MasterCampaignId,string SegmentId)
        {
          
            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                if (string.IsNullOrEmpty(BusinessGroupId) && BusinessGroupId != "" && string.IsNullOrEmpty(MasterCampaignId) && MasterCampaignId != "" && string.IsNullOrEmpty(SegmentId) && SegmentId != "")
                {
                    dropDownResponse.List = _masterCampaignServices.MasterCampaignTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                else
                {
                    if (BusinessGroupId == null || BusinessGroupId == "" || (IsNumeric(BusinessGroupId) == false)) { BusinessGroupId = null; }
                    if (MasterCampaignId == null || MasterCampaignId == "" || (IsNumeric(MasterCampaignId) == false)) { MasterCampaignId = null; }
                    if (SegmentId == null || SegmentId == "" || (IsNumeric(SegmentId) == false)) { SegmentId = null; }


                    dropDownResponse.List = _masterCampaignServices.GetMasterCampaignForApi().
                                              Where(x =>
                                               ((BusinessGroupId != null && x.BusinessGroups.Any(y => y.Id == Convert.ToInt32(BusinessGroupId)))
                                               && 
                                               ((MasterCampaignId != null && x.Id.Equals(MasterCampaignId))
                                               ||
                                               (SegmentId!=null && x.Segments.Any(s => s.Id==Convert.ToInt32(SegmentId)))))
                                               || 
                                               ((BusinessGroupId != null && x.BusinessGroups.Any(y => y.Id == Convert.ToInt32(BusinessGroupId)))
                                               ||
                                               (MasterCampaignId != null && x.Id.Equals(MasterCampaignId))
                                               ||
                                               (SegmentId != null && x.Segments.Any(s => s.Id == Convert.ToInt32(SegmentId))))
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


        /// <summary>
        /// Get CCampaign 
        /// </summary>
        /// <param name="MasterCampaignId"></param>
        /// <param name="ChildCampaignId"></param>
        /// <returns></returns>
        /// <Written By>
        /// Faizan Ahmad
        /// </Written>
        public JsonResult GetCCampaign(string MasterCampaignId, string ChildCampaignId)
        {

            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                if (string.IsNullOrEmpty(MasterCampaignId) && MasterCampaignId != "" && string.IsNullOrEmpty(ChildCampaignId) && ChildCampaignId != "")
                {
                    dropDownResponse.List = _childCampaignServices.ChildCampaignTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                else
                {
                    if (MasterCampaignId == null || MasterCampaignId == "" || (IsNumeric(MasterCampaignId) == false)) { MasterCampaignId = null; }
                    if (ChildCampaignId == null || ChildCampaignId == "" || (IsNumeric(ChildCampaignId) == false)) { ChildCampaignId = null; }
                    dropDownResponse.List = _childCampaignServices.ChildCampaignTable().Where(x => (MasterCampaignId != null && (x.MasterCampaigns.Id ==Convert.ToInt32(MasterCampaignId) && !string.IsNullOrEmpty(x.Name))) || (ChildCampaignId!=null && (x.Id == Convert.ToInt32(ChildCampaignId) && !string.IsNullOrEmpty(x.Name)))).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
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

        /// <summary>
        /// GetTCampaign
        /// </summary>
        /// <param name="MasterCampaignId"></param>
        /// <param name="ChildCampaignId"></param>
        /// <returns>
        /// It return Tactic campaign list
        /// </returns>
        /// <Written By>
        /// Faizan Ahmad
        /// </Written>
        public JsonResult GetTCampaign(string ChildCampaignId, string TacticId)
        {

            DropDownResponse dropDownResponse = new DropDownResponse();
            try
            {
                if (string.IsNullOrEmpty(ChildCampaignId) && ChildCampaignId != ""  && string.IsNullOrEmpty(TacticId) && TacticId != "")
                {
                    dropDownResponse.List = _tacticCampaignServices.TacticCampaignTable().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
                }
                else
                {
                    if (ChildCampaignId == null || ChildCampaignId == "" || (IsNumeric(ChildCampaignId) == false)) { ChildCampaignId = null; }
                    if (TacticId == null || TacticId == "" || (IsNumeric(TacticId) == false)) { TacticId = null; }
                    dropDownResponse.List = _tacticCampaignServices.TacticCampaignTable().Where(x => (ChildCampaignId!=null && (x.ChildCampaigns.Id == Convert.ToInt32(ChildCampaignId) && !string.IsNullOrEmpty(x.Name))) || (TacticId!=null && (x.Id == Convert.ToInt32(TacticId) && !string.IsNullOrEmpty(x.Name)))).Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
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


        /// <summary>
        /// GetCampaignBulk
        /// </summary>
        /// <param name="MCampaignId"></param>
        /// <param name="CCampaignId"></param>
        /// <param name="TCampaignId"></param>
        /// <param name="MCampaignDescription"></param>
        /// <param name="MCampaignBGId"></param>
        /// <param name="MCampaignBLId"></param>
        /// <param name="MCampaignSegmentId"></param>
        /// <param name="MCampaignIndustryId"></param>
        /// <param name="MCampaignManager"></param>
        /// <returns>
        /// This api provieds one huge list including Master,Child and Tactic campaign,
        /// this will allow ramp to create a table search function
        /// </returns>
        /// <Written By>
        /// Faizan Ahmad
        /// </Written>
        public JsonResult GetCampaignBulk(string MCampaignId, string CCampaignId, string TCampaignId, string MCampaignDescription, string MCampaignBGId, string MCampaignBLId, string MCampaignSegmentId, string MCampaignIndustryId, string MCampaignManager)
        {
            var IsSuccess = false;
            string Message = string.Empty;
            List<BulkMasterCampaign> allMasterAndChilds = new List<BulkMasterCampaign>();
            try
            {
                if (string.IsNullOrEmpty(MCampaignId) && MCampaignId!="" && string.IsNullOrEmpty(CCampaignId) && CCampaignId!="" && string.IsNullOrEmpty(TCampaignId) && TCampaignId!="" && string.IsNullOrEmpty(MCampaignDescription) && MCampaignDescription!="" && string.IsNullOrEmpty(MCampaignBGId) && MCampaignBGId!="" && string.IsNullOrEmpty(MCampaignBLId) && MCampaignBLId!="" && string.IsNullOrEmpty(MCampaignSegmentId) && MCampaignSegmentId!="" && string.IsNullOrEmpty(MCampaignIndustryId) && MCampaignIndustryId!="" && string.IsNullOrEmpty(MCampaignManager) && MCampaignManager!="")
                {
                    allMasterAndChilds = (from mc_ch in _masterCampaignServices.GetMasterandChildCampaignForApiTable()
                                          select
                                  new BulkMasterCampaign
                                  {
                                      MCId = mc_ch.Id,
                                      MCampaignDescription = mc_ch.CampaignDescription,
                                      BusinessGroupList = mc_ch.BusinessGroups.Select(x => new ApiBusinessGroup { Id = x.Id, Name = x.Name }).ToList(),
                                      BusinessLineList = mc_ch.BusinessLines.Select(x => new ApiBusinessLine { Id = x.Id, Name = x.Name }).ToList(),
                                      SegmentList = mc_ch.Segments.Select(x => new ApiSegment { Id = x.Id, Name = x.Name }).ToList(),
                                      IndustryList = mc_ch.Industries.Select(x => new ApiIndustry { Id = x.Id, Name = x.Name }).ToList(),
                                    //MCStartDate = String.Format("{0:dd MMM yyyy}", mc_ch.StartDate),
                                    //MCEndDate = String.Format("{0:dd MMM yyyy}", mc_ch.EndDate),
                                     MCStartDate = mc_ch.StartDate,
                                      MCEndDate = mc_ch.EndDate,
                                      MCampaignManager = mc_ch.CampaignManager,
                                      ChildCampaignList = mc_ch.ChildCampaigns.Select(X => new ApiChildCampaign
                                      {
                                          Id = X.Id,
                                          Name = X.Name,
                                          CampaignDescription = X.CampaignDescription,
                                          CampaignManager = X.CampaignManager,
                                          CampaignType = X.CampaignType,
                                        //StartDate = String.Format("{0:dd MMM yyyy}", X.StartDate),
                                        //EndDate = String.Format("{0:dd MMM yyyy}", X.EndDate),
                                          StartDate = X.StartDate,
                                          EndDate = X.EndDate,
                                          BusinessGroupList = X.BusinessGroups.Select(x => new ApiBusinessGroup { Id = x.Id, Name = x.Name }).ToList(),
                                          BusinessLineList = X.BusinessLines.Select(x => new ApiBusinessLine { Id = x.Id, Name = x.Name }).ToList(),
                                          SegmentList = X.Segments.Select(x => new ApiSegment { Id = x.Id, Name = x.Name }).ToList(),
                                          IndustryList = X.Industries.Select(x => new ApiIndustry { Id = x.Id, Name = x.Name }).ToList(),
                                      }).ToList(),

                                  }).ToList();

                    List<int> childIds = new List<int>();
                    foreach (var master in allMasterAndChilds)
                    {
                        childIds.AddRange(master.ChildCampaignList.Select(t => t.Id));
                    }

                    var allTactics = _tacticCampaignServices.TacticCampaignTable().Where(t => childIds.Contains(t.Id)).Select(X => new ApiTacticCampaign
                    {
                        Id = X.Id,
                        ParentChildId = X.ChildCampaigns.Id,
                        Name = X.Name,
                        CampaignDescription = X.TacticDescription,
                        TacticType = X.TacticType,
                        //StartDate = String.Format("{0:dd MMM yyyy}", X.StartDate),
                        //EndDate = String.Format("{0:dd MMM yyyy}", X.EndDate),
                        StartDate = X.StartDate,
                        EndDate = X.EndDate,
                        BusinessGroupList = X.BusinessGroups.Select(x => new ApiBusinessGroup { Id = x.Id, Name = x.Name }).ToList(),
                        BusinessLineList = X.BusinessLines.Select(x => new ApiBusinessLine { Id = x.Id, Name = x.Name }).ToList(),
                        SegmentList = X.Segments.Select(x => new ApiSegment { Id = x.Id, Name = x.Name }).ToList(),
                        IndustryList = X.Industries.Select(x => new ApiIndustry { Id = x.Id, Name = x.Name }).ToList(),
                    }).ToList();

                    foreach (var master in allMasterAndChilds)
                    {
                        foreach (var child in master.ChildCampaignList)
                        {
                            child.TacticList = allTactics.Where(t => t.ParentChildId == child.Id).ToList();
                        }
                    }
                    IsSuccess = true;
                    if (allMasterAndChilds.Count == 0)
                    {
                        Message = "Data is not available as per search!";
                    }
                }
                else
                {
                    if (MCampaignId==null || MCampaignId=="" || (IsNumeric(MCampaignId) == false)){ MCampaignId = null;}
                    if (MCampaignBGId==null || MCampaignBGId=="" || (IsNumeric(MCampaignBGId) == false)){ MCampaignBGId = null; }
                    if (MCampaignBLId==null || MCampaignBLId=="" || (IsNumeric(MCampaignBLId) == false)){ MCampaignBLId = null; }
                    if (MCampaignSegmentId==null || MCampaignSegmentId=="" || (IsNumeric(MCampaignSegmentId) == false)){ MCampaignSegmentId = null; }
                    if (MCampaignIndustryId==null || MCampaignIndustryId=="" || (IsNumeric(MCampaignIndustryId) == false)){ MCampaignIndustryId = null; }
                    if (CCampaignId==null || CCampaignId=="" || (IsNumeric(CCampaignId) == false)){ CCampaignId = null; }
                    if (TCampaignId == null || TCampaignId == "" || (IsNumeric(TCampaignId) == false)) { TCampaignId = null; }

                    allMasterAndChilds = (from mc_ch in _masterCampaignServices.GetMasterandChildCampaignForApiTable().ToList().
                                          Where(x =>
                                (MCampaignId!=null && x.Id.Equals(Convert.ToInt32(MCampaignId)))
                                ||
                                (MCampaignBGId!=null && x.BusinessGroups.Any(b => b.Id == Convert.ToInt32(MCampaignBGId)))
                                ||
                                (MCampaignBLId!=null && x.BusinessLines.Any(b => b.Id == Convert.ToInt32(MCampaignBLId)))
                                ||
                                (MCampaignSegmentId!=null && x.Segments.Any(b => b.Id == Convert.ToInt32(MCampaignSegmentId)))
                                ||
                                (MCampaignIndustryId!=null && x.Industries.Any(b => b.Id == Convert.ToInt32(MCampaignIndustryId)))
                                ||
                                (CCampaignId!=null && x.ChildCampaigns.Any(c => c.Id == Convert.ToInt32(CCampaignId)))
                                )
                                          select
                                          new BulkMasterCampaign
                                          {
                                              MCId = mc_ch.Id,
                                              MCampaignDescription = mc_ch.CampaignDescription,
                                              MCampaignManager = mc_ch.CampaignManager,
                                              BusinessGroupList = mc_ch.BusinessGroups.Select(x => new ApiBusinessGroup { Id = x.Id, Name = x.Name }).ToList(),
                                              BusinessLineList = mc_ch.BusinessLines.Select(x => new ApiBusinessLine { Id = x.Id, Name = x.Name }).ToList(),
                                              SegmentList = mc_ch.Segments.Select(x => new ApiSegment { Id = x.Id, Name = x.Name }).ToList(),
                                              IndustryList = mc_ch.Industries.Select(x => new ApiIndustry { Id = x.Id, Name = x.Name }).ToList(),
                                             //MCStartDate = String.Format("{0:dd MMM yyyy}", mc_ch.StartDate),
                                             //MCEndDate = String.Format("{0:dd MMM yyyy}", mc_ch.EndDate),
                                              MCStartDate = mc_ch.StartDate,
                                              MCEndDate = mc_ch.EndDate,
                                              ChildCampaignList = mc_ch.ChildCampaigns.Select(X => new ApiChildCampaign
                                              {
                                                  Id = X.Id,
                                                  Name = X.Name,
                                                  CampaignDescription = X.CampaignDescription,
                                                  CampaignManager = X.CampaignManager,
                                                  CampaignType = X.CampaignType,
                                                 //StartDate = String.Format("{0:dd MMM yyyy}", X.StartDate),
                                                 //EndDate = String.Format("{0:dd MMM yyyy}", X.EndDate),
                                                  StartDate = X.StartDate,
                                                  EndDate = X.EndDate,
                                                  BusinessGroupList = X.BusinessGroups.Select(x => new ApiBusinessGroup { Id = x.Id, Name = x.Name }).ToList(),
                                                  BusinessLineList = X.BusinessLines.Select(x => new ApiBusinessLine { Id = x.Id, Name = x.Name }).ToList(),
                                                  SegmentList = X.Segments.Select(x => new ApiSegment { Id = x.Id, Name = x.Name }).ToList(),
                                                  IndustryList = X.Industries.Select(x => new ApiIndustry { Id = x.Id, Name = x.Name }).ToList(),
                                              }).ToList(),
                                          }).ToList();
                    if (!string.IsNullOrEmpty(MCampaignDescription))
                    {
                        allMasterAndChilds = allMasterAndChilds.Where(x => x.MCampaignDescription.ToLower().Contains(MCampaignDescription.ToLower())).ToList();
                    }
                    if (!string.IsNullOrEmpty(MCampaignManager))
                    {
                        allMasterAndChilds = allMasterAndChilds.Where(x => x.MCampaignManager.ToLower().Contains(MCampaignManager.ToLower())).ToList();
                    }

                    List<int> childIds = new List<int>();
                    foreach (var master in allMasterAndChilds)
                    {
                        childIds.AddRange(master.ChildCampaignList.Select(t => t.Id));
                    }

                    var allTactics = _tacticCampaignServices.TacticCampaignTable().Where(t => childIds.Contains(t.Id)).Select(X => new ApiTacticCampaign
                    {
                        Id = X.Id,
                        ParentChildId = X.ChildCampaigns.Id,
                        Name = X.Name,
                        CampaignDescription = X.TacticDescription,
                        //StartDate = String.Format("{0:dd MMM yyyy}", X.StartDate),
                        //EndDate = String.Format("{0:dd MMM yyyy}", X.EndDate),
                        StartDate = X.StartDate,
                        EndDate = X.EndDate,
                        TacticType = X.TacticType,
                        BusinessGroupList = X.BusinessGroups.Select(x => new ApiBusinessGroup { Id = x.Id, Name = x.Name }).ToList(),
                        BusinessLineList = X.BusinessLines.Select(x => new ApiBusinessLine { Id = x.Id, Name = x.Name }).ToList(),
                        SegmentList = X.Segments.Select(x => new ApiSegment { Id = x.Id, Name = x.Name }).ToList(),
                        IndustryList = X.Industries.Select(x => new ApiIndustry { Id = x.Id, Name = x.Name }).ToList(),
                    }).ToList();

                    if (TCampaignId == null)
                    {
                        foreach (var master in allMasterAndChilds)
                        {
                            foreach (var child in master.ChildCampaignList)
                            {
                                child.TacticList = allTactics.Where(t => t.ParentChildId == child.Id).ToList();
                            }
                        }
                    }
                    else
                    {
                        foreach (var master in allMasterAndChilds)
                        {
                            foreach (var child in master.ChildCampaignList)
                            {
                                child.TacticList = allTactics.Where(t => t.ParentChildId == child.Id).ToList().
                                    Where(x => (TCampaignId != null && x.Id.Equals(Convert.ToInt32(TCampaignId)))).ToList();
                            }
                        }

                    }
                }
                IsSuccess = true;
                if (allMasterAndChilds.Count == 0)
                {
                    Message = "Data is not available as per search!";
                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = ex.Message;
            }
            if (IsSuccess == true && allMasterAndChilds.Count > 0)
                return Json(allMasterAndChilds, JsonRequestBehavior.AllowGet);
            else
                return Json(Message, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Business Group List
        /// </summary>
        /// <returns>
        /// It returns complete Business Group List
        /// </returns>
        /// <written by>
        /// Faizan Ahmad
        /// </written>
        public JsonResult GetBusinessGroupList()
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

        /// <summary>
        /// Business Line List
        /// </summary>
        /// <returns>
        /// It returns complete Business Line List
        /// </returns>
        /// <written by>
        /// Faizan Ahmad
        /// </written>
        public JsonResult GetBusinessLineList()
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

        /// <summary>
        /// Segment List
        /// </summary>
        /// <returns>
        /// It returns complete Segment List
        /// </returns>
        /// <written by>
        /// Faizan Ahmad
        /// </written>
        public JsonResult GetSegmentList()
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

        /// <summary>
        /// Industry List
        /// </summary>
        /// <returns>
        /// It returns complete Industry List
        /// </returns>
        /// <written by>
        /// Faizan Ahmad
        /// </written>
        public JsonResult GetIndustryList()
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


        /// <summary>
        /// It makes complete campaign hirarchy, when end date of tactic passed from current date
        /// </summary>
        /// <returns></returns>
        /// <written by>
        /// Faizan Ahmad
        /// </written>
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

        /// <summary>
        /// It makes draft status inactive or soft delete of any campaign, which is not seen since last one year
        /// </summary>
        /// <returns></returns>
        /// <written by>
        /// Faizan Ahmad
        /// </written>
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


        public bool IsNumeric(string parm)
        {
           var isNumeric = true;
            foreach (char c in parm)
            {
                if (!Char.IsNumber(c))
                {
                    isNumeric = false;
                    break;
                }
            }
            return isNumeric;
        }
    }
}
