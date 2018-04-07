using Coinbase.Commerce.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Coinbase.Tests
{
   [TestFixture]
   public class ModelTests
   {
      [Test]
      public void should_seralize_requestedinfo_when_property_has_been_modified()
      {
         var update = new UpdateCheckout();
         update.ShouldSerializeRequestedInfo().Should().BeFalse();

         update.RequestedInfo.Add("hello");

         update.ShouldSerializeRequestedInfo().Should().BeTrue();

         update.RequestedInfo = null;

         update.ShouldSerializeRequestedInfo().Should().BeFalse();

         update.RequestedInfo.Add("world");
         update.RequestedInfo.Clear();

         update.ShouldSerializeRequestedInfo().Should().BeTrue();
      }
   }

}