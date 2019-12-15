using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Coinbase.Tests.ApiTests
{
   public class CheckoutTests : CommerceApiTests
   {
      [Test]
      public async Task can_list_checkouts()
      {
         SetupServerPagedResponse(PageData.Pagination, CheckoutData.Checkout);

         var checkouts = await api.ListCheckoutsAsync();

         var truth = new PagedResponse<Checkout>
         {
            Pagination = PageData.PaginationModel,
            Data = new[] { CheckoutData.CheckoutModel }
         };

         truth.Should().BeEquivalentTo(checkouts);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/checkouts")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task list_checkouts_with_different_ordering()
      {
         SetupServerPagedResponse(PageData.Pagination, CheckoutData.Checkout);

         await api.ListCheckoutsAsync(ListOrder.Asc, 75, "ffff", "gggg");

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/checkouts?order=asc&limit=75&starting_after=ffff&ending_before=gggg")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_get_a_single_checkout()
      {
         SetupServerSingleResponse(CheckoutData.Checkout);

         var charge = await api.GetCheckoutAsync("30934862-d980-46cb-9402-43c81b0cabd5");

         var truth = new Response<Checkout>
         {
            Data = CheckoutData.CheckoutModel
         };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/checkouts/30934862-d980-46cb-9402-43c81b0cabd5")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_create_a_checkout()
      {
         var requestInfo = @"""email""";
         SetupServerSingleResponse(CheckoutData.CheckoutWithRequestInfo(requestInfo));

         var newCheckout = new CreateCheckout
         {
            Name = "The Sovereign Individual",
            Description = "Mastering the Transition to the Information Age",
            LocalPrice = new Money { Amount = 100.00m, Currency = "USD" },
            PricingType = PricingType.FixedPrice,
            RequestEmail = true
         };

         var checkout = await api.CreateCheckoutAsync(newCheckout);

         var truth = new Response<Checkout>
         {
            Data = CheckoutData.CheckoutModel
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
         SetupServerSingleResponse(CheckoutData.CheckoutWithRequestInfo(requestInfo));

         var updateCheckout = new UpdateCheckout
         {
            LocalPrice = new Money { Amount = 200.00m, Currency = "USD" }
         };

         var update = await api.UpdateCheckoutAsync(CheckoutData.CheckoutModel.Id, updateCheckout);

         var truth = new Response<Checkout>
         {
            Data = CheckoutData.CheckoutModel
         };

         truth.Data.RequestedInfo.Add("email");

         truth.Should().BeEquivalentTo(update);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/checkouts/{CheckoutData.CheckoutModel.Id}")
            .WithVerb(HttpMethod.Put);
      }

      [Test]
      public async Task sending_checkout_updates_to_the_server_should_only_be_of_defined_values()
      {
         SetupServerSingleResponse(CheckoutData.Checkout);

         var updateCheckout = new UpdateCheckout
         {
            LocalPrice = new Money { Amount = 200.00m, Currency = "USD" }
         };

         await api.UpdateCheckoutAsync(CheckoutData.CheckoutModel.Id, updateCheckout);

         //client should only send up only explicitly filled out fields.
         var onlyMinimalUpdateSentToServer = @"{""local_price"":{""amount"":200.00,""currency"":""USD""}}";
         server.CallLog.First().RequestBody.Should().Be(onlyMinimalUpdateSentToServer);
      }

      [Test]
      public async Task can_delete_a_checkout()
      {
         server.RespondWith("");
         await api.DeleteCheckoutAsync(CheckoutData.CheckoutModel.Id);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/checkouts/{CheckoutData.CheckoutModel.Id}")
            .WithVerb(HttpMethod.Delete);
      }

   }
}
