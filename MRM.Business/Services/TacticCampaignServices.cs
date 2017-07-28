using System;
using System.Collections.Generic;
using System.Linq;
using MRM.Database.Model;
using MRM.Business.Interfaces;
using MRM.Database.GenericUnitOfWork;
using MRM.ViewModel;

namespace MRM.Business.Services
{
    public class TacticCampaignServices : ITacticCampaignServices
    {
        private GenericUnitOfWork guow = null;

        public TacticCampaignServices()
        {
            guow = new GenericUnitOfWork();
        }

        public IQueryable<TacticCampaign> TacticCampaignTable()
        {
            return guow.GenericRepository<TacticCampaign>().Table;
        }

        public IQueryable<TacticCampaign> GetOrderedTacticCampaign()
        {
            return guow.GenericRepository<TacticCampaign>().Table.OrderByDescending(t => t.UpdatedDate);
        }

        public IList<TacticCampaign> GetTacticCampaign()
        {
            IList<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAll().ToList();
            return tacticCampaign;
        }

        public List<TacticCampaign> GetTacticCampaignById(TacticCampaignViewModel model)
        {
            List<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAllIncluding((t => t.Geographys), (m => m.Industries), (m => m.BusinessGroups), (m => m.BusinessLines), (m => m.Segments), (m => m.Themes), (m => m.TacticTypes)).Where(t => t.Id == model.Id).ToList();
            return tacticCampaign;
        }

        public List<TacticCampaign> GetTacticBySubCampaignId(TacticCampaignViewModel model)
        {
            List<TacticCampaign> tacticlist = new List<TacticCampaign>();
            ChildCampaign child = guow.GenericRepository<ChildCampaign>().GetByID(model.ChildCampaign_Id);
            tacticlist.AddRange(child.TacticCampaigns);
            return tacticlist;

        }

        public List<TacticCampaign> GetTacticCampaignByMasterId(int id)
        {
            List<TacticCampaign> returnlist=  guow.GenericRepository<TacticCampaign>().GetAll().Where(t => t.MasterCampaign_Id == id).ToList();
            return returnlist;
        }

        public List<TacticCampaign> GetTacticCampaignByChildId(int id)
        {
            List<TacticCampaign> returnlist = guow.GenericRepository<TacticCampaign>().GetAll().Where(t => t.ChildCampaigns.Id == id).ToList();
            return returnlist;
        }


