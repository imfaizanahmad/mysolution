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
   public class IndustryServices : IIndustryServices
    {
        private GenericUnitOfWork guow = null;

        public  IndustryServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<Industry> GetIndustry()
        {
            IEnumerable<Industry> industry = guow.GenericRepository<Industry>().GetAll().Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            return industry;
        }
       
        public bool CreateMCIndustry(MasterCampaign MC)
        {
            guow.GenericRepository<MasterCampaign>().Insert(MC);
            if (MC.Id != 0)
                return true;
            else
                return false;
        }

        public List<Industry> GetIndustryBySegmentId(string SegmentId)
        {

            List<Industry> lstIndustry = new List<Industry>();
            foreach (var item in SegmentId)
            {
                var industry = guow.GenericRepository<Industry>().GetByID(item);
                lstIndustry.Add(industry);
            }
            return lstIndustry;
        }
    }
}
