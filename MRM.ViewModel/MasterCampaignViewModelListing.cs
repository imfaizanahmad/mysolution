using MRM.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.ViewModel
{
    public class MasterCampaignViewModelListing
    {       
        public string Id { get; set; }
        public string Name { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignManager { get; set; }
        public string Status { get; set; }
        public Boolean IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InheritStatus { get; set; }

        public string CreatedBy { get; set; }
    }
}
