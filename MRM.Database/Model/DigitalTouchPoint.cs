using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{    
    public class DigitalTouchPoint : CommonEntity
    {
        public DigitalTouchPoint()
        {
            IsDelete = false;
        }
        public int Id { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }        
        public string Medium { get; set; }        
        public string Term { get; set; }

        public bool IsDelete { get; set; }
        public string InheritStatus { get; set; }
        public string UTM { get; set; }
        public virtual TacticType TacticType { get; set; }
        public virtual TacticCampaign TacticCampaign { get; set; }

    }
}
