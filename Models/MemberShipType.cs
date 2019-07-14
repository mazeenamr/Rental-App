using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.Models
{
    public class MemberShipType
    {
        public byte id { get; set; }
        public string name { get; set; }

        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        public static readonly byte unknown = 0;
        public static readonly byte payAsYouGo = 1;

    }
}