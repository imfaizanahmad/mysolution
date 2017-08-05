using System;
using System.Collections.Generic;

namespace MRM.Models
{
    public class BulkMasterCampaign
    {
        
        public int MCId { get; set; }
        public string MCName { get; set; }
        public string MCampaignDescription { get; set; }
        public string MCampaignManager { get; set; }
        public DateTime? MCStartDate { get; set; }
        public DateTime? MCEndDate { get; set; }
        public IList<ApiBusinessGroup> BusinessGroupList { get; set; }
        public IList<ApiBusinessLine> BusinessLineList { get; set; }
        public IList<ApiSegment> SegmentList { get; set; }
        public IList<ApiIndustry> IndustryList { get; set; }
        public IList<ApiChildCampaign> ChildCampaignList { get; set; }
    }
    public class ApiChildCampaign
    {
        public ApiChildCampaign()
        {
            TacticList = new List<ApiTacticCampaign>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignManager { get; set; }
        public int CampaignType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IList<ApiTacticCampaign> TacticList { get; set; }
        public IList<ApiBusinessGroup> BusinessGroupList { get; set; }
        public IList<ApiBusinessLine> BusinessLineList { get; set; }
        public IList<ApiSegment> SegmentList { get; set; }
        public IList<ApiIndustry> IndustryList { get; set; }
    }
    public class ApiTacticCampaign
    {
        public int ParentChildId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignManager { get; set; }
        public int? TacticType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IList<ApiBusinessGroup> BusinessGroupList { get; set; }
        public IList<ApiBusinessLine> BusinessLineList { get; set; }
        public IList<ApiSegment> SegmentList { get; set; }
        public IList<ApiIndustry> IndustryList { get; set; }
    }

    public class ApiBusinessGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ApiBusinessLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ApiSegment
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ApiIndustry
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}