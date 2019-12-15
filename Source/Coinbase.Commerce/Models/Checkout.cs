using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// Checkouts make it possible to sell a single fixed price item or accept
   /// arbitrary amounts of cryptocurrency very easily. Checkouts can have
   /// many charges and each charge is automatically generated on a per customer
   /// basis. Checkouts can also be quickly integrated into a website by embedding
   /// payment buttons. Each checkout has a publicly accessible hosted page that
   /// can be shared with anyone.
   /// </summary>
   public partial class Checkout : Json
   {
      /// <summary>
      /// Checkout UUID
      /// </summary>
      [JsonProperty("id")]
      public string Id { get; set; }

      /// <summary>
      /// Resource name: "checkout"
      /// </summary>
      [JsonProperty("resource")]
      public string Resource { get; set; }

      /// <summary>
      /// Checkout name
      /// </summary>
      [JsonProperty("name")]
      public string Name { get; set; }

      /// <summary>
      /// Checkout description
      /// </summary>
      [JsonProperty("description")]
      public string Description { get; set; }

      /// <summary>
      /// Checkout image URL
      /// </summary>
      [JsonProperty("logo_url")]
      public string LogoUrl { get; set; }

      /// <summary>
      /// Array of strings specifying what information the merchants wants to 
      /// collect from the buyers: name, email
      /// </summary>
      [JsonProperty("requested_info")]
      public HashSet<string> RequestedInfo { get; set; }

      /// <summary>
      /// If a charge has a fixed_price pricing type, then there will be a pricing 
      /// object associated with it. Pricing object is composed of local price which 
      /// is set by the merchant in their native fiat currency and corresponding 
      /// prices in every cryptocurrency that the merchant has activated for their account
      /// </summary>
      [JsonProperty("pricing_type")]
      public PricingType PricingType { get; set; }

      /// <summary>
      /// If <seealso cref="PricingType"/> is fixed_price, then this field will specify the price.
      /// Pricing object is composed of local price which is set by the
      /// merchant in their native fiat currency and corresponding prices in every
      /// cryptocurrency that the merchant has activated for their account
      /// </summary>
      [JsonProperty("local_price")]
      public Money LocalPrice { get; set; }
   }

   public partial class Checkout
   {
      /// <summary>
      /// Helper property. Checks if the "email" was requested in <seealso cref="RequestedInfo"/>.
      /// </summary>
      [JsonIgnore]
      public bool RequestedEmail => this.RequestedInfo?.Contains("email") ?? false;

      /// <summary>
      /// Helper property. Checks if the "name" was requested in <seealso cref="RequestedInfo"/>.
      /// </summary>
      [JsonIgnore]
      public bool RequestedName => this.RequestedInfo?.Contains("name") ?? false;
   }
}
