using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// All error messages include a type identifier and a human readable message.
   /// </summary>
   public partial class Error : Json
   {
      /// <summary>
      /// validation_error with status code 400 is returned when the validation of the resource fails on POST or PUT requests.
      /// </summary>
      [JsonProperty("type")]
      public string Type { get; set; }

      [JsonProperty("message")]
      public string Message { get; set; }
   }
}