using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vidly.Models
{
    public class Customer
    {
        public int id { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name="name ")]
        public string name { get; set; }
        [Display(Name="Date of birth ")]
        [Min18YearsIfAMember]
        public DateTime? birthdayDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MemberShipType MemberShipType{ get; set; }
        [Display(Name="Membership type ")]
        public byte MemberShipTypeId { get; set; }

    }
}