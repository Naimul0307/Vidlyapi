using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        
        [Display(Name = "Membership Type")]
        public MemberShipType MemberShipType { get; set; }

        public byte MemberShipTypeId { get; set; }
        
        [Display(Name="Date of Birth")]
        public DateTime? BirthDate { get; set; }

    }
}