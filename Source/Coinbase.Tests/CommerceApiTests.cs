using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase.Commerce;
using Coinbase.Commerce.Models;
using FluentAssertions;
using Flurl.Http.Testing;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace Coinbase.Tests
{
   [TestFixture]
   public class CommerceApiTests
   {
      private HttpTest server;
      private CommerceApi com;
      public string apiKey = "DBBD0428-B818-4F53-A5F4-F553DC4C374C";

      [SetUp]
      public void BeforeEachTest()
      {
         server = new HttpTest();

         com = new CommerceApi(apiKey);
      }

      [TearDown]
      public void AfterEachTest()
      {
         EnsureEveryRequestHasCorrectHeaders();
         server.Dispose();
      }

      void EnsureEveryRequestHasCorrectHeaders()
      {
         server.ShouldHaveMadeACall()
            .WithHeader(HeaderNames.Version, CommerceApi.ApiVersionDate)
            .WithHeader(HeaderNames.ApiKey, apiKey)
            .WithHeader("User-Agent", CommerceApi.UserAgent);
      }

      void SetupServerPagedResponse(string pageJson, string dataJson)
      {
         var json = @"{
    ""pagination"": {pageJson},
    ""data"": [{dataJson}]
}
".Replace("{dataJson}", dataJson)
            .Replace("{pageJson}", pageJson);

         server.RespondWith(json);
      }

      void SetupServerSingleResponse(string dataJson)
      {
         var json = @"{
    ""data"": {dataJson}
}
".Replace("{dataJson}", dataJson);

         server.RespondWith(json);
      }


      [Test]
      public async Task can_list_charges()
      {
         SetupServerPagedResponse(Examples.Pagination, Examples.Charge);

         var charges = await com.ListChargesAsync();

         var truth = new PagedResponse<Charge>()
         {
            Pagination = Examples.PaginationModel,
            Data = new[] { Examples.ChargeModel }
         };

         truth.Should().BeEquivalentTo(charges);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/charges")
            .WithVerb(HttpMethod.Get);

      }

      [Test]
      public async Task list_charges_with_different_ordering()
      {
         SetupServerPagedResponse(Examples.Pagination, Examples.Charge);

         await com.ListChargesAsync(ListOrder.Asc, 75, "ffff", "gggg");

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/charges?order=asc&limit=75&starting_after=ffff&ending_before=gggg")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_get_a_single_charge()
      {
         SetupServerSingleResponse(Examples.Charge);

         var charge = await com.GetChargeAsync("66BEOV2A");

         var truth = new Response<Charge>
            {
               Data = Examples.ChargeModel
            };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/charges/66BEOV2A")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_create_a_charge()
      {
         var metadata = @"{
       ""customer_id"": ""id_1005"",
      ""customer_name"": ""Satoshi Nakamoto""
}";

         SetupServerSingleResponse(Examples.ChargeWithMetadata(metadata));


         var newCharge = new CreateCharge
            {
               Name = "The Sovereign Individual",
               Description = "Mastering the Transition to the Information Age",
               LocalPrice = new Money {Amount = 100.00m, Currency = "USD"},
               PricingType = PricingType.FixedPrice,
               Metadata =new JObject
                  {
                     {"customer_id", "id_1005"},
                     {"customer_name", "Satoshi Nakamoto"}
                  }
            };

         var charge = await com.CreateChargeAsync(newCharge);


         var truth = new Response<Charge>
            {
               Data = Examples.ChargeModel
            };
         truth.Data.Metadata = newCharge.Metadata;


         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/charges")
            .WithVerb(HttpMethod.Post);
      }
      
      [Test]
      public async Task can_cancel_a_charge()
      {
         SetupServerSingleResponse(Examples.CancelCharge);

         var charge = await com.CancelChargeAsync("66BEOV2A");

         var truth = new Response<Charge>
            {
               Data = Examples.CancelChargeModel
            };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/charges/66BEOV2A/cancel")
            .WithVerb(HttpMethod.Post)
            .WithRequestBody("");
      }
      
      [Test]
      public async Task can_resolve_a_charge()
      {
         SetupServerSingleResponse(Examples.ResolveCharge);

         var charge = await com.ResolveChargeAsync("66BEOV2A");

         var truth = new Response<Charge>
            {
               Data = Examples.ResolveChargeModel
            };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/charges/66BEOV2A/resolve")
            .WithVerb(HttpMethod.Post)
            .WithRequestBody("");
      }

      [Test]
      public async Task can_list_checkouts()
      {
         SetupServerPagedResponse(Examples.Pagination, Examples.Checkout);

         var checkouts = await com.ListCheckoutsAsync();

         var truth = new PagedResponse<Checkout>
            {
               Pagination = Examples.PaginationModel,
               Data = new[] {Examples.CheckoutModel}
            };

         truth.Should().BeEquivalentTo(checkouts);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/checkouts")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task list_checkouts_with_different_ordering()
      {
         SetupServerPagedResponse(Examples.Pagination, Examples.Checkout);

         await com.ListCheckoutsAsync(ListOrder.Asc, 75, "ffff", "gggg");

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/checkouts?order=asc&limit=75&starting_after=ffff&ending_before=gggg")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_get_a_single_checkout()
      {
         SetupServerSingleResponse(Examples.Checkout);

         var charge = await com.GetCheckoutAsync("30934862-d980-46cb-9402-43c81b0cabd5");

         var truth = new Response<Checkout>
            {
               Data = Examples.CheckoutModel
            };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/checkouts/30934862-d980-46cb-9402-43c81b0cabd5")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_create_a_checkout()
      {
         var requestInfo = @"""email""";
         SetupServerSingleResponse(Examples.CheckoutWithRequestInfo(requestInfo));

         var newCheckout = new CreateCheckout
            {
               Name = "The Sovereign Individual",
               Description = "Mastering the Transition to the Information Age",
               LocalPrice = new Money {Amount = 100.00m, Currency = "USD"},
               PricingType = PricingType.FixedPrice,
               RequestEmail = true
            };

         var checkout = await com.CreateCheckoutAsync(newCheckout);

         var truth = new Response<Checkout>
            {
               Data = Examples.CheckoutModel
            };

         truth.Data.RequestedInfo.Add("email");

         truth.Should().BeEquivalentTo(checkout);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/checkouts")
            .WithVerb(HttpMethod.Post);
      }

      [Test]
      public async Task can_update_a_checkout()
      {
         var requestInfo = @"""email""";
         SetupServerSingleResponse(Examples.CheckoutWithRequestInfo(requestInfo));

         var updateCheckout = new UpdateCheckout
            {
               LocalPrice = new Money {Amount = 200.00m, Currency = "USD"}
            };

         var update = await com.UpdateCheckoutAsync(Examples.CheckoutModel.Id, updateCheckout);

         var truth = new Response<Checkout>
            {
               Data = Examples.CheckoutModel
            };

         truth.Data.RequestedInfo.Add("email");

         truth.Should().BeEquivalentTo(update);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/checkouts/{Examples.CheckoutModel.Id}")
            .WithVerb(HttpMethod.Put);
      }

      [Test]
      public async Task sending_checkout_updates_to_the_server_should_only_be_of_defined_values()
      {
         SetupServerSingleResponse(Examples.Checkout);

         var updateCheckout = new UpdateCheckout
            {
               LocalPrice = new Money { Amount = 200.00m, Currency = "USD" }
            };

         await com.UpdateCheckoutAsync(Examples.CheckoutModel.Id, updateCheckout);

         //client should only send up only explicitly filled out fields.
         var onlyMinimalUpdateSentToServer = @"{""local_price"":{""amount"":200.00,""currency"":""USD""}}";
         server.CallLog.First().RequestBody.Should().Be(onlyMinimalUpdateSentToServer);
      }

      [Test]
      public async Task can_delete_a_checkout()
      {
         server.RespondWith("");
         await com.DeleteCheckoutAsync(Examples.CheckoutModel.Id);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/checkouts/{Examples.CheckoutModel.Id}")
            .WithVerb(HttpMethod.Delete);
      }

      [Test]
      public async Task can_list_events()
      {
         SetupServerPagedResponse(Examples.Pagination, Examples.Event);

         var events = await com.ListEventsAsync();

         var truth = new PagedResponse<Event>()
            {
               Pagination = Examples.PaginationModel,
               Data = new[] { Examples.EventModel }
            };

         truth.Should().BeEquivalentTo(events);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/events")
            .WithVerb(HttpMethod.Get);

      }

      [Test]
      public async Task list_events_with_different_ordering()
      {
         SetupServerPagedResponse(Examples.Pagination, Examples.Event);

         await com.ListEventsAsync(ListOrder.Asc, 75, "ffff", "gggg");

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/events?order=asc&limit=75&starting_after=ffff&ending_before=gggg")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_get_a_single_event()
      {
         SetupServerSingleResponse(Examples.Event);

         var charge = await com.GetEventAsync(Examples.EventModel.Id);

         var truth = new Response<Event>
            {
               Data = Examples.EventModel
            };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/events/{Examples.EventModel.Id}")
            .WithVerb(HttpMethod.Get);
      }

   }
}
