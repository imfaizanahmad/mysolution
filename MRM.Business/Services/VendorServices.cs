using System.Collections.Generic;
using System.Linq;
using MRM.Database.Model;
using MRM.Database.GenericUnitOfWork;

namespace MRM.Business.Services
{
   public class VendorServices
    {
        private GenericUnitOfWork guow;

        public VendorServices()
        {
                guow = new GenericUnitOfWork();
        }
        public IEnumerable<Vendor> GetVendor()
        {
            return guow.GenericRepository<Vendor>().GetAll().ToList();// == null? null:guow.GenericRepository<Vendor>().GetAll().ToList();//.ToList();
        }
    }
}
