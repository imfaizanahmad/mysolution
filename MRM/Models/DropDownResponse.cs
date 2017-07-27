using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRM.Models
{
    public class DropDownResponse
    {
        public DropDownResponse()
        {
            List = new List<DropDownValues>();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public IList<DropDownValues> List { get; set; }
    }

    public class DropDownValues
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}