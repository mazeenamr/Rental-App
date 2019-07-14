using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.Dtos
{
    public class customerDtos
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime? birthdayDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        public MemberShipTypeDtos MemberShipType { get; set; }
        public byte MemberShipTypeId { get; set; }
        

    }
}