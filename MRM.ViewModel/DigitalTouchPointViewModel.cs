using MRM.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.ViewModel
{
    public class DigitalTouchPointViewModel
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }
        public string Medium { get; set; }
        public string Term { get; set; }
        public int TacticType_Id { get; set; }
        public string TacticCampaign_Id { get; set; }
        // public virtual ICollection<Vendor> Vendors { get; set; }
        public virtual IList<TacticType> TacticType { get; set; }
        public virtual IList<TacticCampaign> TacticCampaign { get; set; }
    }
}
