﻿using System;
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
            BusinessGroupViewModels = new[] { new BusinessGroup() };
            SegmentViewModels = (new[] { new Segment() });
            BusinessLineViewModels = (new[] { new BusinessLine() });
            ThemeViewModels = (new[] { new Theme() });
            GeographyViewModels = (new[] { new Geography() });
            IndustryViewModels = (new[] { new Industry() });
            ChildCampaignViewModels = (new[] { new ChildCampaign() });
            VendorViewModels = (new[] { new Vendor() });
             
            TacticCampaignReachResponseViewModels = (new[] {
                new TacticCampaignReachResponse {MetricType="Reach",MetricId=default(int),Goal=default(int),Low=default(int),High=default(int) },
                new TacticCampaignReachResponse { MetricType="Response",MetricId=default(int),Goal=default(int),Low=default(int),High=default(int)}
            });

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public string Year { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? MCStartDate { get; set; }
        public DateTime? MCEndDate { get; set; }

        public string Status { get; set; }
        public string Vendor { get; set; }
        public int ChildCampaign_Id { get; set; }
        public int MasterCampaign_Id { get; set; }
        public Boolean IsActive { get; set; }

        
        public bool ThemeSelectUnselect { get; set; }
        public bool BgSelectUnselect { get; set; }
        public bool BlSelectUnselect { get; set; }
        public bool GeoSelectUnselect { get; set; }
        public bool SegSelectUnselect { get; set; }
        public bool IndustrySelectUnselect { get; set; }
        public string StatusInheritaceStamp { get; set; }
        public string InheritanceStatus { get; set; }

        public IEnumerable<Industry> IndustryViewModels { get; set; }
        public IEnumerable<Geography> GeographyViewModels { get; set; }
        public IEnumerable<Theme> ThemeViewModels { get; set; }
        public IEnumerable<Segment> SegmentViewModels { get; set; }
        public IEnumerable<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IEnumerable<BusinessLine> BusinessLineViewModels { get; set; }
        public IEnumerable<MasterCampaign> MasterViewModels { get; set; }
        public IEnumerable<ChildCampaign> ChildCampaignViewModels { get; set; }
        public IEnumerable<Vendor> VendorViewModels { get; set; }
        public IEnumerable<TacticType> TacticTypeViewModels { get; set; }
        public int[] BusinessGroups_Id { get; set; }
        public int[] BusinessLines_Id { get; set; }
        public int[] Geographys_Id { get; set; }
        public int[] Industries_Id { get; set; }
        public int[] Segments_Id { get; set; }
        public int[] Themes_Id { get; set; }
        public int[] Vendor_Id { get; set; }
        public int[] Tactic_Id { get; set; }
        public int[] TacticType_Id { get; set; }
      
        public IEnumerable<MetricReach> MetricReachViewModels { get; set; }     
        public IEnumerable<MetricResponse> MetricResponseViewModels { get; set; }
        public IEnumerable<TacticCampaignReachResponse> TacticCampaignReachResponseViewModels { get; set; }
    }

    public class TacticCampaignViewModelList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}
