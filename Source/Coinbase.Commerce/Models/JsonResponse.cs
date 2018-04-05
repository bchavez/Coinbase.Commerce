using System.Collections.Generic;
using System.Linq;
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

      /// <summary>
      /// Responses can include a warnings parameter to notify the developer
      /// of best practices, implementation suggestions or deprecation warnings.
      /// While you don’t need show warnings to the user, they are usually
      /// something you need to act on.
      /// </summary>
      [JsonProperty("warnings")]
      public string[] Warnings { get; set; }

      /// <summary>
      /// Checks if the response has errors.
      /// </summary>
      public bool HasError()
      {
         return this.Error != null;
      }

      /// <summary>
      /// Checks if the response has warnings.
      /// </summary>
      public bool HasWarnings()
      {
         return this.Warnings?.Length > 0;
      }
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