using MRM.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Business.Interfaces
{
    public interface IMetricReachServices
    {
        IList<MetricReach> GetAllMetricReach();
    }
}
