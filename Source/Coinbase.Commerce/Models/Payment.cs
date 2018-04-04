using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   public partial class Payment : Json
   {
      [JsonProperty("network")]
      public string Network { get; set; }

      [JsonProperty("transaction_id")]
      public string TransactionId { get; set; }

      [JsonProperty("status")]
      public string Status { get; set; }

      [JsonProperty("value")]
      public Dictionary<string, Money> Value { get; set; }

      [JsonProperty("block")]
      public Block Block { get; set; }
   }
}