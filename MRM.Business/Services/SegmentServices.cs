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
   public class SegmentServices : ISegmentServices
    {
        private GenericUnitOfWork guow = null;

        public SegmentServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<Segment> GetSegment()
        {
            IEnumerable<Segment> seg = guow.GenericRepository<Segment>().GetAll().ToList();
            return seg;
        }
    }
}
