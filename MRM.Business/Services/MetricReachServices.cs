using MRM.Business.Interfaces;
using MRM.Database.GenericUnitOfWork;
using MRM.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Business.Services
{
    public class MetricReachServices : IMetricReachServices
    {
        private GenericUnitOfWork guow = null;

        public MetricReachServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<MetricReach> GetAllMetricReach()
        {
            IEnumerable<MetricReach> metricReachList = guow.GenericRepository<MetricReach>().GetAll().ToList();
            return metricReachList;
        }
    }
}
