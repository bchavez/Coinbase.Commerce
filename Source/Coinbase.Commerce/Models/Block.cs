using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// Block information about a payment.
   /// </summary>
   public partial class Block : Json
   {
      /// <summary>
      /// Block height
      /// </summary>
      [JsonProperty("height")]
      public long Height { get; set; }

      /// <summary>
      /// Block hash
      /// </summary>
      [JsonProperty("hash")]
      public string Hash { get; set; }

      /// <summary>
      /// How many block confirmations there have been
      /// </summary>
      [JsonProperty("confirmations_accumulated")]
      public long ConfirmationsAccumulated { get; set; }

      /// <summary>
      /// How many are required to secure the transaction on the blockchain
      /// </summary>
      [JsonProperty("confirmations_required")]
      public long ConfirmationsRequired { get; set; }
   }
}