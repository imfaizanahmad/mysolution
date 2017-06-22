using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.Business.Interfaces;
using MRM.Database.GenericUnitOfWork;
using MRM.ViewModel;

namespace MRM.Business.Services
{
   public class TacticCampaignServices
    {
        private GenericUnitOfWork guow = null;

        public TacticCampaignServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IEnumerable<TacticCampaign> GetTacticCampaign()
        {
            IEnumerable<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAll().ToList();
            return tacticCampaign;
        }

        public bool CreateTacticCampaign(TacticCampaignViewModel model)
        {
            TacticCampaign tacticCampaignEntity = new TacticCampaign();

            tacticCampaignEntity.ChildCampaigns = guow.GenericRepository<ChildCampaign>().GetByID(model.ChildCampaign_Id);

            tacticCampaignEntity.Name = model.Name;
            tacticCampaignEntity.TacticDescription = model.TacticDescription;
            tacticCampaignEntity.StartDate = Convert.ToDateTime(model.StartDate);
            tacticCampaignEntity.EndDate = Convert.ToDateTime(model.EndDate);
            tacticCampaignEntity.Status = model.Status;
            tacticCampaignEntity.CreatedBy = "user";
          
           

            List<BusinessGroup> lstBGroup = new List<BusinessGroup>();
            foreach (var item in model.BusinessGroups_Id)
            {
                var Bgroups = guow.GenericRepository<BusinessGroup>().GetByID(item);
                lstBGroup.Add(Bgroups);
            }
            List<Theme> lsttheme = new List<Theme>();
            foreach (var item in model.Themes_Id)
            {
                var theme = guow.GenericRepository<Theme>().GetByID(item);
                lsttheme.Add(theme);
            }
            List<BusinessLine> lstBline = new List<BusinessLine>();
            foreach (var item in model.BusinessLines_Id)
            {
                var Bline = guow.GenericRepository<BusinessLine>().GetByID(item);
                lstBline.Add(Bline);
            }

            List<Segment> lstsegment = new List<Segment>();
            foreach (var item in model.Segments_Id)
            {
                var segment = guow.GenericRepository<Segment>().GetByID(item);
                lstsegment.Add(segment);
            }

            List<Geography> lstgeography = new List<Geography>();
            foreach (var item in model.Geographys_Id)
            {
                var geography = guow.GenericRepository<Geography>().GetByID(item);
                lstgeography.Add(geography);
            }

            List<Industry> lstindustry = new List<Industry>();
            foreach (var item in model.Industries_Id)
            {
                var industry = guow.GenericRepository<Industry>().GetByID(item);
                lstindustry.Add(industry);
            }

            tacticCampaignEntity.BusinessGroups = lstBGroup;
            tacticCampaignEntity.Themes = lsttheme;
            tacticCampaignEntity.BusinessLines = lstBline;
            tacticCampaignEntity.Segments = lstsegment;
            tacticCampaignEntity.Industries = lstindustry;
            tacticCampaignEntity.Geographys = lstgeography;


            guow.GenericRepository<TacticCampaign>().Insert(tacticCampaignEntity);
            if (tacticCampaignEntity.Id != 0)
                return true;
            else
                return false;
        }

        public List<TacticCampaign> GetTacticCampaignById(TacticCampaignViewModel model)
        {
            List<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == model.Id).ToList();
            return tacticCampaign;
        }
    }
}
