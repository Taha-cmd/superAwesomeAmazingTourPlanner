using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Maps
{
    public interface IMapsApiResponse
    {
        double Distance { get; }
        string ImagePath { get; }
    }
}
