using MRM.Business.Interfaces;
using MRM.Database.GenericUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Business.Services
{
    public class TacticCampaignReachResponsesServices: ITacticCampaignReachResponsesServices
    {
        private GenericUnitOfWork guow = null;

        public TacticCampaignReachResponsesServices()
        {
            guow = new GenericUnitOfWork();
        }
    }
}
