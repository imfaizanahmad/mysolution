using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;

namespace MRM.Business.Interfaces
{
   public interface ISegmentServices
    {

        IEnumerable<Segment> GetSegment();
    }
}
