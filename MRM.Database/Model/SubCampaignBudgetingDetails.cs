using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
    public class SubCampaignBudgetingDetail : CommonEntity
    {
        public int Id { get; set; }
        public string BusinessGroupId { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Spend { get; set; }

        public virtual ChildCampaign ChildCampaign { get; set; }
    }
}
