using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
    public class MasterCampaign : CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public virtual BusinessGroup BusinessGroups { get; set; }
        public virtual BusinessLine BusinessLines { get; set; }
        public virtual Segment Segments { get; set; }
        public virtual Industry Industries { get; set; }
        public virtual Theme Themes { get; set; }
        public virtual Geography Geographys { get; set; }
        public virtual ICollection<ChildCampaign> ChildCampaigns { get; set; }
    }
}
