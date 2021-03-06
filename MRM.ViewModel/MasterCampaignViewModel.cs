﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using System.ComponentModel.DataAnnotations;

namespace MRM.ViewModel
{

    public class MasterCampaignViewModel
    {
        public MasterCampaignViewModel()
        {
            BusinessGroupViewModels = new[] { new BusinessGroup() };
            SegmentViewModels = (new[] { new Segment() });
            BusinessLineViewModels = (new[] { new BusinessLine() });
            ThemeViewModels = (new[] { new Theme() });
            GeographyViewModels = (new[] { new Geography() });
            IndustryViewModels = (new[] { new Industry() });
            //StartDate = DateTime.Now.ToString("MM/dd/yyyy");
            //EndDate = DateTime.Now.ToString("MM/dd/yyyy");

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignManager { get; set; }
        public string Status { get; set; }
        public Boolean IsActive { get; set; }
        public string CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IList<Industry> IndustryViewModels { get; set; }
        public IList<Geography> GeographyViewModels { get; set; }
        public IList<Theme> ThemeViewModels { get; set; }
        public IList<Segment> SegmentViewModels { get; set; }
        public IList<BusinessGroup> BusinessGroupViewModels { get; set; }
        public IList<BusinessLine> BusinessLineViewModels { get; set; }

        public string StatusInheritaceStamp { get; set; }
        public string InheritanceStatus { get; set; }
        public bool ThemeSelectUnselect { get; set; }
        public bool BgSelectUnselect { get; set; }
        public bool BlSelectUnselect { get; set; }
        public bool GeoSelectUnselect { get; set; }
        public bool SegSelectUnselect { get; set; }
        public bool IndustrySelectUnselect { get; set; }

        public int[] BusinessGroups_Id { get; set; }
        public int[] BusinessLines_Id { get; set; }
        public int[] Geographys_Id { get; set; }
        public int[] Industries_Id { get; set; }
        public int[] Segments_Id { get; set; }
        public int[] Themes_Id { get; set; }
        //public int[] SelectedThemes_Id {get; set;}
        

    }
}
