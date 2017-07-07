using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
    public class TacticCampaignReachResponse : CommonEntity
    {
        public int Id { get; set; }
        public string MetricType { get; set; }
        public int MetricId { get; set; }
        public int Goal { get; set; }
        public int High { get; set; }
        public int Low { get; set; }

        public virtual TacticCampaign TacticCampaign { get; set; }
    }
}
