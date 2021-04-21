using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Maps
{
    public class MapQuestResponse : IMapsApiResponse
    {
        public double Distance { get; set; }
        public string ImagePath { get; set; }

        public MapQuestResponse(double distance, string imagePath)
        {
            Distance = distance;
            ImagePath = imagePath;
        }

        public MapQuestResponse()
        {

        }
    }
}
