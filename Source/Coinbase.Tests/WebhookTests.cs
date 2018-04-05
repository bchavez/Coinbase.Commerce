using Coinbase.Commerce.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Coinbase.Tests
{
   [TestFixture]
   public class WebhookTests
   {
      [Test]
      public void can_deseralize_webhook()
      {
         var callback = JsonConvert.DeserializeObject<Webhook>(Examples.Webhook);
         callback.Should().BeEquivalentTo(Examples.WebhookModel);

         var charge = callback.Event.DataAs<Charge>();
         charge.Should().NotBeNull();
      }

      [Test]
      [Ignore("only an example")]
      public void example()
      {
         var webhook = JsonConvert.DeserializeObject<Webhook>(Examples.Webhook);

         var chargeInfo = webhook.Event.DataAs<Charge>();
         var customerId = chargeInfo.Metadata["customerId"].ToObject<string>();

         if (webhook.Event.IsChargeCreated)
         {
            // The charge was created just now.
            // Do something with the newly created
            // event.
         }
         else if (webhook.Event.IsChargeFailed)
         {
            // The payment failed. Log something.
         }
         else if( webhook.Event.IsChargeConfirmed )
         {
            // The payment was confirmed.
            // Fulfill the order
         }

         ///return Response.Ok();
      }
   }
}