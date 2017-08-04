using MRM.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.Database.GenericUnitOfWork;
using MRM.ViewModel;
using MRM.Common;

namespace MRM.Business.Services
{
    public class DigitalTouchpoint : IDigitalTouchpoint
    {
        private GenericUnitOfWork _unitOfWork = null;

        public DigitalTouchpoint()
        {
            _unitOfWork = new GenericUnitOfWork();
        }

        public DigitalViewModel GetbyId(int id)
        {
            Util util = new Util();
            DigitalViewModel digitalViewModel = _unitOfWork.GenericRepository<TacticCampaign>().Table
                .Where(x => x.Id == id).Select(x => new DigitalViewModel
                {
                    TacticCampaign_Id =util.DigitalId(x.Id), //string.Format("T{0}", id.ToString("0000000")),
                    TacticName = x.Name,
                    TacticType_Id = x.TacticType,
                }).FirstOrDefault();
            List<DigitalTouchPointViewModel> model = _unitOfWork.GenericRepository<DigitalTouchPoint>().Table
                .Where(x => x.TacticCampaign.Id == id && x.IsDelete == false).Select(x => new DigitalTouchPointViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                    Medium = x.Medium,
                    Source = x.Source,
                    Term = x.Term,
                    UTM = x.UTM,
                    InheritStatus=x.InheritStatus

                }).ToList();

            digitalViewModel.TacticTypeName = _unitOfWork.GenericRepository<TacticType>().GetByID(digitalViewModel.TacticType_Id).Name;
            digitalViewModel.DigitalTouchPoint = model;
            return digitalViewModel;
        }

        public IEnumerable<DigitalTouchPoint> GetList()
        {
            return _unitOfWork.GenericRepository<DigitalTouchPoint>().GetAll();
        }

        public IList<ICollection<DigitalTouchPoint>> GetList(int tacticId)
        {
            return _unitOfWork.GenericRepository<TacticCampaign>().Table.Where(x => x.Id == tacticId).Select(x => x.DigitalTouchPoint).ToList();
        }

        public DigitalViewModel Insert(List<DigitalTouchPointViewModel> digitalTouchPointViewModel)
        {
            DigitalTouchPoint digitalTouchPoint;
            if (digitalTouchPointViewModel!=null)
            {
                foreach (var item in digitalTouchPointViewModel)
                {
                    digitalTouchPoint = new DigitalTouchPoint();
                    ModelToEntity(item, digitalTouchPoint);
                    if (digitalTouchPoint.Id==0)
                    {
                        _unitOfWork.GenericRepository<DigitalTouchPoint>().Insert(digitalTouchPoint);
                        item.Id = digitalTouchPoint.Id;
                        digitalTouchPoint.UTM = "utm_source=" + item.Source + "&utm_medium=" + item.Medium + "&utm_campaign=" + item.DisplayDigitalId + "&utm_term=" + item.Term + "&utm_content=" + item.Content;
                        _unitOfWork.GenericRepository<DigitalTouchPoint>().Update(digitalTouchPoint);
                    }
                    else
                    {
                        digitalTouchPoint.UTM = "utm_source=" + item.Source + "&utm_medium=" + item.Medium + "&utm_campaign=" + item.DisplayDigitalId + "&utm_term=" + item.Term + "&utm_content=" + item.Content;
                        _unitOfWork.GenericRepository<DigitalTouchPoint>().Update(digitalTouchPoint);
                    }
                    
                   
                }

                return GetbyId(digitalTouchPointViewModel[0].TacticCampaignId);
            }
            return null;
        }

        public void Delete(int tacticCampaignId)
        {

            List<DigitalTouchPoint> digitalTouchPoint=_unitOfWork.GenericRepository<DigitalTouchPoint>().Table.Where(x => x.TacticCampaign.Id == tacticCampaignId).ToList();
            foreach (var item in digitalTouchPoint)
            {
                item.IsDelete = true;
                _unitOfWork.GenericRepository<DigitalTouchPoint>().Update(item);
            }
            
        }
        public void DeleteSingleDigitalPoint(int digitalId)
        {
            DigitalTouchPoint digitalTouchPoint = _unitOfWork.GenericRepository<DigitalTouchPoint>().GetByID(digitalId);
            digitalTouchPoint.IsDelete = true;
            _unitOfWork.GenericRepository<DigitalTouchPoint>().Update(digitalTouchPoint);            
        }
        private void ModelToEntity(DigitalTouchPointViewModel model, DigitalTouchPoint digitalTouchPoint)
        {
            digitalTouchPoint.Id = model.Id;
            digitalTouchPoint.Content = model.Content;
            digitalTouchPoint.CreatedBy = "user";
            digitalTouchPoint.Medium = model.Medium;
            digitalTouchPoint.Source = model.Source;
            digitalTouchPoint.Term = model.Term;
            digitalTouchPoint.InheritStatus = model.InheritStatus;
            digitalTouchPoint.UTM = "utm_source=" + model.Source + "&utm_medium=" + model.Medium + "&utm_campaign=" + model.DisplayDigitalId + "&utm_term=" + model.Term + "&utm_content=" + model.Content;
            digitalTouchPoint.TacticCampaign = _unitOfWork.GenericRepository<TacticCampaign>().GetByID(model.TacticCampaignId);
            digitalTouchPoint.TacticType = _unitOfWork.GenericRepository<TacticType>().GetByID(model.TacticType_Id);

        }
    }
}
