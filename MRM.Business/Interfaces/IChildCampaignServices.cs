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
        IList<ChildCampaign> GetChildCampaign();
        List<ChildCampaign> GetChildCampaignById(ChildCampaignViewModel model);
        List<ChildCampaign> GetChildCampaignByMasterId(int masterId);
        bool InsertChildCampaign(ChildCampaignViewModel model);
        ChildCampaign Update(ChildCampaign childCampaign);
        void Update(ChildCampaignViewModel model);
        bool DeleteSubCampaign(int id);
        List<ChildCampaign> GetDDLValuesByChildId(int masterId);
    }
}
