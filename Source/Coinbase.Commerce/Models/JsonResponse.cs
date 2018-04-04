using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coinbase.Commerce.Models
{
   public class Json
   {
      /// <summary>
      /// Extra data for/from the JSON serializer/deserializer to included with the object model.
      /// </summary>
      [JsonExtensionData]
      public IDictionary<string, JToken> ExtraJson { get; } = new Dictionary<string, JToken>();
   }

   public class JsonResponse : Json
   {
      /// <summary>
      /// All error messages include a type identifier and a human readable message.
      /// </summary>
      [JsonProperty("error")]
      public Error Error { get; set; }

      [JsonProperty("warnings")]
      public string[] Warnings { get; set; }
   }

   public class Response<T> : JsonResponse
   {
      [JsonProperty("data")]
      public T Data { get; set; }
   }

   public class PagedResponse<T> : JsonResponse
   {
      [JsonProperty("pagination")]
      public Pagination Pagination { get; set; }

      [JsonProperty("data")]
      public T[] Data { get; set; }
   }
}