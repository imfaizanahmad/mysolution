﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.Business.Interfaces;
using MRM.Database.GenericUnitOfWork;
using MRM.ViewModel;

namespace MRM.Business.Services
{
  public class ChildCampaignServices : IChildCampaignServices
    {
        private GenericUnitOfWork guow = null;

        public ChildCampaignServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<ChildCampaign> GetChildCampaign()
        {
            IEnumerable<ChildCampaign> childCampaign = guow.GenericRepository<ChildCampaign>().GetAll().OrderByDescending(t => t.UpdatedDate).ToList();
            return childCampaign;
        }

        public List<ChildCampaign> GetChildCampaignById(ChildCampaignViewModel model)
        {
            List<ChildCampaign> childCampaign = guow.GenericRepository<ChildCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == model.Id).ToList();
            return childCampaign;
        }
        public List<ChildCampaign> GetChildCampaignByMasterId(int masterId)
        {
            var lstchildcampaign = new List<ChildCampaign>();

            var master = guow.GenericRepository<MasterCampaign>().GetByID(masterId);
            lstchildcampaign.AddRange(master.ChildCampaigns);
            return lstchildcampaign;

        }

        public List<ChildCampaign> GetDDLValuesByChildId(int childId)
        {
            List<ChildCampaign> childCampaign = guow.GenericRepository<ChildCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == childId).ToList();
            return childCampaign;
        }

        private void ModelToEntity(ChildCampaignViewModel model, ChildCampaign childCampaignEntity)
        {
            childCampaignEntity.MasterCampaigns = guow.GenericRepository<MasterCampaign>()
                .GetByID(model.MasterCampaignId);
            childCampaignEntity.Name = model.Name;
            childCampaignEntity.CampaignDescription = model.CampaignDescription;
            childCampaignEntity.CampaignManager = model.CampaignManager;
            childCampaignEntity.StartDate = model.StartDate;
            childCampaignEntity.EndDate = model.EndDate;
            childCampaignEntity.Status = model.Status;
            childCampaignEntity.CreatedBy = "user";
            childCampaignEntity.Budget = model.Budget;
            childCampaignEntity.Spend = model.Spend;
            childCampaignEntity.MarketingInfluenceLeads = model.MarketingInfluenceLeads;
            childCampaignEntity.MarketingGeneratedLeads = model.MarketingGeneratedLeads;
            childCampaignEntity.MarketingInfluenceOpportunity = model.MarketingInfluenceOpportunity;
            childCampaignEntity.MarketingGeneratedOpportunity = model.MarketingGeneratedOpportunity;
            childCampaignEntity.MILGoal = model.MILGoal;
            childCampaignEntity.MILLow = model.MILLow;
            childCampaignEntity.MILHigh = model.MILHigh;
            childCampaignEntity.MGLGoal = model.MGLGoal;
            childCampaignEntity.MGLLow = model.MGLLow;
            childCampaignEntity.MGLHigh = model.MGLHigh;
            childCampaignEntity.MIOGoal = model.MIOGoal;
            childCampaignEntity.MIOLow = model.MIOLow;
            childCampaignEntity.MIOHigh = model.MIOHigh;
            childCampaignEntity.MGOGoal = model.MGOGoal;
            childCampaignEntity.MGOLow = model.MGOLow;
            childCampaignEntity.MGOHigh = model.MGOHigh;
            childCampaignEntity.CampaignType = (model.CampaignTypes == CampaignType.BG_Led ? 0 : 1);
           

            List<BusinessLine> lstBline = null;
            List<BusinessGroup> lstBGroup = null;
            if (model.BusinessGroups_Id != null)
            {
                lstBGroup = new List<BusinessGroup>();
                foreach (var item in model.BusinessGroups_Id)
                {
                    var Bgroups = guow.GenericRepository<BusinessGroup>().GetByID(item);
                    lstBGroup.Add(Bgroups);
                }
                lstBline = new List<BusinessLine>();

                if (model.BusinessLines_Id != null)
                {
                    foreach (var item in model.BusinessLines_Id)
                    {
                        var Bline = guow.GenericRepository<BusinessLine>().GetByID(item);
                        lstBline.Add(Bline);
                    }
                }
            }


            List<Theme> lsttheme = null;
            if (model.Themes_Id != null)
            {
                lsttheme = new List<Theme>();
                foreach (var item in model.Themes_Id)
                {
                    var theme = guow.GenericRepository<Theme>().GetByID(item);
                    lsttheme.Add(theme);
                }
            }

            List<Industry> lstindustry = null;
            List<Segment> lstsegment = null;

            if (model.Segments_Id != null)
            {
                lstsegment = new List<Segment>();
                foreach (var item in model.Segments_Id)
                {
                    var segment = guow.GenericRepository<Segment>().GetByID(item);
                    lstsegment.Add(segment);
                }
                lstindustry = new List<Industry>();


                if (model.Industries_Id != null)
                {
                    foreach (var item in model.Industries_Id)
                    {
                        var industry = guow.GenericRepository<Industry>().GetByID(item);
                        lstindustry.Add(industry);
                    }
                }
            }

            List<Geography> lstgeography = null;
            if (model.Geographys_Id != null)
            {
                lstgeography = new List<Geography>();
                foreach (var item in model.Geographys_Id)
                {
                    var geography = guow.GenericRepository<Geography>().GetByID(item);
                    lstgeography.Add(geography);
                }
            }


            childCampaignEntity.BusinessGroups = lstBGroup;
            childCampaignEntity.Themes = lsttheme;
            childCampaignEntity.BusinessLines = lstBline;
            childCampaignEntity.Segments = lstsegment;
            childCampaignEntity.Industries = lstindustry;
            childCampaignEntity.Geographys = lstgeography;
            if (model.Id == 0)
            {
                childCampaignEntity.VisitedDate = DateTime.Now;
            }

        }

        public void Update(ChildCampaignViewModel model)
        {
            var childCamp = LoadChilCampaignEntity(model.Id);
            childCamp = FlushChildRecords(childCamp);
            ModelToEntity(model, childCamp);
            guow.GenericRepository<ChildCampaign>().Update(childCamp);
        }

        public bool InsertChildCampaign(ChildCampaignViewModel model)
        {
            var childCampaignEntity = new ChildCampaign();
            ModelToEntity(model, childCampaignEntity);
            guow.GenericRepository<ChildCampaign>().Insert(childCampaignEntity);
            return childCampaignEntity.Id != 0;
        }

        private ChildCampaign LoadChilCampaignEntity(int masterCampaignId)
        {
            return guow.GenericRepository<ChildCampaign>().GetByID(masterCampaignId);
        }

        public ChildCampaign Update(ChildCampaign childCampaign)
        {
            return guow.GenericRepository<ChildCampaign>().Update(childCampaign);
        }

        public bool DeleteMasterCampaign(int Id)
        {
            guow.GenericRepository<MasterCampaign>().Delete(Id);
            return true;

        }

       
        private ChildCampaign FlushChildRecords(ChildCampaign childCampaignCamp)
        {

            childCampaignCamp.Industries.Remove(childCampaignCamp.Industries.FirstOrDefault<Industry>());
            childCampaignCamp.Segments.Remove(childCampaignCamp.Segments.FirstOrDefault<Segment>());
            childCampaignCamp.Themes.Remove(childCampaignCamp.Themes.FirstOrDefault<Theme>());
            childCampaignCamp.Geographys.Remove(childCampaignCamp.Geographys.FirstOrDefault<Geography>());
            childCampaignCamp.BusinessLines.Remove(childCampaignCamp.BusinessLines.FirstOrDefault<BusinessLine>());
            childCampaignCamp.BusinessGroups.Remove(childCampaignCamp.BusinessGroups.FirstOrDefault<BusinessGroup>());
            return childCampaignCamp;
        }

        public bool DeleteSubCampaign(int Id)
        {
            guow.GenericRepository<ChildCampaign>().Delete(Id);
            return true;

        }

        //Deleted last visited
        public void DeleteLastyearVisited()
        {
            var ChildList = GetChildCampaign()
                .Where(s => s.Status == "Save Draft" && (s.VisitedDate <= DateTime.Now.AddYears(-1))).ToList();
            if (ChildList.Count > 0)
            {
                foreach (var item in ChildList)
                {
                    var childCampaign = GetChildCampaignById(new ChildCampaignViewModel() {Id = item.Id})
                        .FirstOrDefault();
                    childCampaign.IsActive = false;
                    Update(childCampaign);

                }
            }
        }

    }
}
