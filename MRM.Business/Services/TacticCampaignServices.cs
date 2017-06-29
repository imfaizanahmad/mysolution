using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<TacticCampaign> GetTacticCampaign()
        {
            IEnumerable<TacticCampaign> tacticCampaign = guow.GenericRepository<TacticCampaign>().GetAll().ToList();
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

        private void ModelToEntity(TacticCampaignViewModel model, TacticCampaign tacticCampaignEntity)
        {
            tacticCampaignEntity.ChildCampaigns = guow.GenericRepository<ChildCampaign>().GetByID(model.ChildCampaign_Id);
            tacticCampaignEntity.ChildCampaigns.MasterCampaigns = guow.GenericRepository<MasterCampaign>().GetByID(model.MasterCampaign_Id);
            tacticCampaignEntity.Name = model.Name;
            tacticCampaignEntity.TacticDescription = model.TacticDescription;
            tacticCampaignEntity.StartDate = model.StartDate;
            tacticCampaignEntity.EndDate = model.EndDate;
            tacticCampaignEntity.Status = model.Status;
            tacticCampaignEntity.CreatedBy = "user";
            tacticCampaignEntity.Year = model.Year;
            tacticCampaignEntity.Status = model.Status;
            tacticCampaignEntity.Vendor = model.Vendor;
            tacticCampaignEntity.MasterCampaign_Id = model.MasterCampaign_Id;
            tacticCampaignEntity.ReachR1Goal = model.ReachR1Goal;
            tacticCampaignEntity.ReachR1Low = model.ReachR1Low;
            tacticCampaignEntity.ReachR1High = model.ReachR1High;
            tacticCampaignEntity.ReachR11Goal = model.ReachR11Goal;
            tacticCampaignEntity.ReachR12Low = model.ReachR12Low;
            tacticCampaignEntity.ReachR13High = model.ReachR13High;
            tacticCampaignEntity.ResponseR1Goal = model.ResponseR1Goal;
            tacticCampaignEntity.ResponseR1Low = model.ResponseR1Low;
            tacticCampaignEntity.ResponseR1High = model.ResponseR1High;
            tacticCampaignEntity.ResponseR21Goal = model.ResponseR21Goal;
            tacticCampaignEntity.ResponseR22Low = model.ResponseR22Low;
            tacticCampaignEntity.ResponseR23High = model.ResponseR23High;
            tacticCampaignEntity.EfficiencyE1Goal = model.EfficiencyE1Goal;
            tacticCampaignEntity.EfficiencyE1Low = model.EfficiencyE1Low;
            tacticCampaignEntity.EfficiencyE1High = model.EfficiencyE1High;
            tacticCampaignEntity.EfficiencyE11Goal = model.EfficiencyE11Goal;
            tacticCampaignEntity.EfficiencyE12Low = model.EfficiencyE12Low;
            tacticCampaignEntity.EfficiencyE13High = model.EfficiencyE13High;

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

            tacticCampaignEntity.BusinessGroups = lstBGroup;
            tacticCampaignEntity.Themes = lsttheme;
            tacticCampaignEntity.BusinessLines = lstBline;
            tacticCampaignEntity.Segments = lstsegment;
            tacticCampaignEntity.Industries = lstindustry;
            tacticCampaignEntity.Geographys = lstgeography;
            //tacticCampaignEntity.Vendors = lstvendor;
            tacticCampaignEntity.TacticTypes = lstTacticType;
        }

        public bool InsertTacticCampaign(TacticCampaignViewModel model)
        {
            var tacticCampaignEntity = new TacticCampaign();
            ModelToEntity(model, tacticCampaignEntity);
            guow.GenericRepository<TacticCampaign>().Insert(tacticCampaignEntity);
            return tacticCampaignEntity.Id != 0;
        }


        public void Update(TacticCampaignViewModel model)
        {
            var tacticCamp = LoadTacticEntity(model.Id);
            tacticCamp = FlushChildRecords(tacticCamp);
            ModelToEntity(model, tacticCamp);
            guow.GenericRepository<TacticCampaign>().Update(tacticCamp);
        }

        public void Update(TacticCampaign entity)
        {
            guow.GenericRepository<TacticCampaign>().Update(entity);
        }

        private TacticCampaign FlushChildRecords(TacticCampaign  tacticCampaignCamp)
        {

            tacticCampaignCamp.Industries.Remove(tacticCampaignCamp.Industries.FirstOrDefault<Industry>());
            tacticCampaignCamp.Segments.Remove(tacticCampaignCamp.Segments.FirstOrDefault<Segment>());
            tacticCampaignCamp.Themes.Remove(tacticCampaignCamp.Themes.FirstOrDefault<Theme>());
            tacticCampaignCamp.Geographys.Remove(tacticCampaignCamp.Geographys.FirstOrDefault<Geography>());
            tacticCampaignCamp.BusinessLines.Remove(tacticCampaignCamp.BusinessLines.FirstOrDefault<BusinessLine>());
            tacticCampaignCamp.BusinessGroups.Remove(tacticCampaignCamp.BusinessGroups.FirstOrDefault<BusinessGroup>());
            //tacticCampaignCamp.Vendors.Remove(tacticCampaignCamp.Vendors.FirstOrDefault<Vendor>());
            tacticCampaignCamp.TacticTypes.Remove(tacticCampaignCamp.TacticTypes.FirstOrDefault<TacticType>());
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
    }
}
