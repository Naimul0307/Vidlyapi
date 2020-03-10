using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyApi.Models;

namespace VidlyApi.ViewModel
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MemberShipType> MemberShipTypes { get; set; }
        public Customer customer { get; set; }
    }
}