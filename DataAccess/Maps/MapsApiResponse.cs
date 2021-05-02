using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Maps
{
    public class MapsApiResponse
    {
        public double Distance { get; set; }
        public string ImagePath { get; set; }
        public bool RouteExists { get; set; } = true;

        public MapsApiResponse(double distance, string imagePath)
        {
            Distance = distance;
            ImagePath = imagePath;
        }

        public MapsApiResponse(bool routeExists) => RouteExists = routeExists;
    }
}
