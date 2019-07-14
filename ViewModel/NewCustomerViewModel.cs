using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vidly.Models;

namespace vidly.ViewModel
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MemberShipType> MemberShipType { get; set; }
        public Customer Customer{ get; set; }

    }
}