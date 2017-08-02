using System.Collections.Generic;
using System.Linq;
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

        public IList<BusinessLine> GetBusinessLine()
        {
            IList<BusinessLine> bl = guow.GenericRepository<BusinessLine>().GetAll().Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            return bl;
        }

        public bool CreateMCBusinessLine(MasterCampaign MC)
        {
            guow.GenericRepository<MasterCampaign>().Insert(MC);
            if (MC.Id != 0)
                return true;
            else
                return false;
        }

        public List<BusinessLine> GetBusinessLineByBGId(int[] BGId)
        {
            List<BusinessLine> lstBline = new List<BusinessLine>();
            if (BGId != null)
            {
                var businessLines = guow.GenericRepository<BusinessLine>().Table.Where(t => BGId.Contains(t.BusinessGroups.Id)).ToList();
                lstBline.AddRange(businessLines);
            }
            return lstBline;
        }

        public IQueryable<BusinessLine> BusinessLineTable()
        {
            return guow.GenericRepository<BusinessLine>().Table;
        }
    }
}
