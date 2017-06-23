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
    public class MasterCampaignServices : IMasterCampaignServices
    {
        private GenericUnitOfWork guow = null;

        public MasterCampaignServices()
        {
            guow = new GenericUnitOfWork();
        }

        public List<MasterCampaign> GetMasterCampaignById(MasterCampaignViewModel model)
        {
            List<MasterCampaign> masterCampaign = guow.GenericRepository<MasterCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == model.Id && t.Status == "Complete").ToList();
            return masterCampaign;
        }

        public IEnumerable<MasterCampaign> GetMasterCampaign()
        {
            IEnumerable<MasterCampaign> masterCampaign = guow.GenericRepository<MasterCampaign>().GetAll();
            return masterCampaign;
        }

        public bool CreateMasterCampaign(MasterCampaignViewModel model)
        {

            MasterCampaign masterCampaignEntity = new MasterCampaign();
            masterCampaignEntity.Name = model.Name;
            masterCampaignEntity.CampaignDescription = model.CampaignDescription;
            masterCampaignEntity.StartDate = string.IsNullOrEmpty(model.StartDate) == true ? DateTime.Now : Convert.ToDateTime(model.StartDate);
            masterCampaignEntity.EndDate = string.IsNullOrEmpty(model.EndDate) == true ? DateTime.Now : Convert.ToDateTime(model.EndDate);
            masterCampaignEntity.Status = model.Status;
            masterCampaignEntity.CreatedBy = "user";


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





            masterCampaignEntity.BusinessGroups = lstBGroup;
            masterCampaignEntity.Themes = lsttheme;
            masterCampaignEntity.BusinessLines = lstBline;
            masterCampaignEntity.Segments = lstsegment;
            masterCampaignEntity.Industries = lstindustry;
            masterCampaignEntity.Geographys = lstgeography;

            if(model.Id == 0)
            guow.GenericRepository<MasterCampaign>().Insert(masterCampaignEntity);
            else
                guow.GenericRepository<MasterCampaign>().Update(masterCampaignEntity);

            if (masterCampaignEntity.Id != 0)
                return true;
            else
                return false;
        }


        public bool DeleteMasterCampaign(int Id)
        {
            guow.GenericRepository<MasterCampaign>().Delete(Id);
            return true;

        }

    }
}
