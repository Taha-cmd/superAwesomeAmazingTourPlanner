using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Maps
{
    public interface IMapsApiClient
    {
        Task<IMapsApiResponse> GetRouteInformation(string from, string to);
        Task<bool> RouteExists(string from, string to);
    }
}
