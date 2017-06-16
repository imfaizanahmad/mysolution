using System;
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
        public string Budget { get; set; }
        public string Spend { get; set; }
        public string MarketingInfluenceLeads { get; set; }
        public string MarketingGeneratedLeads { get; set; }
        public string MarketingInfluenceOpportunity { get; set; }
        public string MarketingGeneratedOpportunity { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

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
