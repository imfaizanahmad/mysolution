using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;

namespace MRM.ViewModel
{
    public class TacticCampaignViewModel
    {
        public TacticCampaignViewModel()
        {
            BusinessGroupViewModels = (new[] { new BusinessGroup { Id = -1, Name = "None selected." } });
            SegmentViewModels = (new[] { new Segment { Id = -1, Name = "None selected." } });
            BusinessLineViewModels = (new[] { new BusinessLine() });
            ThemeViewModels = (new[] { new Theme() });
            GeographyViewModels = (new[] { new Geography() });
            IndustryViewModels = (new[] { new Industry() });
            ChildCampaignViewModels = (new[] { new ChildCampaign() });
            VendorViewModels = (new[] { new Vendor() });
            TacticTypeViewModels = (new[] { new TacticType { Id = -1, Name = "None selected." } });

            TacticCampaignReachResponseViewModels = (new[] {
                new TacticCampaignReachResponse {MetricType="Reach",MetricId=default(int),Goal=default(int),Low=default(int),High=default(int) },
                new TacticCampaignReachResponse { MetricType="Response",MetricId=default(int),Goal=default(int),Low=default(int),High=default(int)}
            });

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public int? Year { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? MCStartDate { get; set; }
        public DateTime? MCEndDate { get; set; }

        public string Status { get; set; }
        public string Vendor { get; set; }
        public int ChildCampaign_Id { get; set; }
        public int MasterCampaign_Id { get; set; }
        public Boolean IsActive { get; set; }

        public int SubCampaignType { get; set; }

        public bool ThemeSelectUnselect { get; set; }
        public bool BgSelectUnselect { get; set; }
        public bool BlSelectUnselect { get; set; }
        public bool GeoSelectUnselect { get; set; }
        public bool SegSelectUnselect { get; set; }
        public bool IndustrySelectUnselect { get; set; }
        public string StatusInheritaceStamp { get; set; }
        public string InheritanceStatus { get; set; }
        //public JourneyStage JourneyStages { get; set; }

        public IList<JourneyStage> JourneyStageViewModels { get; set; }
        public IList<Industry> IndustryViewModels { get; set; }
        public IList<Geography> GeographyViewModels { get; set; }
        public IList<Theme> ThemeViewModels { get; set; }
        public IList<Segment> SegmentViewModels { get; set; }
        public IList<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IList<BusinessLine> BusinessLineViewModels { get; set; }
        public IList<MasterCampaign> MasterViewModels { get; set; }
        public IList<ChildCampaign> ChildCampaignViewModels { get; set; }
        public IList<Vendor> VendorViewModels { get; set; }
        public IList<TacticType> TacticTypeViewModels { get; set; }
        public int[] BusinessGroups_Id { get; set; }
        public int[] BusinessLines_Id { get; set; }
        public int[] Geographys_Id { get; set; }
        public int[] Industries_Id { get; set; }
        public int[] Segments_Id { get; set; }
        public int[] Themes_Id { get; set; }
        public int[] Vendor_Id { get; set; }
        public int[] Tactic_Id { get; set; }
        public int[] TacticType_Id { get; set; }
        public int? TacticType { get; set; }
        public int JournetStage_Id { get; set; }

        public IList<MetricReach> MetricReachViewModels { get; set; }     
        public IList<MetricResponse> MetricResponseViewModels { get; set; }
        public IList<TacticCampaignReachResponse> TacticCampaignReachResponseViewModels { get; set; }
    }

    public class TacticCampaignViewModelList
    {
        public string DigitalID { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string InheritStatus { get; set; }
    }
}
