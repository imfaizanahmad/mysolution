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

        public IList<Segment> GetSegment()
        {
            IList<Segment> seg = guow.GenericRepository<Segment>().GetAll().Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            return seg;
        }

        public bool CreateMCSegment(MasterCampaign MC)
        {
            guow.GenericRepository<MasterCampaign>().Insert(MC);
            if (MC.Id != 0)
                return true;
            else
                return false;
        }
    }
}
