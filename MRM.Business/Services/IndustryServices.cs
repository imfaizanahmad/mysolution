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
   public class IndustryServices : IIndustryServices
    {
        private GenericUnitOfWork guow = null;

        public  IndustryServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IList<Industry> GetIndustry()
        {
            IList<Industry> industry = guow.GenericRepository<Industry>().GetAll().Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            return industry;
        }
       
        public bool CreateMCIndustry(MasterCampaign MC)
        {
            guow.GenericRepository<MasterCampaign>().Insert(MC);
            if (MC.Id != 0)
                return true;
            else
                return false;
        }

        public List<Industry> GetIndustryBySegmentId(int [] SegmentId)
        {
            List<Industry> lstIndustry = new List<Industry>();
            if (SegmentId != null)
            {
                var businessLines = guow.GenericRepository<Industry>().Table.Where(t => SegmentId.Contains(t.Segments.Id)).ToList();
                lstIndustry.AddRange(businessLines);
            }
            return lstIndustry;
        }

        public IQueryable<Industry> IndustryTable()
        {
            return guow.GenericRepository<Industry>().Table;
        }
    }
}
