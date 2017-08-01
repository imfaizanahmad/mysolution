using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
    public class TacticType : CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  string ShortName { get; set; }

        public virtual ICollection<TacticCampaign> TacticCampaigns { get; set; }

        public virtual ICollection<DigitalTouchPoint> DigitalTouchPoint { get; set; }
    }
}
