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


        public IEnumerable<Vendor> GetVendor()
        {
            return guow.GenericRepository<Vendor>().GetAll().ToList();
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



            List<BusinessLine> lstBline = null;
            List<BusinessGroup> lstBGroup = null;
            if (model.BusinessGroups_Id != null)
            {
                lstBGroup = new List<BusinessGroup>();
                foreach (var item in model.BusinessGroups_Id)
                {
                    var Bgroups = guow.GenericRepository<BusinessGroup>().GetByID(item);
                    lstBGroup.Add(Bgroups);
                }
                lstBline = new List<BusinessLine>();
                foreach (var item in model.BusinessLines_Id)
                {
                    var Bline = guow.GenericRepository<BusinessLine>().GetByID(item);
                    lstBline.Add(Bline);
                }
            }


            List<Theme> lsttheme = null;
            if (model.Themes_Id != null)
            {
                lsttheme = new List<Theme>();
                foreach (var item in model.Themes_Id)
                {
                    var theme = guow.GenericRepository<Theme>().GetByID(item);
                    lsttheme.Add(theme);
                }

            }

            List<Industry> lstindustry = null;
            List<Segment> lstsegment = null;

            if (model.Segments_Id != null)
            {
                lstsegment = new List<Segment>();
                foreach (var item in model.Segments_Id)
                {
                    var segment = guow.GenericRepository<Segment>().GetByID(item);
                    lstsegment.Add(segment);
                }
                lstindustry = new List<Industry>();
                foreach (var item in model.Industries_Id)
                {
                    var industry = guow.GenericRepository<Industry>().GetByID(item);
                    lstindustry.Add(industry);
                }
            }

            List<Geography> lstgeography = null;
            if (model.Geographys_Id != null)
            {
                lstgeography = new List<Geography>();
                foreach (var item in model.Geographys_Id)
                {
                    var geography = guow.GenericRepository<Geography>().GetByID(item);
                    lstgeography.Add(geography);
                }

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

        public List<TacticCampaign> GetTacticBySubCampaignId(TacticCampaignViewModel model)
        {
            List<TacticCampaign> tacticlist = new List<TacticCampaign>();
            ChildCampaign child = guow.GenericRepository<ChildCampaign>().GetByID(model.ChildCampaign_Id);
            tacticlist.AddRange(child.TacticCampaigns);
            return tacticlist;

        }
        //created by suraj
        public bool DeleteTacticCampaign(int Id)
        {
            guow.GenericRepository<TacticCampaign>().Delete(Id);
            return true;

        }

        //created by suraj
        public List<TacticCampaign> GetTacticCampaignById(TacticCampaignViewModel model)
        {
            List<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == model.Id).ToList();
            return tacticCampaign;
        }
    }
}
