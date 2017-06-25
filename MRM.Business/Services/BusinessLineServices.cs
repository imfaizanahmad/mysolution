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
            if ( BGId != null)
            {
                foreach (var item in BGId)
                {
                    BusinessGroup bg = guow.GenericRepository<BusinessGroup>().GetByID(item);
                    lstBline.AddRange(bg.BusinessLines);
                    //var Bline = guow.GenericRepository<BusinessLine>().Get( x => x.BusinessGroups.BusinessGroupId == item);
                    //lstBline.Add(Bline);
                }

            }

            return lstBline;
        }
    }
}
