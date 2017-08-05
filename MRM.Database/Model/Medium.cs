using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Database.Model
{
    public class DigitalMedium : CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DigitalTouchPoint> DigitalTouchPoint { get; set; }
    }
}
