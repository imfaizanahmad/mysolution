using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;

namespace MRM.Business.Interfaces
{
   public interface IBusinessGroupServices
    {
        IList<BusinessGroup> GetBG();
        bool CreateMCBusinessGroup(BusinessGroup MCBG);
    }
}
