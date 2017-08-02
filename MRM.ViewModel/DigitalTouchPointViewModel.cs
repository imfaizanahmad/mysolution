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
    }

    public class DigitalViewModel
    {
        public string TacticCampaign_Id { get; set; }
        public string TacticName {get;set;}
        public int? TacticType_Id { get; set; }
        public string TacticTypeName { get; set; }
        public ICollection<DigitalTouchPointViewModel> DigitalTouchPoint { get; set; }

    }
}
