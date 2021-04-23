using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Maps
{
    public interface IMapsApiClient
    {
        Task<MapsApiResponse> GetRouteInformation(string from, string to, bool saveMap = false);
    }
}
