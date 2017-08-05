using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.ViewModel;

namespace MRM.Business.Interfaces
{
   public interface IBusinessLineServices
    {
        IList<BusinessLine> GetBusinessLine();
        List<BusinessLine> GetBusinessLineByBGId(int [] BGId);
        bool CreateMCBusinessLine(MasterCampaign MC);
        IQueryable<BusinessLine> BusinessLineTable();
    }
}
