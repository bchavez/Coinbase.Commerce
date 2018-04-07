using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// Payment information
   /// </summary>
   public partial class Payment : Json
   {
      /// <summary>
      /// Blockchain network: "bitcoin", "ethereum", "litecoin" etc.
      /// </summary>
      [JsonProperty("network")]
      public string Network { get; set; }

      [JsonProperty("transaction_id")]
      public string TransactionId { get; set; }

      /// <summary>
      /// Status of the payment
      /// </summary>
      [JsonProperty("status")]
      public string Status { get; set; }

      [JsonProperty("value")]
      public Dictionary<string, Money> Value { get; set; }

      /// <summary>
      /// Blockchain information.
      /// </summary>
      [JsonProperty("block")]
      public Block Block { get; set; }
   }
}