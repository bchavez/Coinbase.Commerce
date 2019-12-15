using System;
using Coinbase.Commerce.Models;
using Newtonsoft.Json.Linq;

namespace Coinbase.Tests
{
   public class WebhookData
   {
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
