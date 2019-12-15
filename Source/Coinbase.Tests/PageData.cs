using Coinbase.Commerce.Models;

namespace Coinbase.Tests
{
   public static class PageData
   {
      public const string Pagination = @"
      {
        ""order"": ""desc"",
        ""starting_after"": null,
        ""ending_before"": null,
        ""total"": 25,
        ""yielded"": 20,
        ""limit"": 20,
        ""previous_uri"": null,
        ""next_uri"": ""https://api.commerce.coinbase.com/checkouts?limit=20&starting_after=fb6721f2-1622-48f0-b713-aac6c819b67a"",
        ""cursor_range"": [""a76721f2-1611-48fb-a513-aac6c819a9d6"", ""fb6721f2-1622-48f0-b713-aac6c819b67a""]
    }";

      public static Pagination PaginationModel => new Pagination
         {
            Order = ListOrder.Desc,
            StartingAfter = null,
            EndingBefore = null,
            Total = 25,
            Yielded = 20,
            Limit = 20,
            PreviousUri = null,
            NextUri = "https://api.commerce.coinbase.com/checkouts?limit=20&starting_after=fb6721f2-1622-48f0-b713-aac6c819b67a",
            CursorRange = new[] {"a76721f2-1611-48fb-a513-aac6c819a9d6", "fb6721f2-1622-48f0-b713-aac6c819b67a"}
         };

   }
}
