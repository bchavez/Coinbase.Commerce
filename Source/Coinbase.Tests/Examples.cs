using System;
using System.Collections.Generic;
using Coinbase.Commerce.Models;
using Newtonsoft.Json.Linq;

namespace Coinbase.Tests
{
   public static class Examples
   {
      public const string Pagination = @"
      {
        ""order"": ""desc"",
        ""starting_after"": null,
        ""ending_before"": null,
        ""total"": 25,
        ""yielded"": 20,
        ""limit"": 20,
        ""previous_uri"": null,
        ""next_uri"": ""https://api.commerce.coinbase.com/checkouts?limit=20&starting_after=fb6721f2-1622-48f0-b713-aac6c819b67a"",
        ""cursor_range"": [""a76721f2-1611-48fb-a513-aac6c819a9d6"", ""fb6721f2-1622-48f0-b713-aac6c819b67a""]
    }";

      public static Pagination PaginationModel => new Pagination
         {
            Order = ListOrder.Desc,
            StartingAfter = null,
            EndingBefore = null,
            Total = 25,
            Yielded = 20,
            Limit = 20,
            PreviousUri = null,
            NextUri = "https://api.commerce.coinbase.com/checkouts?limit=20&starting_after=fb6721f2-1622-48f0-b713-aac6c819b67a",
            CursorRange = new[] {"a76721f2-1611-48fb-a513-aac6c819a9d6", "fb6721f2-1622-48f0-b713-aac6c819b67a"}
         };

      public const string Charge = @"
{
      ""code"": ""66BEOV2A"",
      ""name"": ""The Sovereign Individual"",
      ""description"": ""Mastering the Transition to the Information Age"",
      ""logo_url"": ""https://commerce.coinbase.com/charges/ybjknds.png"",
      ""hosted_url"": ""https://commerce.coinbase.com/charges/66BEOV2A"",
      ""created_at"": ""2017-01-31T20:49:02Z"",
      ""expires_at"": ""2017-01-31T21:04:02Z"",
      ""checkout"": {
          ""id"": ""a76721f2-1611-48fb-a513-aac6c819a9d6""
      },
      ""timeline"": [
        {
          ""time"": ""2017-01-31T20:49:02Z"",
          ""status"": ""NEW""
        }
      ],
      ""metadata"": {},
      ""pricing_type"": ""fixed_price"",
      ""pricing"": {
        ""local"": { ""amount"": ""100.00"", ""currency"": ""USD"" },
        ""bitcoin"": { ""amount"": ""1.00"", ""currency"": ""BTC"" },
        ""ethereum"": { ""amount"": ""10.00"", ""currency"": ""ETH"" }
      },
      ""payments"": [],
      ""addresses"": {
        ""bitcoin"": ""mymZkiXhQNd6VWWG7VGSVdDX9bKmviti3U"",
        ""ethereum"": ""0x419f91df39951fd4e8acc8f1874b01c0c78ceba6""
      }
    }
";

      public static string ChargeWithMetadata(string jsonMetadata)
      {
         return Charge.Replace(@"""metadata"": {}", $@"""metadata"": {jsonMetadata}");
      }

      public static Charge ChargeModel => new Charge
         {
            Code = "66BEOV2A",
            Name = "The Sovereign Individual",
            Description = "Mastering the Transition to the Information Age",
            LogoUrl = "https://commerce.coinbase.com/charges/ybjknds.png",
            HostedUrl = "https://commerce.coinbase.com/charges/66BEOV2A",
            CreatedAt = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
            ExpiresAt = DateTimeOffset.Parse("2017-01-31T21:04:02Z"),
            Checkout = new Checkout {Id = "a76721f2-1611-48fb-a513-aac6c819a9d6"},
            Timeline = new[]
               {
                  new Timeline
                     {
                        Time = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
                        Status = "NEW"
                     }
               },
            Metadata = new JObject(),
            PricingType = PricingType.FixedPrice,
            Pricing = new Dictionary<string, Money>
               {
                  {"local", new Money {Amount = 100.0m, Currency = "USD"}},
                  {"bitcoin", new Money {Amount = 1.0m, Currency = "BTC"}},
                  {"ethereum", new Money {Amount = 10.0m, Currency = "ETH"}}
               },
            Payments = new Payment[0],
            Addresses = new Dictionary<string, string>
               {
                  {"bitcoin", "mymZkiXhQNd6VWWG7VGSVdDX9bKmviti3U"},
                  {"ethereum", "0x419f91df39951fd4e8acc8f1874b01c0c78ceba6"}
               }

         };

      public const string Checkout = @"{
    ""id"": ""30934862-d980-46cb-9402-43c81b0cabd5"",
    ""name"": ""The Sovereign Individual"",
    ""description"": ""Mastering the Transition to the Information Age"",
    ""logo_url"": ""https://commerce.coinbase.com/charges/ybjknds.png"",
    ""requested_info"": [],
    ""pricing_type"": ""fixed_price"",
    ""local_price"": { ""amount"": ""100.0"", ""currency"": ""USD"" }
}";

