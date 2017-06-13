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
   public class ThemeServices : IThemeServices
    {
        private GenericUnitOfWork guow = null;

        public ThemeServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<Theme> GetTheme()
        {
            IEnumerable<Theme> thmval = guow.GenericRepository<Theme>().GetAll().ToList();
            return thmval;
        }
    }
}
