using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MRM.Database.Model
{
   public class MRMContext : DbContext
    {
        public MRMContext() : base("DBConnectionString")
        {

        }

        public DbSet<MasterCampaign> MasterCampaigns { get; set; }
        public DbSet<ChildCampaign> ChildCampaigns { get; set; }
        public DbSet<TacticCampaign> TacticCampaigns { get; set; }
        public DbSet<BusinessGroup> BusinessGroups { get; set; }
        public DbSet<BusinessLine> BusinessLines { get; set; }
        public DbSet<Segment> Segment { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Geography> Geographys { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

    }

}
