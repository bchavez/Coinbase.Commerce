using System;
using System.Collections.Generic;
using Coinbase.Commerce.Models;
using Newtonsoft.Json.Linq;

namespace Coinbase.Tests.ApiTests
{
   public static class ChargeData
   {
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


      public const string CancelCharge = @"{
    ""id"": ""f765421f2-1451-fafb-a513-aac6c819fba9"",
    ""resource"": ""charge"",
    ""code"": ""66BEOV2A"",
    ""name"": ""The Sovereign Individual"",
    ""description"": ""Mastering the Transition to the Information Age"",
    ""logo_url"": ""https://commerce.coinbase.com/charges/ybjknds.png"",
    ""hosted_url"": ""https://commerce.coinbase.com/charges/66BEOV2A"",
    ""created_at"": ""2017-01-31T20:49:02Z"",
    ""expires_at"": ""2017-01-31T21:49:02Z"",
    ""timeline"": [
      {
        ""time"": ""2017-01-31T20:49:02Z"",
        ""status"": ""NEW""
      },
      {
        ""time"": ""2017-01-31T20:52:02Z"",
        ""status"": ""CANCELED""      
      }
    ],
    ""metadata"": {
      ""customer_id"": ""id_1005"",
      ""customer_name"": ""Satoshi Nakamoto""
    },
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
    },
    ""redirect_url"": ""https://charge/completed/page"",
    ""cancel_url"": ""https://charge/canceled/page"",
  }";

      public static Charge CancelChargeModel => new Charge
         {
            Id = "f765421f2-1451-fafb-a513-aac6c819fba9",
            Resource = "charge",
            Code = "66BEOV2A",
            Name = "The Sovereign Individual",
            Description = "Mastering the Transition to the Information Age",
            LogoUrl = "https://commerce.coinbase.com/charges/ybjknds.png",
            HostedUrl = "https://commerce.coinbase.com/charges/66BEOV2A",
            CreatedAt = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
            ExpiresAt = DateTimeOffset.Parse("2017-01-31T21:49:02Z"),
            Timeline = new[]
               {
                  new Timeline
                     {
                        Time = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
                        Status = "NEW"
                     },
                  new Timeline
                     {
                        Time = DateTimeOffset.Parse("2017-01-31T20:52:02Z"),
                        Status = "CANCELED"
                     }
               },
            Metadata = new JObject
               {
                  {"customer_id", "id_1005"},
                  {"customer_name", "Satoshi Nakamoto"}
               },
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
               },
            ExtraJson =
               {
                  {"redirect_url", "https://charge/completed/page"},
                  {"cancel_url", "https://charge/canceled/page"},
               }
         };

      public const string ResolveCharge = @"{
    ""id"": ""f765421f2-1451-fafb-a513-aac6c819fba9"",
    ""resource"": ""charge"",
    ""code"": ""66BEOV2A"",
    ""name"": ""The Sovereign Individual"",
    ""description"": ""Mastering the Transition to the Information Age"",
    ""logo_url"": ""https://commerce.coinbase.com/charges/ybjknds.png"",
    ""hosted_url"": ""https://commerce.coinbase.com/charges/66BEOV2A"",
    ""created_at"": ""2017-01-31T20:49:02Z"",
    ""expires_at"": ""2017-01-31T21:49:02Z"",
    ""timeline"": [
      {
        ""time"": ""2017-01-31T20:49:02Z"",
        ""status"": ""NEW""
      },
      {
       ""status"": ""EXPIRED"",
        ""time"": ""2017-01-31T21:49:02Z""
      },
      {
        ""status"": ""UNRESOLVED"",
        ""time"": ""2017-01-31T22:00:00Z"",
        ""context"": ""DELAYED""
      },
      {
        ""status"": ""RESOLVED"",
        ""time"": ""2017-01-31T23:00:00Z""
      }
    ],
    ""metadata"": {
      ""customer_id"": ""id_1005"",
      ""customer_name"": ""Satoshi Nakamoto""
    },
    ""pricing_type"": ""fixed_price"",
    ""pricing"": {
      ""local"": { ""amount"": ""100.00"", ""currency"": ""USD"" },
      ""bitcoin"": { ""amount"": ""1.00"", ""currency"": ""BTC"" },
      ""ethereum"": { ""amount"": ""10.00"", ""currency"": ""ETH"" }
    },
    ""payments"": [
      {
        ""network"": ""ethereum"",
        ""transaction_id"": ""0x0000000000000000000000000000000000000000000000000000000000000000"",
        ""status"": ""CONFIRMED"",
        ""detected_at"": ""2017-01-31T22:00:00Z"",
        ""value"": {
          ""local"": { ""amount"": ""100.0"", ""currency"": ""USD"" },
          ""crypto"": { ""amount"": ""10.00"", ""currency"": ""ETH"" }
        },
        ""block"": {
          ""height"": 100,
          ""hash"": ""0x0000000000000000000000000000000000000000000000000000000000000000"",
          ""confirmations_accumulated"": 8,
          ""confirmations_required"": 2
        }
      }
    ],
    ""addresses"": {
      ""bitcoin"": ""mymZkiXhQNd6VWWG7VGSVdDX9bKmviti3U"",
      ""ethereum"": ""0x419f91df39951fd4e8acc8f1874b01c0c78ceba6""
    },
    ""redirect_url"": ""https://charge/completed/page"",
    ""cancel_url"": ""https://charge/canceled/page"",
  }";

      public static Charge ResolveChargeModel => new Charge
         {
            Id = "f765421f2-1451-fafb-a513-aac6c819fba9",
            Resource = "charge",
            Code = "66BEOV2A",
            Name = "The Sovereign Individual",
            Description = "Mastering the Transition to the Information Age",
            LogoUrl = "https://commerce.coinbase.com/charges/ybjknds.png",
            HostedUrl = "https://commerce.coinbase.com/charges/66BEOV2A",
            CreatedAt = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
            ExpiresAt = DateTimeOffset.Parse("2017-01-31T21:49:02Z"),
            Timeline = new[]
               {
                  new Timeline
                     {
                        Time = DateTimeOffset.Parse("2017-01-31T20:49:02Z"),
                        Status = "NEW"
                     },
                  new Timeline
                     {
                        Time = DateTimeOffset.Parse("2017-01-31T21:49:02Z"),
                        Status = "EXPIRED"
                     },
                  new Timeline
                     {
                        Time = DateTimeOffset.Parse("2017-01-31T22:00:00Z"),
                        Status = "UNRESOLVED",
                        Context = "DELAYED"
                     },
                  new Timeline
                     {
                        Time = DateTimeOffset.Parse("2017-01-31T23:00:00Z"),
                        Status = "RESOLVED"
                     }
               },
            Metadata = new JObject
               {
                  {"customer_id", "id_1005"},
                  {"customer_name", "Satoshi Nakamoto"}
               },
            PricingType = PricingType.FixedPrice,
            Pricing = new Dictionary<string, Money>
               {
                  {"local", new Money {Amount = 100.0m, Currency = "USD"}},
                  {"bitcoin", new Money {Amount = 1.0m, Currency = "BTC"}},
                  {"ethereum", new Money {Amount = 10.0m, Currency = "ETH"}}
               },
            Payments = new[]
               {
                  new Payment
                     {
                        Network = "ethereum",
                        TransactionId = "0x0000000000000000000000000000000000000000000000000000000000000000",
                        Status = "CONFIRMED",
                        Value = new Dictionary<string, Money>
                           {
                              { "local", new Money(){ Amount = 100.0m, Currency = "USD"}},
                              { "crypto", new Money(){ Amount = 10.0m, Currency = "ETH"}}
                           },
                        Block = new Block
                           {
                              Height = 100,
                              Hash = "0x0000000000000000000000000000000000000000000000000000000000000000",
                              ConfirmationsAccumulated = 8,
                              ConfirmationsRequired = 2
                           },
                        ExtraJson =
                           {
                              { "detected_at", DateTimeOffset.Parse("2017-01-31T22:00:00Z")}
                           }
                     }
               },
            Addresses = new Dictionary<string, string>
               {
                  {"bitcoin", "mymZkiXhQNd6VWWG7VGSVdDX9bKmviti3U"},
                  {"ethereum", "0x419f91df39951fd4e8acc8f1874b01c0c78ceba6"}
               },
            ExtraJson =
               {
                  {"redirect_url", "https://charge/completed/page"},
                  {"cancel_url", "https://charge/canceled/page"},
               }
         };
   }
}
