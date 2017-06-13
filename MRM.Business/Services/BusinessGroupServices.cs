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
   public class BusinessGroupServices : IBusinessGroupServices
    {
        private GenericUnitOfWork guow = null;

        public BusinessGroupServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<BusinessGroup> GetBG()
        {
            IEnumerable<BusinessGroup> bg = guow.GenericRepository<BusinessGroup>().GetAll().ToList();
            return bg;
        }
    }
}
