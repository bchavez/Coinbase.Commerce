using System;
using Coinbase.Commerce.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Coinbase.Tests.GitHubIssues
{
   [TestFixture]
   public class Issue12
   {
      public const string Webhook = @"{
  ""attempt_number"": 1,
  ""event"": {
    ""api_version"": ""2018-03-22"",
    ""created_at"": ""2019-12-19T16:18:40Z"",
    ""data"": {
      ""id"": ""foo"",
      ""code"": ""foo"",
      ""name"": ""foo"",
      ""pricing"": {
        ""usdc"": {
          ""amount"": ""1275.000000"",
          ""currency"": ""USDC""
        },
        ""local"": {
          ""amount"": ""1275.00"",
          ""currency"": ""USD""
        },
        ""bitcoin"": {
          ""amount"": ""0.17824340"",
          ""currency"": ""BTC""
        },
        ""ethereum"": {
          ""amount"": ""9.982384000"",
          ""currency"": ""ETH""
        },
        ""litecoin"": {
          ""amount"": ""31.90291505"",
          ""currency"": ""LTC""
        },
        ""bitcoincash"": {
          ""amount"": ""6.76410515"",
          ""currency"": ""BCH""
        }
      },
      ""logo_url"": ""foo"",
      ""metadata"": {
        ""DepositId"": ""111""
      },
      ""payments"": [
        {
          ""block"": {
            ""hash"": null,
            ""height"": null,
            ""confirmations"": 0,
            ""confirmations_required"": 1
          },
          ""value"": {
            ""local"": {
              ""amount"": ""1274.67"",
              ""currency"": ""USD""
            },
            ""crypto"": {
              ""amount"": ""0.17819718"",
              ""currency"": ""BTC""
            }
          },
          ""status"": ""PENDING"",
          ""network"": ""bitcoin"",
          ""detected_at"": ""2019-12-19T16:18:40Z"",
          ""transaction_id"": ""foo""
        }
      ],
      ""resource"": ""charge"",
      ""timeline"": [
        {
          ""time"": ""2019-12-19T16:18:11Z"",
          ""status"": ""NEW""
        },
        {
          ""time"": ""2019-12-19T16:18:40Z"",
          ""status"": ""PENDING"",
          ""payment"": {
            ""network"": ""bitcoin"",
            ""transaction_id"": ""foo""
          }
        }
      ],
      ""addresses"": {
        ""usdc"": ""foo"",
        ""bitcoin"": ""foo"",
        ""ethereum"": ""foo"",
        ""litecoin"": ""foo"",
        ""bitcoincash"": ""foo""
      },
      ""created_at"": ""2019-12-19T16:18:11Z"",
      ""expires_at"": ""2019-12-19T17:18:11Z"",
      ""hosted_url"": ""https:\/\/commerce.coinbase.com\/charges\/foo"",
      ""description"": ""foo"",
      ""pricing_type"": ""fixed_price""
    },
    ""id"": ""foo"",
    ""resource"": ""event"",
    ""type"": ""charge:pending""
  },
  ""id"": ""foo"",
  ""scheduled_for"": ""2019-12-19T16:18:40Z""
}";


      public static Webhook WebhookModel =>
         new Webhook
            {
               AttemptNumber = 1,
               Id = "foo",
               ScheduledFor = DateTimeOffset.Parse("2019-12-19T16:18:40Z"),
               Event = new Event
                  {
                     ApiVersion = "2018-03-22",
                     CreatedAt = DateTimeOffset.Parse("2019-12-19T16:18:40Z"),
                     Id = "foo",
                     Resource = "event",
                     Type = "charge:pending",
                     Data = new JObject
                        {
                           {"id", "foo"},
                           {"code", "foo"},
                           {"name", "foo"},
                           {
                              "pricing", new JObject
                                 {
                                    {
                                       "usdc", new JObject
                                          {
                                             {"amount", "1275.000000"},
                                             {"currency", "USDC"}
                                          }
                                    },
                                    {
                                       "local", new JObject
                                          {
                                             {"amount", "1275.00"},
                                             {"currency", "USD"}
                                          }
                                    },
                                    {
                                       "bitcoin", new JObject
                                          {
                                             {"amount", "0.17824340"},
                                             {"currency", "BTC"}
                                          }
                                    },
                                    {
                                       "ethereum", new JObject
                                          {
                                             {"amount", "9.982384000"},
                                             {"currency", "ETH"}
                                          }
                                    },
                                    {
                                       "litecoin", new JObject
                                          {
                                             {"amount", "31.90291505"},
                                             {"currency", "LTC"}
                                          }
                                    },
                                    {
                                       "bitcoincash", new JObject
                                          {
                                             {"amount", "6.76410515"},
                                             {"currency", "BCH"}
                                          }
                                    }
                                 }
                           },
                           {"logo_url", "foo"},
                           {
                              "metadata", new JObject
                                 {
                                    {"DepositId", "111"},
                                 }
                           },
                           {
                              "payments", new JArray()
                                 {
                                    new JObject()
                                       {
                                          {
                                             "block", new JObject
                                                {
                                                   {"hash", null},
                                                   {"height", null},
                                                   {"confirmations", 0},
                                                   {"confirmations_required", 1}
                                                }
                                          },
                                          {
                                             "value", new JObject
                                                {
                                                   {
                                                      "local", new JObject()
                                                         {
                                                            {"amount", "1274.67"},
                                                            {"currency", "USD"},
                                                         }
                                                   },
                                                   {
                                                      "crypto", new JObject()
                                                         {
                                                            {"amount", "0.17819718"},
                                                            {"currency", "BTC"},
                                                         }
                                                   }
                                                }
                                          },
                                          {"status", "PENDING"},
                                          {"network", "bitcoin"},
                                          {"detected_at", "2019-12-19T16:18:40Z"},
                                          {"transaction_id", "foo"},
                                       }
                                 }
                           },
                           {"resource", "charge"},
                           {
                              "timeline", new JArray
                                 {
                                    new JObject
                                       {
                                          {"time", "2019-12-19T16:18:11Z"},
                                          {"status", "NEW"},
                                       },
                                    new JObject
                                       {
                                          {"time", "2019-12-19T16:18:40Z"},
                                          {"status", "PENDING"},
                                          {
                                             "payment", new JObject()
                                                {
                                                   {"network", "bitcoin"},
                                                   {"transaction_id", "foo"}
                                                }
                                          }
                                       }
                                 }
                           },
                           {
                              "addresses", new JObject
                                 {
                                    {"usdc", "foo"},
                                    {"bitcoin", "foo"},
                                    {"ethereum", "foo"},
                                    {"litecoin", "foo"},
                                    {"bitcoincash", "foo"}
                                 }
                           },
                           {"created_at", "2019-12-19T16:18:11Z"},
                           {"expires_at", "2019-12-19T17:18:11Z"},
                           {"hosted_url", "https://commerce.coinbase.com/charges/foo"},
                           {"description", "foo"},
                           {"pricing_type", "fixed_price"}
                        }

                  }
            };

      [Test]
      public void can_deserialize_webhook_with_null_block_height()
      {
         var callback = JsonConvert.DeserializeObject<Webhook>(Webhook);
         callback.Should().BeEquivalentTo(WebhookModel);

         var charge = callback.Event.DataAs<Charge>();
         charge.Should().NotBeNull();
      }
   }
}
