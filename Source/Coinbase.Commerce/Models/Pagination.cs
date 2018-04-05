using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// All GET endpoints which return an object list support cursor
   /// based pagination with pagination information inside a pagination object.
   /// This means that to get all objects, you need to paginate through the
   /// results by always using the id of the last resource in the list as a
   /// starting_after parameter for the next call. To make it easier, the API
   /// will construct the next call into next_uri together with all the currently
   /// used pagination parameters. You know that you have paginated all the
   /// results when the response’s next_uri is empty. Default limit is set 
   /// to 25 but values up to 100 are permitted.
   /// </summary>
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