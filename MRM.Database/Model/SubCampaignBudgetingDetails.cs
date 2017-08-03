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
        public int Budget { get; set; }
        public int Spend { get; set; }
    }
}
