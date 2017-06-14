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
  public class ChildCampaignServices : IChildCampaignServices
    {
        private GenericUnitOfWork guow = null;

        public ChildCampaignServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<ChildCampaign> GetChildCampaign()
        {
            IEnumerable<ChildCampaign> childCampaign = guow.GenericRepository<ChildCampaign>().GetAll().ToList();
            return childCampaign;
        }

        public bool CreateChildCampaign(ChildCampaign campaign)
        {
            guow.GenericRepository<ChildCampaign>().Insert(campaign);
            if (campaign.Id != 0)
                return true;
            else
                return false;
        }
    }
}
