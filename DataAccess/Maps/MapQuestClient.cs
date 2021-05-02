using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace DataAccess.Maps
{
    public class MapQuestClient : IMapsApiClient
    {
        private readonly string key;
        private readonly string imagesFolderPath;
        private readonly HttpClient httpClient = new HttpClient();
        public MapQuestClient(string key, string imagesFolderPath)
        {
            this.key = key;
            this.imagesFolderPath = imagesFolderPath;
        }
        private string CreateRouteRequest(string from, string to) => $"http://www.mapquestapi.com/directions/v2/route?key={key}&from={from}&to={to}";
        private string CreateStaticMapRequest(double width, double length, int zoom, string sessionId, string boundingBox)
            => $"https://www.mapquestapi.com/staticmap/v5/map?key={key}&size={width},{length}&zoom={zoom}&session={sessionId}&boundingBox={boundingBox}";
        public async Task<MapsApiResponse> GetRouteInformation(string from, string to, bool saveMap = false)
        {
            var routeInfo = await httpClient.GetStringAsync(CreateRouteRequest(from, to));
            return await ParseRouteInfo(routeInfo, saveMap);
        }
        private async Task<MapsApiResponse> ParseRouteInfo(string routeInfo, bool saveMap)
        {
            var data = new ParsedMapQuestApiResponse(routeInfo);
            //Debug.WriteLine(CreateStaticMapRequest(640, 480, 11, data.Session, data.BoundingBox.ToString()));
            if (data.StatusCode != 0)
                return new MapsApiResponse(false);

            string imagePath = null;

            if (saveMap)
            {
                var staticMapResponse = await httpClient.GetByteArrayAsync(CreateStaticMapRequest(640, 480, 11, data.Session, data.BoundingBox.ToString()));
                imagePath = $"{imagesFolderPath}/{Guid.NewGuid()}.jpg";
                File.WriteAllBytes(imagePath, staticMapResponse);
            }

            return new MapsApiResponse(data.Distance, imagePath);
        }
    }
}
