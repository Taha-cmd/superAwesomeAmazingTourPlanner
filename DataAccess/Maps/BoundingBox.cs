using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DataAccess.Maps
{
    public class BoundingBox
    {
        public double UL_LNG { get; set; }
        public double UL_LAT { get; set; }
        public double LR_LNG { get; set; }
        public double LR_LAT { get; set; }

        public override string ToString()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            // use . as a decimal point seperator

            return $"{UL_LAT.ToString(nfi)},{UL_LNG.ToString(nfi)},{LR_LAT.ToString(nfi)},{LR_LNG.ToString(nfi)}";
        }
    }
}