      public static string CheckoutWithRequestInfo(string jsonRequestInfo) =>
         Checkout.Replace(@"""requested_info"": []", $@"""requested_info"": [{jsonRequestInfo}]");
      public static Checkout CheckoutModel => new Checkout
         {
            Id = "30934862-d980-46cb-9402-43c81b0cabd5",
            Name = "The Sovereign Individual",
            Description = "Mastering the Transition to the Information Age",
            LogoUrl = "https://commerce.coinbase.com/charges/ybjknds.png",
            RequestedInfo = new HashSet<string> {},
            PricingType = PricingType.FixedPrice,
            LocalPrice = new Money {Amount = 100.0m, Currency = "USD"}
         };


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

      public const string WebhookHeaderSignature = "e337ef2d998015288b742d2d43024088fedf8333dc55043a338fd58eff1b282b";
      public const string Webhook = @"{""attempt_number"":1,""event"":{""api_version"":""2018-03-22"",""created_at"":""2018-04-04T23:49:00Z"",""data"":{""code"":""8EKMDPVQ"",""name"":""Candy Bar"",""pricing"":{""local"":{""amount"":""1.00"",""currency"":""USD""},""bitcoin"":{""amount"":""0.00014691"",""currency"":""BTC""},""ethereum"":{""amount"":""0.002632000"",""currency"":""ETH""},""litecoin"":{""amount"":""0.00841928"",""currency"":""LTC""},""bitcoincash"":{""amount"":""0.00153559"",""currency"":""BCH""}},""metadata"":{""customerId"":""ccaf106d-7fd1-4b8e-b409-0fa345e4d82b""},""payments"":[],""timeline"":[{""time"":""2018-04-04T23:49:00Z"",""status"":""NEW""}],""addresses"":{""bitcoin"":""1Fir6DxwEbQdLWeDMYrqgpnYrs2mDLxobc"",""ethereum"":""0x116462dd6260ecc1b10a79f07bb30af10affc886"",""litecoin"":""Lh8He9HadDszxmY33GTngqQYLDSHaPRLaW"",""bitcoincash"":""qqs2llv7yz76d7c5kkjnpazzllwq7sq29546zt5edc""},""created_at"":""2018-04-04T23:49:00Z"",""expires_at"":""2018-04-05T00:04:00Z"",""hosted_url"":""https://commerce.coinbase.com/charges/8EKMDPVQ"",""description"":""Sweet Tasting Chocolate"",""pricing_type"":""fixed_price""},""id"":""f6972c57-c100-4e64-b47c-193adecfadc6"",""type"":""charge:created""},""id"":""0d0cee27-8ffb-405d-9d05-3326dd4bcb7d"",""scheduled_for"":""2018-04-04T23:49:00Z""}";

      public static Webhook WebhookModel =>
         new Webhook
            {
               AttemptNumber = 1,
               Id = "0d0cee27-8ffb-405d-9d05-3326dd4bcb7d",
               ScheduledFor = DateTimeOffset.Parse("2018-04-04T23:49:00Z"),
               Event = new Event
                  {
                     ApiVersion = "2018-03-22",
                     CreatedAt = DateTimeOffset.Parse("2018-04-04T23:49:00Z"),
                     Id = "f6972c57-c100-4e64-b47c-193adecfadc6",
                     Type = "charge:created",
                     Data = new JObject
                        {
                           {"code", "8EKMDPVQ"},
                           {"name", "Candy Bar"},
                           {
                              "pricing", new JObject
                                 {
                                    {
                                       "local", new JObject
                                          {
                                             {"amount", "1.00"},
                                             {"currency", "USD"}
                                          }
                                    },
                                    {
                                       "bitcoin", new JObject
                                          {
                                             {"amount", "0.00014691"},
                                             {"currency", "BTC"}
                                          }
                                    },
                                    {
                                       "ethereum", new JObject
                                          {
                                             {"amount", "0.002632000"},
                                             {"currency", "ETH"}
                                          }
                                    },
                                    {
                                       "litecoin", new JObject
                                          {
                                             {"amount", "0.00841928"},
                                             {"currency", "LTC"}
                                          }
                                    },
                                    {
                                       "bitcoincash", new JObject
                                          {
                                             {"amount", "0.00153559"},
                                             {"currency", "BCH"}
                                          }
                                    }
                                 }
                           },
                           {
                              "metadata", new JObject
                                 {
                                    {"customerId", "ccaf106d-7fd1-4b8e-b409-0fa345e4d82b"},
                                 }
                           },
                           {"payments", new JArray()},
                           {
                              "timeline", new JArray
                                 {
                                    new JObject
                                       {
                                          {"time", "2018-04-04T23:49:00Z"},
                                          {"status", "NEW"},
                                       }
                                 }
                           },
                           {
                              "addresses", new JObject
                                 {
                                    {"bitcoin", "1Fir6DxwEbQdLWeDMYrqgpnYrs2mDLxobc"},
                                    {"ethereum", "0x116462dd6260ecc1b10a79f07bb30af10affc886"},
                                    {"litecoin", "Lh8He9HadDszxmY33GTngqQYLDSHaPRLaW"},
                                    {"bitcoincash", "qqs2llv7yz76d7c5kkjnpazzllwq7sq29546zt5edc"}
                                 }
                           },
                           {"created_at", "2018-04-04T23:49:00Z"},
                           {"expires_at", "2018-04-05T00:04:00Z"},
                           {"hosted_url", "https://commerce.coinbase.com/charges/8EKMDPVQ"},
                           {"description", "Sweet Tasting Chocolate"},
                           {"pricing_type", "fixed_price"}
                        }

                  }
            };
   }
}