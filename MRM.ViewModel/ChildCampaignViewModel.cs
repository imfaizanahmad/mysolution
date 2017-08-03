using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            BusinessGroupViewModels = new[] { new BusinessGroup {Id = -1, Name = "None selected."} };
            SegmentViewModels = (new[] { new Segment { Id = -1, Name = "None selected." } });
            BusinessLineViewModels = (new[] { new BusinessLine() });
            ThemeViewModels = (new[] { new Theme() });
            GeographyViewModels = (new[] { new Geography() });
            IndustryViewModels = (new[] { new Industry() });
            MasterViewModels = (new[] { new MasterCampaign() });
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignManager { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "${0:#,0}")]
        public decimal? Budget { get; set; }
        public decimal? Spend { get; set; }
        public string MarketingInfluenceLeads { get; set; }
        public string MarketingGeneratedLeads { get; set; }
        public string MarketingInfluenceOpportunity { get; set; }
        public string MarketingGeneratedOpportunity { get; set; }
        public string Status { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public DateTime? MCStartDate { get; set; }
        public DateTime? MCEndDate { get; set; }

        public int? MILGoal { get; set; }
        public int? MILLow { get; set; }
        public int? MILHigh { get; set; }
        public int? MGLGoal { get; set; }
        public int? MGLLow { get; set; }
        public int? MGLHigh { get; set; }
        public decimal? MIOGoal { get; set; }
        public decimal? MIOLow { get; set; }
        public decimal? MIOHigh { get; set; }
        public decimal? MGOGoal { get; set; }
        public decimal? MGOLow { get; set; }
        public decimal? MGOHigh { get; set; }
        public string MILSource { get; set; }
        public string MGLSource { get; set; }
        public string MIOSource { get; set; }
        public string MGOSource { get; set; }

        public bool ThemeSelectUnselect { get; set; }
        public bool BgSelectUnselect { get; set; }
        public bool BlSelectUnselect { get; set; }
        public bool GeoSelectUnselect { get; set; }
        public bool SegSelectUnselect { get; set; }
        public bool IndustrySelectUnselect { get; set; }

        public string StatusInheritaceStamp { get; set; }
        public string InheritanceStatus { get; set; }

        public int MasterCampaignId { get; set; }

        public Boolean IsActive { get; set; }
        public IList<Industry> IndustryViewModels { get; set; }
        public IList<Geography> GeographyViewModels { get; set; }
        public IList<Theme> ThemeViewModels { get; set; }
        public IList<Segment> SegmentViewModels { get; set; }
        public IList<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IList<BusinessLine> BusinessLineViewModels { get; set; }
        public IList<MasterCampaign> MasterViewModels { get; set; }
        public IList<SubCampaignBudgetingDetail> SubCampaignBudgetingDetailViewModels { get; set; }
        public CampaignType CampaignTypes { get; set; }
        public int[] BusinessGroups_Id { get; set; }
        public int[] BusinessLines_Id { get; set; }
        public int[] Geographys_Id { get; set; }
        public int[] Industries_Id { get; set; }
        public int[] Segments_Id { get; set; }
        public int[] Themes_Id { get; set; }
    }

    public class ChildCampaignViewModelList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignManager { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InheritStatus { get; set; }
        public string CreatedBy { get; set; }
    }
}
