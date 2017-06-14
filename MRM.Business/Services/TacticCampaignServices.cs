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
   public class TacticCampaignServices
    {
        private GenericUnitOfWork guow = null;

        public TacticCampaignServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<TacticCampaign> GetTacticCampaign()
        {
            IEnumerable<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAll().ToList();
            return tacticCampaign;
        }

        public bool CreateTacticCampaign(TacticCampaign campaign)
        {
            guow.GenericRepository<TacticCampaign>().Insert(campaign);
            if (campaign.Id != 0)
                return true;
            else
                return false;
        }

    }
}
