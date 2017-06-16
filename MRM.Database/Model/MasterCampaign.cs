using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<BusinessGroup> BusinessGroups { get; set; }
        public virtual ICollection<BusinessLine> BusinessLines { get; set; }
        public virtual ICollection<Segment> Segments { get; set; }
        public virtual ICollection<Industry> Industries { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
        public virtual ICollection<Geography> Geographys { get; set; }
        public virtual ICollection<ChildCampaign> ChildCampaigns { get; set; }

        [NotMapped]
        public List<MasterCampaign> MasterCampaigns = new List<MasterCampaign>();
        [NotMapped]
        public int PageCount { get; set; }
        [NotMapped]
        public int CurrentPageIndex { get; set; }
    }
}
