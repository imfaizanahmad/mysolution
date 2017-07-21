﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
   public class ChildCampaign : CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignManager { get; set; }
        public string Budget { get; set; }
        public string Spend { get; set; }
        public string MarketingInfluenceLeads { get; set; }
        public string MarketingGeneratedLeads { get; set; }
        public string MarketingInfluenceOpportunity { get; set; }
        public string MarketingGeneratedOpportunity { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VisitedDate { get; set; }
        public int CampaignType { get; set; }
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



        public virtual ICollection<BusinessGroup> BusinessGroups { get; set; }
        public virtual ICollection<BusinessLine> BusinessLines { get; set; }
        public virtual ICollection<Segment> Segments { get; set; }
        public virtual ICollection<Industry> Industries { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
        public virtual ICollection<Geography> Geographys { get; set; }
        public virtual MasterCampaign MasterCampaigns { get; set; }
        public virtual ICollection<TacticCampaign> TacticCampaigns { get; set; }

        [NotMapped]
        public List<ChildCampaign> ChildCampaigns = new List<ChildCampaign>();
        [NotMapped]
        public virtual int PageCount { get; set; }
        [NotMapped]
        public virtual int CurrentPageIndex { get; set; }
    }
}
