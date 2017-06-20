using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.ViewModel;

namespace MRM.Business.Interfaces
{
   public interface IChildCampaignServices
    {
        IEnumerable<ChildCampaign> GetChildCampaign();
        bool CreateChildCampaign(ChildCampaignViewModel campaign);
        List<ChildCampaign> GetChildCampaignById(ChildCampaignViewModel model);
    }
}
