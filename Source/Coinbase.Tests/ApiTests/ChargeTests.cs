using System.Net.Http;
using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Coinbase.Tests.ApiTests
{
   public class ChargeTests : CommerceApiTests
   {
      [Test]
      public async Task can_list_charges()
      {
         SetupServerPagedResponse(PageData.Pagination, ChargeData.Charge);

         var charges = await api.ListChargesAsync();

         var truth = new PagedResponse<Charge>()
         {
            Pagination = PageData.PaginationModel,
            Data = new[] { ChargeData.ChargeModel }
         };

         truth.Should().BeEquivalentTo(charges);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/charges")
            .WithVerb(HttpMethod.Get);

      }

      [Test]
      public async Task list_charges_with_different_ordering()
      {
         SetupServerPagedResponse(PageData.Pagination, ChargeData.Charge);

         await api.ListChargesAsync(ListOrder.Asc, 75, "ffff", "gggg");

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/charges?order=asc&limit=75&starting_after=ffff&ending_before=gggg")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_get_a_single_charge()
      {
         SetupServerSingleResponse(ChargeData.Charge);

         var charge = await api.GetChargeAsync("66BEOV2A");

         var truth = new Response<Charge>
         {
            Data = ChargeData.ChargeModel
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

         SetupServerSingleResponse(ChargeData.ChargeWithMetadata(metadata));


         var newCharge = new CreateCharge
         {
            Name = "The Sovereign Individual",
            Description = "Mastering the Transition to the Information Age",
            LocalPrice = new Money { Amount = 100.00m, Currency = "USD" },
            PricingType = PricingType.FixedPrice,
            Metadata = new JObject
                  {
                     {"customer_id", "id_1005"},
                     {"customer_name", "Satoshi Nakamoto"}
                  }
         };

         var charge = await api.CreateChargeAsync(newCharge);


         var truth = new Response<Charge>
         {
            Data = ChargeData.ChargeModel
         };
         truth.Data.Metadata = newCharge.Metadata;


         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/charges")
            .WithVerb(HttpMethod.Post);
      }

      [Test]
      public async Task can_cancel_a_charge()
      {
         SetupServerSingleResponse(ChargeData.CancelCharge);

         var charge = await api.CancelChargeAsync("66BEOV2A");

         var truth = new Response<Charge>
         {
            Data = ChargeData.CancelChargeModel
         };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/charges/66BEOV2A/cancel")
            .WithVerb(HttpMethod.Post)
            .WithRequestBody("");
      }

      [Test]
      public async Task can_resolve_a_charge()
      {
         SetupServerSingleResponse(ChargeData.ResolveCharge);

         var charge = await api.ResolveChargeAsync("66BEOV2A");

         var truth = new Response<Charge>
         {
            Data = ChargeData.ResolveChargeModel
         };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/charges/66BEOV2A/resolve")
            .WithVerb(HttpMethod.Post)
            .WithRequestBody("");
      }
   }
}
