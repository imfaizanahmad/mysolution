using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
   public class Segment : CommonEntity
    {
        public int Id { get; set; }
        public int SegmentId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<MasterCampaign> MasterCampaigns { get; set; }
        public virtual ICollection<ChildCampaign> ChildCampaigns { get; set; }
        public virtual ICollection<TacticCampaign> TacticCampaigns { get; set; }
        public virtual ICollection<Industry> Industries { get; set; }
    }
}
