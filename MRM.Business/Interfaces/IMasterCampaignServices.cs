using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.ViewModel;
namespace MRM.Business.Interfaces
{
   public interface IMasterCampaignServices
    {
        IEnumerable<MasterCampaign> GetMasterCampaign();
        bool CreateMasterCampaign(MasterCampaignViewModel campaign);
        List<MasterCampaign> GetMasterCampaignById(MasterCampaignViewModel model);
    }
}
