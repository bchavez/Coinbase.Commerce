using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase.Commerce;
using Coinbase.Commerce.Models;
using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Configuration;
using NUnit.Framework;
using Z.ExtensionMethods;

namespace Coinbase.Tests
{
   public class ProxyFactory : DefaultHttpClientFactory
   {
      private readonly WebProxy proxy;

      public ProxyFactory(WebProxy proxy)
      {
         this.proxy = proxy;
      }

      public override HttpMessageHandler CreateMessageHandler()
      {
         return new HttpClientHandler
            {
               Proxy = this.proxy,
               UseProxy = true
            };
      }
   }

   [TestFixture]
   [Explicit]
   public class IntegrationTests
   {
      private CommerceApi commerceApi;
      private string webhookSecret;

      public IntegrationTests()
      {
         Directory.SetCurrentDirectory(Path.GetDirectoryName(typeof(IntegrationTests).Assembly.Location));
         var lines  = File.ReadAllLines("../../.secrets.txt");
         var apiKey = lines[0].GetAfter(":");
         webhookSecret = lines[1].GetAfter(":");

         var webProxy = new WebProxy("http://localhost.:8888", BypassOnLocal: false);

         FlurlHttp.Configure(settings =>
            {
               settings.HttpClientFactory = new ProxyFactory(webProxy);
            });

         commerceApi = new CommerceApi(apiKey);
      }

      [Test]
      public async Task create_a_charge()
      {
         //Something that identifies
         //the customer on your system with
         //the payment the customer is about to make.
         var customerId = Guid.NewGuid();

         var charge = new CreateCharge
            {
               Name = "Candy Bar",
               Description = "Sweet Tasting Chocolate",
               PricingType = PricingType.FixedPrice,
               LocalPrice = new Money { Amount = 1.00m, Currency = "USD"},
               Metadata =
                  {
                     {"customerId", customerId }
                  },
            };

         var response = await commerceApi.CreateChargeAsync(charge);

         response.Dump();
         response.HasError().Should().Be(false);
         response.HasWarnings().Should().Be(false);
         response.Data.Should().NotBeNull();
         response.Data.HostedUrl.Should().StartWith("https://commerce.coinbase.com/charges/");

         //Redirect the user to the checkout URL:
         if( response.HasError() )
         {
            //The server says something is wrong
            //log and report back to the user an
            //error has occurred.
            Console.WriteLine(response.Error.Message);
            return;
         }
         //else, the carge creation was successful,
         //send the user to the hosted checkout page
         //at coinbase.
         //Server.Redirect(response.Data.HostedUrl);
         Console.WriteLine(response.Data.HostedUrl);
      }

      [Test]
      public void can_verify_a_webhook()
      {
         WebhookHelper.IsValid(webhookSecret, Examples.WebhookHeaderSignature, Examples.Webhook)
            .Should().Be(true);
      }
   }
}