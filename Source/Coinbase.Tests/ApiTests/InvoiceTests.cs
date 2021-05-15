using System.Net.Http;
using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using FluentAssertions;
using NUnit.Framework;
using VerifyNUnit;

namespace Coinbase.Tests.ApiTests
{
   public class InvoiceTests : CommerceApiTests
   {
      [Test]
      public async Task can_list_invoices()
      {
         server.RespondWithJsonTestFile();

         var r = await api.ListInvoicesAsync();

         server.ShouldHaveCalledSomePath("/invoices")
            .WithVerb(HttpMethod.Get);

         await Verifier.Verify(r);
      }

      [Test]
      public async Task can_get_invoice()
      {
         server.RespondWithJsonTestFile();
         var r = await api.GetInvoiceAsync("fff");

         server.ShouldHaveCalledSomePath("/invoices/fff")
            .WithVerb(HttpMethod.Get);

         await Verifier.Verify(r);
      }

      [Test]
      public async Task can_create_invoice()
      {
         server.RespondWithJsonTestFile();
         var newInvoice = new CreateInvoice
            {
               BusinessName = "BIZ NAME",
               CustomerEmail = "some@user.com",
               CustomerName = "Some User",
               LocalPrice = new Money
                  {
                     Amount = 10.0m,
                     Currency = "USD"
                  },
               Memo = "HELLO WORLD"
            };

         var r = await api.CreateInvoiceAsync(newInvoice);

         server.ShouldHaveCalledSomePath("/invoices")
            .WithRequestJson(newInvoice)
            .WithVerb(HttpMethod.Post);

         await Verifier.Verify(r);
      }

      [Test]
      public async Task can_void_invoice()
      {
         server.RespondWithJsonTestFile();
         var r = await api.VoidInvoiceAsync("fff");

         server.ShouldHaveCalledSomePath("/invoices/fff/void")
            .WithRequestBody("")
            .WithVerb(HttpMethod.Post);

         r.Data.Status.Should().Be("VOID");

         await Verifier.Verify(r);
      }


      [Test]
      public async Task can_resolve_invoice()
      {
         server.RespondWithJsonTestFile();
         var r = await api.ResolveInvoiceAsync("fff");

         server.ShouldHaveCalledSomePath("/invoices/fff/resolve")
            .WithRequestBody("")
            .WithVerb(HttpMethod.Post);

         r.Data.Status.Should().Be("PAID");

         await Verifier.Verify(r);
      }
   }
}
