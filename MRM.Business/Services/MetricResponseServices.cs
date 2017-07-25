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
    public class MetricResponseServices: IMetricResponseServices
    {
        private GenericUnitOfWork guow = null;

        public MetricResponseServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IList<MetricResponse> GetAllMetricResponse()
        {
            IList<MetricResponse> metricResponseList = guow.GenericRepository<MetricResponse>().GetAll().ToList();
            return metricResponseList;
        }
    }
}
