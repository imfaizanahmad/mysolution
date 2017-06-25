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
            if (model.Id == 0)
            {
                tacticCampaignEntity.ChildCampaigns = guow.GenericRepository<ChildCampaign>().GetByID(model.ChildCampaign_Id);
                tacticCampaignEntity.Name = model.Name;
                tacticCampaignEntity.TacticDescription = model.TacticDescription;
                tacticCampaignEntity.StartDate = string.IsNullOrEmpty(model.StartDate) == true ? DateTime.Now : Convert.ToDateTime(model.StartDate);
                tacticCampaignEntity.EndDate = string.IsNullOrEmpty(model.EndDate) == true ? DateTime.Now : Convert.ToDateTime(model.EndDate);
                tacticCampaignEntity.Status = model.Status;
                tacticCampaignEntity.CreatedBy = "user";
                tacticCampaignEntity.TacticType = model.TacticType;
                tacticCampaignEntity.Year = model.Year;
                tacticCampaignEntity.Status = model.Status;
                tacticCampaignEntity.MasterCampaign_Id = model.MasterCampaign_Id;
                tacticCampaignEntity.TacticType = model.TacticType;
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

                tacticCampaignEntity.BusinessGroups = lstBGroup;
                tacticCampaignEntity.Themes = lsttheme;
                tacticCampaignEntity.BusinessLines = lstBline;
                tacticCampaignEntity.Segments = lstsegment;
                tacticCampaignEntity.Industries = lstindustry;
                tacticCampaignEntity.Geographys = lstgeography;
                tacticCampaignEntity.Vendors = lstvendor;

                guow.GenericRepository<TacticCampaign>().Insert(tacticCampaignEntity);
            }
            else
            {
                 tacticCampaignEntity = guow.GenericRepository<TacticCampaign>().GetByID(model.Id);
                 tacticCampaignEntity.Name = model.Name;
                 tacticCampaignEntity.TacticDescription = model.TacticDescription;
                 tacticCampaignEntity.StartDate = string.IsNullOrEmpty(model.StartDate) == true ? DateTime.Now : Convert.ToDateTime(model.StartDate);
                 tacticCampaignEntity.EndDate = string.IsNullOrEmpty(model.EndDate) == true ? DateTime.Now : Convert.ToDateTime(model.EndDate);
                 tacticCampaignEntity.Status = model.Status;
                 tacticCampaignEntity.Year = model.Year;
                 tacticCampaignEntity.Status = model.Status;               
                 tacticCampaignEntity.MasterCampaign_Id = model.MasterCampaign_Id;
                 tacticCampaignEntity.TacticType = model.TacticType;
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

                guow.GenericRepository<TacticCampaign>().Update(tacticCampaignEntity);
            }
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
