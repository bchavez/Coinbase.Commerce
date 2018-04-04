using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Coinbase.Commerce.Models
{
   [JsonConverter(typeof(StringEnumConverter), true)]
   public enum ListOrder
   {
      Asc,
      Desc
   }
}