using System;
using System.Collections.Generic;
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

        public virtual BusinessGroup BusinessGroups { get; set; }
        public virtual BusinessLine BusinessLines { get; set; }
        public virtual Segment Segments { get; set; }
        public virtual Industry Industries { get; set; }
        public virtual Theme Themes { get; set; }
        public virtual Geography Geographys { get; set; }
        public virtual ICollection<MasterCampaign> MasterCampaigns { get; set; }
        public virtual ICollection<TacticCampaign> TacticCampaigns { get; set; }
    }
}
