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
   public class BusinessLineServices : IBusinessLineServices
    {
        private GenericUnitOfWork guow = null;

        public BusinessLineServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<BusinessLine> GetBusinessLine()
        {
            IEnumerable<BusinessLine> bl = guow.GenericRepository<BusinessLine>().GetAll().Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            return bl;
        }
    }
}
