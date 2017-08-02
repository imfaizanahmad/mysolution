using MRM.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.Database.GenericUnitOfWork;
using MRM.ViewModel;

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


            DigitalViewModel digitalViewModel = _unitOfWork.GenericRepository<TacticCampaign>().Table
                .Where(x => x.Id == id).Select(x => new DigitalViewModel
                {
                    TacticCampaign_Id = x.Id.ToString(), //string.Format("T{0}", id.ToString("0000000")),
                    TacticName = x.Name,
                    TacticType_Id = x.TacticType,
                }).FirstOrDefault();
            List<DigitalTouchPointViewModel> model = _unitOfWork.GenericRepository<DigitalTouchPoint>().Table
                .Where(x => x.TacticCampaign.Id == id).Select(x => new DigitalTouchPointViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    Medium = x.Medium,
                    Source = x.Source,
                    Term = x.Term,
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
    }
}
