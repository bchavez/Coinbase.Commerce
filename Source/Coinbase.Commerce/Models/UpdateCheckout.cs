using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
   public partial class UpdateCheckout : Json
   {
      public UpdateCheckout()
      {
         this.RequestedInfo = new HashSet<string>();
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

      /// <summary>
      /// Information to collect from the customer
      /// </summary>
      [JsonProperty("requested_info")]
      public HashSet<string> RequestedInfo { get; set; }

      public bool ShouldSerializeRequestedInfo()
      {
         return this.RequestedInfo.Count > 0;
      }
   }

   public partial class UpdateCheckout
   {
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