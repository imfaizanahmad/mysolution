using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
   public class TacticCampaign : CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public string Year { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? VisitedDate { get; set; }
        public string Status { get; set; }
        public int MasterCampaign_Id { get; set; }
        public int? TacticType { get; set; }
        public string Vendor { get; set; }

        public string InheritStatus { get; set; }

        public int JourneyStage_Id { get; set; }
       
        public virtual ICollection<BusinessGroup> BusinessGroups { get; set; }
        public virtual ICollection<BusinessLine> BusinessLines { get; set; }
        public virtual ICollection<Segment> Segments { get; set; }
        public virtual ICollection<Industry> Industries { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
        public virtual ICollection<Geography> Geographys { get; set; }
       // public virtual ICollection<Vendor> Vendors { get; set; }
        public virtual ICollection<TacticType> TacticTypes { get; set; }
        public virtual ChildCampaign ChildCampaigns { get; set; }

        public virtual ICollection<TacticCampaignReachResponse> TacticCampaignReachResponses { get; set; }


        [NotMapped]
        public List<TacticCampaign> TacticCampaigns = new List<TacticCampaign>();
        [NotMapped]
        public int PageCount { get; set; }
        [NotMapped]
        public int CurrentPageIndex { get; set; }
    }
}
