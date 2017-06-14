using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.Business.Interfaces;
using MRM.Database.GenericUnitOfWork;

namespace MRM.Business.Services
{
   public class MasterCampaignServices : IMasterCampaignServices
    {
        private GenericUnitOfWork guow = null;

        public MasterCampaignServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<MasterCampaign> GetMasterCampaign()
        {
            IEnumerable<MasterCampaign> masterCampaign = guow.GenericRepository<MasterCampaign>().GetAll().ToList();
            return masterCampaign;
        }

       public bool CreateMasterCampaign(MasterCampaign campaign)
        {
           guow.GenericRepository<MasterCampaign>().Insert(campaign);
            if (campaign.Id != 0)
                return true;
            else
                return false;
        }

    }
}
