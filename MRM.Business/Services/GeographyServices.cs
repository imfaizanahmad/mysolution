﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.Business.Interfaces;
using MRM.Database.GenericUnitOfWork;

namespace MRM.Business.Services
{
   public class GeographyServices : IGeographyServices
    {
        private GenericUnitOfWork guow = null;

        public GeographyServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<Geography> GetGeography()
        {
            IEnumerable<Geography> grphy = guow.GenericRepository<Geography>().GetAll().ToList();
            return grphy;
        }
    }
}
