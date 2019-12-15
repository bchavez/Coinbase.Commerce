using System;
using System.Collections.Generic;
using Coinbase.Commerce.Models;
using Newtonsoft.Json.Linq;

namespace Coinbase.Tests.ApiTests
{
   public class EventData
   {
      public const string Event = @"{
    ""id"": ""24934862-d980-46cb-9402-43c81b0cdba6"",
    ""type"": ""charge:created"",
    ""api_version"": ""2018-03-22"",
    ""created_at"": ""2017-01-31T20:49:02Z"",
    ""data"": {
      ""code"": ""66BEOV2A"",
      ""name"": ""The Sovereign Individual"",
      ""description"": ""Mastering the Transition to the Information Age"",
      ""hosted_url"": ""https://commerce.coinbase.com/charges/66BEOV2A"",
      ""created_at"": ""2017-01-31T20:49:02Z"",
      ""expires_at"": ""2017-01-31T21:04:02Z"",
      ""timeline"": [
        {
          ""time"": ""2017-01-31T20:49:02Z"",
          ""status"": ""NEW""
        }
      ],
      ""metadata"": {},
      ""pricing_type"": ""no_price"",
      ""payments"": [],
      ""addresses"": {
        ""bitcoin"": ""mymZkiXhQNd6VWWG7VGSVdDX9bKmviti3U"",
        ""ethereum"": ""0x419f91df39951fd4e8acc8f1874b01c0c78ceba6""
      }
    }
}";

      public static Event EventModel =>
         new Event
         {
            Id = "24934862-d980-46cb-9402-43c81b0cdba6",
            Type = "charge:created",
            ApiVersion = "2018-03-22",
            CreatedAt = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
            Data = JObject.FromObject(new
            {
               code = "66BEOV2A",
               name = "The Sovereign Individual",
               description = "Mastering the Transition to the Information Age",
               hosted_url = "https://commerce.coinbase.com/charges/66BEOV2A",
               created_at = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
               expires_at = DateTimeOffset.Parse("2017-01-31T21:04:02Z"),
               timeline = new[]
                        {
                           new {time = DateTimeOffset.Parse("2017-01-31T20:49:02Z"), status = "NEW"}
                        },
               metadata = new JObject(),
               pricing_type = PricingType.NoPrice,
               payments = new Payment[0],
               addresses = new Dictionary<string, string>
                        {
                           {"bitcoin", "mymZkiXhQNd6VWWG7VGSVdDX9bKmviti3U"},
                           {"ethereum", "0x419f91df39951fd4e8acc8f1874b01c0c78ceba6"}
                        }
            })
         };
   }
}
