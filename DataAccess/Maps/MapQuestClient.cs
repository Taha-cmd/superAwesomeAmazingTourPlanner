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
        public async Task<IMapsApiResponse> GetRouteInformation(string from, string to)
        {
            var routeInfo = await httpClient.GetStringAsync(CreateRouteRequest(from, to));
            return await ParseRouteInfo(routeInfo);
        }
        private async Task<IMapsApiResponse> ParseRouteInfo(string routeInfo)
        {
            var data = new ParsedMapQuestApiResponse(routeInfo);
            //Debug.WriteLine(CreateStaticMapRequest(640, 480, 11, data.Session, data.BoundingBox.ToString()));
            var staticMapResponse = await httpClient.GetByteArrayAsync(CreateStaticMapRequest(640, 480, 11, data.Session, data.BoundingBox.ToString()));

            string imagePath = $"{imagesFolderPath}/{Guid.NewGuid()}.jpg";
            File.WriteAllBytes(imagePath, staticMapResponse);

            return new MapQuestResponse(data.Distance, imagePath);
        }

        public async Task<bool> RouteExists(string from, string to)
        {
            var routeInfo = await httpClient.GetStringAsync(CreateRouteRequest(from, to));
            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, JsonElement>>>(routeInfo);
            return data["info"]["statuscode"].GetInt32() == 0;
        }
    }
}
