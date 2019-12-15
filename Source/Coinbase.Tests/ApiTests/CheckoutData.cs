using System.Collections.Generic;
using Coinbase.Commerce.Models;

namespace Coinbase.Tests.ApiTests
{
   public static class CheckoutData
   {
      public const string Checkout = @"{
    ""id"": ""30934862-d980-46cb-9402-43c81b0cabd5"",
    ""name"": ""The Sovereign Individual"",
    ""description"": ""Mastering the Transition to the Information Age"",
    ""logo_url"": ""https://commerce.coinbase.com/charges/ybjknds.png"",
    ""requested_info"": [],
    ""pricing_type"": ""fixed_price"",
    ""local_price"": { ""amount"": ""100.0"", ""currency"": ""USD"" }
}";

      public static string CheckoutWithRequestInfo(string jsonRequestInfo) =>
         Checkout.Replace(@"""requested_info"": []", $@"""requested_info"": [{jsonRequestInfo}]");
      public static Checkout CheckoutModel => new Checkout
         {
            Id = "30934862-d980-46cb-9402-43c81b0cabd5",
            Name = "The Sovereign Individual",
            Description = "Mastering the Transition to the Information Age",
            LogoUrl = "https://commerce.coinbase.com/charges/ybjknds.png",
            RequestedInfo = new HashSet<string> { },
            PricingType = PricingType.FixedPrice,
            LocalPrice = new Money { Amount = 100.0m, Currency = "USD" }
         };

   }
}
