using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.ViewModel;

namespace MRM.Business.Interfaces
{
   public interface ITacticCampaignServices
    {
        IEnumerable<TacticCampaign> GetTacticCampaign();
        bool CreateTacticCampaign(TacticCampaign campaign);
        IEnumerable<Vendor> GetVendor();
        List<TacticCampaign> GetTacticBySubCampaignId(TacticCampaignViewModel model);
        bool DeleteTacticCampaign(int Id);
        List<TacticCampaign> GetTacticCampaignById(TacticCampaignViewModel model);


    }
}
