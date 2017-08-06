using MRM.Common;
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
        private Util util;
        public DigitalTouchPointViewModel()
        {
            util = new Util();
        }
        public string DisplayDigitalId {
            get {
                return util.DigitalId(Id).PadLeft(5, '0');
            }
        }
        public string DigitalTouchId { get; set; }
        public int Id { get; set; }        
        public string Content { get; set; }        
        public string Term { get; set; }
        public bool IsDelete { get; set; }
        public string UTM { get; set; }
        public string InheritStatus { get; set; }
        public int TacticType_Id { get; set; }
        public string TacticCampaign_Id { get; set; }

        public int TacticCampaignId { get; set; }

        public int Source_Id { get; set; }
        public int Medium_Id { get; set; }

        public string Medium { get; set; }
        public string Sources { get; set; }
    }

    public class DigitalViewModel
    {
        public string TacticCampaign_Id { get; set; }
        public string TacticName {get;set;}
        public int? TacticType_Id { get; set; }
        public string TacticTypeName { get; set; }
        public ICollection<DigitalTouchPointViewModel> DigitalTouchPoint { get; set; }
        public IList<DropDownValue> Medium { get; set; }
        public IList<DropDownValue> Source { get; set; }

    }

    public class DropDownValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
