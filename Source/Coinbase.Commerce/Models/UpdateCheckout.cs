using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// When updating a checkout, you do not need to copy the entire
   /// object graph from an existing checkout object. Only fill this
   /// object with the checkout data that you wish to change.
   /// This update object works like a PATCH update.
   /// </summary>
   [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
   public partial class UpdateCheckout : Json
   {
      /// <summary>
      /// When updating a checkout, you do not need to copy the entire
      /// object graph from an existing checkout object. Only fill this
      /// object with the checkout data that you wish to change.
      /// This update object works like a PATCH update.
      /// </summary>
      public UpdateCheckout()
      {
      }

      /// <summary>
      /// Checkout name
      /// </summary>
      [JsonProperty("name")]
      public string Name { get; set; }

      /// <summary>
      /// More detailed description
      /// </summary>
      [JsonProperty("description")]
      public string Description { get; set; }

      /// <summary>
      /// If <seealso cref="PricingType"/> is fixed_price, then this field will specify the price.
      /// Pricing object is composed of local price which is set by the
      /// merchant in their native fiat currency and corresponding prices in every
      /// cryptocurrency that the merchant has activated for their account
      /// </summary>
      [JsonProperty("local_price")]
      public Money LocalPrice { get; set; }

      private HashSet<string> requestedInfo;

      /// <summary>
      /// Information to collect from the customer
      /// </summary>
      [JsonProperty("requested_info")]
      public HashSet<string> RequestedInfo
      {
         get => this.requestedInfo ?? (this.requestedInfo = new HashSet<string>());
         set => this.requestedInfo = value;
      }

      /// <summary>
      /// Called by <seealso cref="Newtonsoft.Json"/> to determine if the
      /// <seealso cref="RequestedInfo"/> property should be serialized.
      /// </summary>
      public virtual bool ShouldSerializeRequestedInfo()
      {
         return !(requestedInfo is null);
      }
   }

   public partial class UpdateCheckout
   {
      /// <summary>
      /// Helper property. Adds "email" to the RequestedInfo property.
      /// </summary>
      [JsonIgnore]
      public bool RequestEmail
      {
         get => this.RequestedInfo.Contains("email");
         set
         {
            if (value) this.RequestedInfo.Add("email");
            else this.RequestedInfo.Remove("email");
         }
      }

      /// <summary>
      /// Helper property. Adds "name" to the RequestedInfo property.
      /// </summary>
      [JsonIgnore]
      public bool RequestName
      {
         get => this.RequestedInfo.Contains("name");
         set
         {
            if (value) this.RequestedInfo.Add("name");
            else this.RequestedInfo.Remove("name");
         }
      }
   }
}