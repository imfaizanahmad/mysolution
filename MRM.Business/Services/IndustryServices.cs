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
            IEnumerable<Industry> industry = guow.GenericRepository<Industry>().GetAll().ToList();
            return industry;
        }
    }
}
