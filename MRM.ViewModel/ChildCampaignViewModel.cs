﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;

namespace MRM.ViewModel
{

    public class ChildCampaignViewModel
    {
        public ChildCampaignViewModel()
        {
            BusinessGroupViewModels = new [] { new BusinessGroup() };
            SegmentViewModels = (new [] { new Segment() });
            BusinessLineViewModels = (new [] { new BusinessLine() });
            ThemeViewModels = (new [] { new Theme() });
            GeographyViewModels = (new [] { new Geography() });
            IndustryViewModels = (new [] { new Industry() });
            StartDate = DateTime.Now.ToString("MM/dd/yyyy");
            EndDate = DateTime.Now.ToString("MM/dd/yyyy");
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string Budget { get; set; }
        public string Spend { get; set; }
        public string MarketingInfluenceLeads { get; set; }
        public string MarketingGeneratedLeads { get; set; }
        public string MarketingInfluenceOpportunity { get; set; }
        public string MarketingGeneratedOpportunity { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string MILGoal { get; set; }
        public string MILLow { get; set; }
        public string MILHigh { get; set; }
        public string MGLGoal { get; set; }
        public string MGLLow { get; set; }
        public string MGLHigh { get; set; }
        public string MIOGoal { get; set; }
        public string MIOLow { get; set; }
        public string MIOHigh { get; set; }
        public string MGOGoal { get; set; }
        public string MGOLow { get; set; }
        public string MGOHigh { get; set; }

        public int MasterCampaignId { get; set; }

        public int CampaignType { get; set; }

        public Boolean IsActive { get; set; }
        public IEnumerable<Industry> IndustryViewModels { get; set; }
        public IEnumerable<Geography> GeographyViewModels { get; set; }
        public IEnumerable<Theme> ThemeViewModels { get; set; }
        public IEnumerable<Segment> SegmentViewModels { get; set; }
        public IEnumerable<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IEnumerable<BusinessLine> BusinessLineViewModels { get; set; }
        public IEnumerable<MasterCampaign> MasterViewModels { get; set; }
        public int[] BusinessGroups_Id { get; set; }
        public int[] BusinessLines_Id { get; set; }
        public int[] Geographys_Id { get; set; }
        public int[] Industries_Id { get; set; }
        public int[] Segments_Id { get; set; }
        public int[] Themes_Id { get; set; }

        public IEnumerable<CampaignTypes> campaignTypeViewModels { get; set; }
    }

    public class CampaignTypes
    {
       public int Id { get; set; }
       public string Name { get; set; }
    }
}