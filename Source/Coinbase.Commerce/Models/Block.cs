using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   public partial class Block : Json
   {
      [JsonProperty("height")]
      public long Height { get; set; }

      [JsonProperty("hash")]
      public string Hash { get; set; }

      [JsonProperty("confirmations_accumulated")]
      public long ConfirmationsAccumulated { get; set; }

      [JsonProperty("confirmations_required")]
      public long ConfirmationsRequired { get; set; }
   }
}