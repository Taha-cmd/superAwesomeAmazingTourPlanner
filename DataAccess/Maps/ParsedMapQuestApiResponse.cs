using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccess.Maps
{
    public class ParsedMapQuestApiResponse
    {
        public double Distance { get; }
        public string Session { get; }
        public int StatusCode { get; }
        public BoundingBox BoundingBox {get;}

        public ParsedMapQuestApiResponse(string response)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, JsonElement>>>(response);
            StatusCode = data["info"]["statuscode"].GetInt32();

            if (StatusCode != 0)
                return;

            Distance = data["route"]["distance"].GetDouble();
            Session = data["route"]["sessionId"].GetString();


            //"boundingBox": {
            //      "lr": {
            //          "lng": 16.373583,
            //          "lat": 47.797558
            //      },
            //      "ul": {
            //          "lng": 13.013587,
            //          "lat": 48.203083
            //       }
            //},

            var box = data["route"]["boundingBox"].EnumerateObject();
            BoundingBox = new BoundingBox();
            box.MoveNext();

            var lr = box.Current.Value.EnumerateObject();

            lr.MoveNext();
            BoundingBox.LR_LNG = lr.Current.Value.GetDouble();

            lr.MoveNext();
            BoundingBox.LR_LAT = lr.Current.Value.GetDouble();

            box.MoveNext();

            var ul = box.Current.Value.EnumerateObject();

            ul.MoveNext();
            BoundingBox.UL_LNG = ul.Current.Value.GetDouble();

            ul.MoveNext();
            BoundingBox.UL_LAT = ul.Current.Value.GetDouble();
        }

    }
}
