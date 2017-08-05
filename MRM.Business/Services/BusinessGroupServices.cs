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

        public IList<BusinessGroup> GetBG()
        {
            IList<BusinessGroup> bg = guow.GenericRepository<BusinessGroup>().GetAll().Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            return bg;
        }


        public bool CreateMCBusinessGroup(BusinessGroup BG)
        {
            guow.GenericRepository<BusinessGroup>().Insert(BG);
            if (BG.Id != 0)
                return true;
            else
                return false;
        }

        public IQueryable<BusinessGroup> BusinessGroupTable()
        {
            return guow.GenericRepository<BusinessGroup>().Table;
        }
    }
}
