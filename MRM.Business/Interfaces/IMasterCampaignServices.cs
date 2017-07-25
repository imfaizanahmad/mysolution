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
        //IList<MasterCampaign> GetMasterCampaign();
        bool InsertMasterCampaign(MasterCampaignViewModel campaign);
        List<MasterCampaign> GetMasterCampaignById(MasterCampaignViewModel model);
        MasterCampaign Update(MasterCampaign masterCampaign);
        void UpdateForDraft(MasterCampaignViewModel model);
        void Submit(MasterCampaignViewModel model);
        bool DeleteMasterCampaign(int Id);
        List<ChildCampaign> GetChildCampaignByMasterId(int id);
    }
}
