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
    public class JourneyStageServices : IJourneyStageServices
    {
        private GenericUnitOfWork guow = null;

        public JourneyStageServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<JourneyStage> GetJourneyStage()
        {
            IEnumerable<JourneyStage> Jorneystageval = guow.GenericRepository<JourneyStage>().GetAll().Where(t => !string.IsNullOrEmpty(t.Name) && t.IsActive).ToList();
            return Jorneystageval;
        }
       
    }
}
