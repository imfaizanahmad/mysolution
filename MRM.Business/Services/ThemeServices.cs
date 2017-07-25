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

        public IList<Theme> GetTheme()
        {
            IList<Theme> thmval = guow.GenericRepository<Theme>().GetAll().Where(t=>!string.IsNullOrEmpty(t.Name)) .ToList();
            
            return thmval;
        }
        //public bool CreateMCTheme(ThemeMasterCampaign tmc)
        //{
        //    guow.GenericRepository<ThemeMasterCampaign>().Insert(tmc);
        //    if (tmc.Theme_Id != 0)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