        private void ModelToEntity(TacticCampaignViewModel model, TacticCampaign tacticCampaignEntity)
        {
          if (tacticCampaignEntity.Status != Status.Complete.ToString())
             {
                tacticCampaignEntity.ChildCampaigns = guow.GenericRepository<ChildCampaign>()
                    .GetByID(model.ChildCampaign_Id);
                tacticCampaignEntity.ChildCampaigns.MasterCampaigns = guow.GenericRepository<MasterCampaign>()
                    .GetByID(model.MasterCampaign_Id);
                tacticCampaignEntity.Name = model.Name;
                tacticCampaignEntity.CreatedBy = "user";
                tacticCampaignEntity.Year = model.Year;
                tacticCampaignEntity.MasterCampaign_Id = model.MasterCampaign_Id;
                tacticCampaignEntity.JourneyStage_Id = model.JournetStage_Id;
                tacticCampaignEntity.TacticType = model.TacticType;

                List<TacticType> lstTacticType = null;
                if (model.TacticType_Id != null)
                {
                    lstTacticType = new List<TacticType>();
                    foreach (var item in model.TacticType_Id)
                    {
                        var tacticType = guow.GenericRepository<TacticType>().GetByID(item);
                        lstTacticType.Add(tacticType);
                    }
                }
                tacticCampaignEntity.TacticTypes = lstTacticType;
                tacticCampaignEntity.TacticCampaignReachResponses =
                    model.TacticCampaignReachResponseViewModels.ToList();

                if (model.Id == 0)
                {
                    tacticCampaignEntity.VisitedDate = DateTime.Now;
                }
            }

            tacticCampaignEntity.InheritStatus = model.Status == Status.Draft.ToString() ? InheritStatus.Draft.ToString() : InheritStatus.Active.ToString();
            if (model.Id != 0 && model.Status == Status.Complete.ToString())
            {
                if (model.EndDate < DateTime.Now)
                {
                    tacticCampaignEntity.InheritStatus = InheritStatus.Complete.ToString();
                }
            }

                tacticCampaignEntity.Status = model.Status;
                tacticCampaignEntity.TacticDescription = model.TacticDescription;
                tacticCampaignEntity.StartDate = model.StartDate;
                tacticCampaignEntity.EndDate = model.EndDate;
                tacticCampaignEntity.Vendor = model.Vendor;

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
                    if (model.BusinessLines_Id != null)
                    {
                        foreach (var item in model.BusinessLines_Id)
                        {
                            var Bline = guow.GenericRepository<BusinessLine>().GetByID(item);
                            lstBline.Add(Bline);
                        }
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

                    if (model.Industries_Id != null)
                    {
                        foreach (var item in model.Industries_Id)
                        {
                            var industry = guow.GenericRepository<Industry>().GetByID(item);
                            lstindustry.Add(industry);
                        }
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

                List<Vendor> lstvendor = null;
                if (model.Vendor_Id != null)
                {
                    lstvendor = new List<Vendor>();
                    foreach (var item in model.Vendor_Id)
                    {
                        var vendor = guow.GenericRepository<Vendor>().GetByID(item);
                        lstvendor.Add(vendor);
                    }

                }

                tacticCampaignEntity.BusinessGroups = lstBGroup;
                tacticCampaignEntity.Themes = lsttheme;
                tacticCampaignEntity.BusinessLines = lstBline;
                tacticCampaignEntity.Segments = lstsegment;
                tacticCampaignEntity.Industries = lstindustry;
                tacticCampaignEntity.Geographys = lstgeography;
              
        }

        public bool InsertTacticCampaign(TacticCampaignViewModel model)
        {
            var tacticCampaignEntity = new TacticCampaign();
            ModelToEntity(model, tacticCampaignEntity);            
            guow.GenericRepository<TacticCampaign>().Insert(tacticCampaignEntity);
            UpdateInheritStatus(model.MasterCampaign_Id, model.ChildCampaign_Id);
            return tacticCampaignEntity.Id != 0;
        }
        public void UpdateInheritStatus(int masterId,int childId)
        {
            string inheritStatus = InheritStatus.Draft.ToString();
            var childInherit = guow.GenericRepository<TacticCampaign>().Table.Where(x => x.ChildCampaigns.Id == childId && (x.InheritStatus == InheritStatus.Active.ToString() || x.InheritStatus == InheritStatus.Draft.ToString())).ToList();
            if (childInherit.Count > 0)
            { inheritStatus = InheritStatus.Active.ToString(); }
            else
            { inheritStatus = InheritStatus.Complete.ToString(); }

            ChildCampaign chcm = new ChildCampaign();
            chcm = guow.GenericRepository<ChildCampaign>().GetByID(childId);
            chcm.InheritStatus = inheritStatus;
            guow.GenericRepository<ChildCampaign>().Update(chcm);

            UpdateMasterStatus(masterId);
        }
        public void UpdateMasterStatus(int masterId)
        {
            string inheritStatus = InheritStatus.Draft.ToString();
            var childInherit = guow.GenericRepository<ChildCampaign>().Table.Where(x => x.MasterCampaigns.Id == masterId && (x.InheritStatus == InheritStatus.Active.ToString() || x.InheritStatus == InheritStatus.Draft.ToString())).ToList();
            if (childInherit.Count > 0)
            { inheritStatus = InheritStatus.Active.ToString(); }
            else
            { inheritStatus = InheritStatus.Complete.ToString(); }
            MasterCampaign mcvm = new MasterCampaign();
            mcvm = guow.GenericRepository<MasterCampaign>().GetByID(masterId);
            mcvm.InheritStatus = inheritStatus;
            guow.GenericRepository<MasterCampaign>().Update(mcvm);
        }

        public void Update(TacticCampaignViewModel model)
        {
            var tacticCamp = LoadTacticEntity(model.Id);
            tacticCamp = FlushChildRecords(tacticCamp);
            ModelToEntity(model, tacticCamp);
            guow.GenericRepository<TacticCampaign>().Update(tacticCamp);
            UpdateInheritStatus(model.MasterCampaign_Id, model.ChildCampaign_Id);
        }

        public void Update(TacticCampaign entity)
        {
            guow.GenericRepository<TacticCampaign>().Update(entity);
        }

        private TacticCampaign FlushChildRecords(TacticCampaign tacticCampaignCamp)
        {

            tacticCampaignCamp.Industries.Remove(tacticCampaignCamp.Industries.FirstOrDefault<Industry>());
            tacticCampaignCamp.Segments.Remove(tacticCampaignCamp.Segments.FirstOrDefault<Segment>());
            tacticCampaignCamp.Themes.Remove(tacticCampaignCamp.Themes.FirstOrDefault<Theme>());
            tacticCampaignCamp.Geographys.Remove(tacticCampaignCamp.Geographys.FirstOrDefault<Geography>());
            tacticCampaignCamp.BusinessLines.Remove(tacticCampaignCamp.BusinessLines.FirstOrDefault<BusinessLine>());
            tacticCampaignCamp.BusinessGroups.Remove(tacticCampaignCamp.BusinessGroups.FirstOrDefault<BusinessGroup>());
            if (tacticCampaignCamp.Status != Status.Complete.ToString())
            {
                tacticCampaignCamp.TacticTypes.Remove(tacticCampaignCamp.TacticTypes.FirstOrDefault<TacticType>());
                tacticCampaignCamp.TacticCampaignReachResponses.Remove(tacticCampaignCamp.TacticCampaignReachResponses
                    .FirstOrDefault<TacticCampaignReachResponse>());
            }
            return tacticCampaignCamp;
        }

        private TacticCampaign LoadTacticEntity(int childId)
        {
            return guow.GenericRepository<TacticCampaign>().GetByID(childId);
        }

        public IEnumerable<TacticType> GetTacticType()
        {
            return guow.GenericRepository<TacticType>().GetAll();
        }
        public bool DeleteTacticCampaign(int Id)
        {
            guow.GenericRepository<TacticCampaign>().Delete(Id);
            return true;

        }

        //Deleted last visited
        public void DeleteLastyearVisited()
        {
            var TacticList = GetTacticCampaign()
                .Where(s => s.Status == Status.Draft.ToString() && (s.VisitedDate <= DateTime.Now.AddDays(-2))).ToList();
               // .Where(s => s.Status == "Save Draft" && (s.VisitedDate <= DateTime.Now.AddYears(-1))).ToList();
            if (TacticList.Count > 0)
            {
                foreach (var item in TacticList)
                {
                    var tacticCampaign = GetTacticCampaignById(new TacticCampaignViewModel() {Id = item.Id})
                        .FirstOrDefault();
                    tacticCampaign.IsActive = false;
                    Update(tacticCampaign);

                }
            }
        }

        public void CompleteAfterEndDatePass()
        {
            IList<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAll().Where(t=>t.InheritStatus==InheritStatus.Active.ToString() && t.EndDate<DateTime.Now).ToList();
         
            foreach (var item in tacticCampaign)
            {
                item.InheritStatus = InheritStatus.Complete.ToString();
                Update(item);
                UpdateInheritStatus(item.MasterCampaign_Id, item.ChildCampaigns.Id);
            }
        }
    }
}
