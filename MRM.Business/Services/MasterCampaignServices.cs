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

        public IQueryable<MasterCampaign> MasterCampaignTable()
        {
            return guow.GenericRepository<MasterCampaign>().Table;
        }

        public IQueryable<MasterCampaign> GetOrderedMasterCampaign()
        {
            return guow.GenericRepository<MasterCampaign>().Table.OrderByDescending(t => t.UpdatedDate);
        }

        public IList<MasterCampaign> GetMasterCampaign()
        {
            IList<MasterCampaign> masterCampaign = guow.GenericRepository<MasterCampaign>().GetAll().OrderByDescending(t => t.UpdatedDate).ToList();
            return masterCampaign;
        }

        public List<MasterCampaign> GetMasterCampaignById(MasterCampaignViewModel model)
        {
            List<MasterCampaign> masterCampaign = guow.GenericRepository<MasterCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == model.Id).ToList();
            return masterCampaign;
        }

        public List<MasterCampaign> GetMasterCampaignById(int masterId)
        {
            List<MasterCampaign> masterCampaign = guow.GenericRepository<MasterCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes)).Where(t => t.Id == masterId).ToList();
            return masterCampaign;
        }

        public List<ChildCampaign> GetChildCampaignByMasterId(int id)
        {
            List<ChildCampaign> returnlist = guow.GenericRepository<ChildCampaign>().GetAll().Where(t => t.MasterCampaigns.Id == id).ToList();
            return returnlist;
        }

        private void ModelToEntity(MasterCampaignViewModel model, MasterCampaign masterCampaignEntity)
        {
            masterCampaignEntity.InheritStatus = model.Status == Status.Draft.ToString() ? InheritStatus.Draft.ToString() : InheritStatus.Active.ToString();
            if (masterCampaignEntity.Status == Status.Complete.ToString() && model.Id != 0)
            {
                masterCampaignEntity.CampaignDescription = model.CampaignDescription;
                masterCampaignEntity.StartDate = model.StartDate;
                masterCampaignEntity.EndDate = model.EndDate;
                masterCampaignEntity.Status = model.Status;
            }
            else
            {
                masterCampaignEntity.Name = model.Name;
                masterCampaignEntity.CampaignDescription = model.CampaignDescription;
                masterCampaignEntity.CampaignManager = model.CampaignManager;
                masterCampaignEntity.StartDate = model.StartDate;
                masterCampaignEntity.EndDate = model.EndDate;
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

                }

                if (model.BusinessLines_Id != null)
                {
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

                }

                if (model.Industries_Id != null)
                {
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
                if (model.Id == 0)
                {
                    masterCampaignEntity.VisitedDate = DateTime.Now;
                }
            }
           
            return;
        }

        public bool InsertMasterCampaign(MasterCampaignViewModel model)
        {
            MasterCampaign masterCampaignEntity = new MasterCampaign();
            ModelToEntity(model, masterCampaignEntity);
            guow.GenericRepository<MasterCampaign>().Insert(masterCampaignEntity);
            //return true;
            if (masterCampaignEntity.Id != 0)
                return true;
            else
                return false;
        }

        private MasterCampaign LoadMasterCampaignEntity(int masterCampaignId)
        {
            return guow.GenericRepository<MasterCampaign>().GetByID(masterCampaignId);
        }

        public MasterCampaign Update(MasterCampaign masterCampaign)
        {
            return guow.GenericRepository<MasterCampaign>().Update(masterCampaign);
        }

        public void UpdateForDraft(MasterCampaignViewModel model)
        {
            var masterCamp = LoadMasterCampaignEntity(model.Id);
            masterCamp = FlushChildRecords(masterCamp);
            ModelToEntity(model, masterCamp);
            guow.GenericRepository<MasterCampaign>().Update(masterCamp);
        }

        private MasterCampaign FlushChildRecords(MasterCampaign masterCamp)
        {
          
            masterCamp.Industries.Remove(masterCamp.Industries.FirstOrDefault<Industry>());
            masterCamp.Segments.Remove(masterCamp.Segments.FirstOrDefault<Segment>());
            masterCamp.Themes.Remove(masterCamp.Themes.FirstOrDefault<Theme>());
            masterCamp.Geographys.Remove(masterCamp.Geographys.FirstOrDefault<Geography>());
            masterCamp.BusinessLines.Remove(masterCamp.BusinessLines.FirstOrDefault<BusinessLine>());
            masterCamp.BusinessGroups.Remove(masterCamp.BusinessGroups.FirstOrDefault<BusinessGroup>());
            return masterCamp;
        }

        public void Submit(MasterCampaignViewModel model)
        {
            var masterCamp = LoadMasterCampaignEntity(model.Id);
            if (masterCamp.Status != Status.Complete.ToString())
            {
                masterCamp = FlushChildRecords(masterCamp);
            }
            ModelToEntity(model, masterCamp);
            guow.GenericRepository<MasterCampaign>().Update(masterCamp);
        }

        public bool DeleteMasterCampaign(int Id)
        {
            guow.GenericRepository<MasterCampaign>().Delete(Id);
            return true;

        }
        //Deleted last visited
        public void DeleteLastyearVisited()
        {
            var MasterList = GetMasterCampaign()
                .Where(s => s.Status == Status.Draft.ToString() && (s.VisitedDate <= DateTime.Now.AddYears(-1))).ToList();
            if (MasterList.Count > 0)
            {
                foreach (var item in MasterList)
                {
                    var masterCampaign = GetMasterCampaignById(new MasterCampaignViewModel() { Id = item.Id })
                        .FirstOrDefault();
                    masterCampaign.IsActive = false;
                    Update(masterCampaign);
                }
            }
        }
    }
}
