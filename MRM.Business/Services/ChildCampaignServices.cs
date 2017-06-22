using System;
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
            IEnumerable<ChildCampaign> childCampaign = guow.GenericRepository<ChildCampaign>().GetAll().ToList();
            return childCampaign;
        }

        public bool CreateChildCampaign(ChildCampaignViewModel model)
        {
            ChildCampaign childCampaignEntity = new ChildCampaign();
            childCampaignEntity.MasterCampaigns = guow.GenericRepository<MasterCampaign>().GetByID(model.MasterCampaignId);
            childCampaignEntity.Name = model.Name;
            childCampaignEntity.CampaignDescription = model.CampaignDescription;
            childCampaignEntity.StartDate = Convert.ToDateTime(model.StartDate);
            childCampaignEntity.EndDate = Convert.ToDateTime(model.EndDate);
            childCampaignEntity.Status = model.Status;
            childCampaignEntity.CreatedBy = "user";
            childCampaignEntity.Budget = model.Budget;
            childCampaignEntity.Spend = model.Spend;
            childCampaignEntity.MarketingInfluenceLeads = model.MarketingInfluenceLeads;
            childCampaignEntity.MarketingGeneratedLeads = model.MarketingGeneratedLeads;
            childCampaignEntity.MarketingInfluenceOpportunity = model.MarketingInfluenceOpportunity;
            childCampaignEntity.MarketingGeneratedOpportunity = model.MarketingGeneratedOpportunity;

            List<BusinessGroup> lstBGroup = new List<BusinessGroup>();
            foreach (var item in model.BusinessGroups_Id)
            {
                var Bgroups = guow.GenericRepository<BusinessGroup>().GetByID(item);
                lstBGroup.Add(Bgroups);
            }
            List<Theme> lsttheme = new List<Theme>();
            foreach (var item in model.Themes_Id)
            {
                var theme = guow.GenericRepository<Theme>().GetByID(item);
                lsttheme.Add(theme);
            }
            List<BusinessLine> lstBline = new List<BusinessLine>();
            foreach (var item in model.BusinessLines_Id)
            {
                var Bline = guow.GenericRepository<BusinessLine>().GetByID(item);
                lstBline.Add(Bline);
            }

            List<Segment> lstsegment = new List<Segment>();
            foreach (var item in model.Segments_Id)
            {
                var segment = guow.GenericRepository<Segment>().GetByID(item);
                lstsegment.Add(segment);
            }

            List<Geography> lstgeography = new List<Geography>();
            foreach (var item in model.Geographys_Id)
            {
                var geography = guow.GenericRepository<Geography>().GetByID(item);
                lstgeography.Add(geography);
            }

            List<Industry> lstindustry = new List<Industry>();
            foreach (var item in model.Industries_Id)
            {
                var industry = guow.GenericRepository<Industry>().GetByID(item);
                lstindustry.Add(industry);
            }

            childCampaignEntity.BusinessGroups = lstBGroup;
            childCampaignEntity.Themes = lsttheme;
            childCampaignEntity.BusinessLines = lstBline;
            childCampaignEntity.Segments = lstsegment;
            childCampaignEntity.Industries = lstindustry;
            childCampaignEntity.Geographys = lstgeography;
           

            guow.GenericRepository<ChildCampaign>().Insert(childCampaignEntity);
            if (childCampaignEntity.Id != 0)
                return true;
            else
                return false;
        }

        public List<ChildCampaign> GetChildCampaignById(ChildCampaignViewModel model)
        {
            List<ChildCampaign> childCampaign = guow.GenericRepository<ChildCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == model.Id).ToList();
            return childCampaign;
        }

        //public List<ChildCampaign> GetChildCampaignMasterId(ChildCampaignViewModel model)
        //{
        //    List<ChildCampaign> childCampaign = guow.GenericRepository<ChildCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.MasterCampaigns_Id == model.MasterCampaigns_Id).ToList();
        //    return childCampaign;
        //}

    }
}
