using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MRM.Database.Model
{
   public class MRMContext : DbContext
    {
        public MRMContext() : base("DBConnectionString")
        {

        }
    }
}
