using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   public partial class Pagination : Json
   {
      [JsonProperty("order")]
      public ListOrder Order { get; set; }

      [JsonProperty("starting_after")]
      public string StartingAfter { get; set; }

      [JsonProperty("ending_before")]
      public string EndingBefore { get; set; }

      [JsonProperty("total")]
      public long Total { get; set; }

      [JsonProperty("yielded")]
      public long Yielded { get; set; }

      [JsonProperty("limit")]
      public long Limit { get; set; }

      [JsonProperty("previous_uri")]
      public string PreviousUri { get; set; }

      [JsonProperty("next_uri")]
      public string NextUri { get; set; }

      [JsonProperty("cursor_range")]
      public string[] CursorRange { get; set; }
   }
}