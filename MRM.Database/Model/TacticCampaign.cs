using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
   public class TacticCampaign : CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TacticDescription { get; set; }
        public string Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int MasterCampaign_Id { get; set; }
        public string TacticType { get; set; }

        public string ReachR1Goal { get; set; }
        public string ReachR1Low { get; set; }
        public string ReachR1High { get; set; }
        public string ReachR11Goal { get; set; }
        public string ReachR12Low { get; set; }
        public string ReachR13High { get; set; }
        public string ResponseR1Goal { get; set; }
        public string ResponseR1Low { get; set; }
        public string ResponseR1High { get; set; }
        public string ResponseR21Goal { get; set; }
        public string ResponseR22Low { get; set; }
        public string ResponseR23High { get; set; }


        public string EfficiencyE1Goal { get; set; }
        public string EfficiencyE1Low { get; set; }
        public string EfficiencyE1High { get; set; }

        public string EfficiencyE11Goal { get; set; }
        public string EfficiencyE12Low { get; set; }
        public string EfficiencyE13High { get; set; }


        public virtual ICollection<BusinessGroup> BusinessGroups { get; set; }
        public virtual ICollection<BusinessLine> BusinessLines { get; set; }
        public virtual ICollection<Segment> Segments { get; set; }
        public virtual ICollection<Industry> Industries { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
        public virtual ICollection<Geography> Geographys { get; set; }
        public virtual ChildCampaign ChildCampaigns { get; set; }


        [NotMapped]
        public List<TacticCampaign> TacticCampaigns = new List<TacticCampaign>();
        [NotMapped]
        public int PageCount { get; set; }
        [NotMapped]
        public int CurrentPageIndex { get; set; }
    }
}
