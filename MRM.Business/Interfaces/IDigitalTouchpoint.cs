using MRM.Database.Model;
using MRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Business.Interfaces
{
    public interface IDigitalTouchpoint
    {
        IEnumerable<DigitalTouchPoint> GetList();

        IList<ICollection<DigitalTouchPoint>> GetList(int tacticId);

        DigitalViewModel GetbyId(int id);
    }
}
