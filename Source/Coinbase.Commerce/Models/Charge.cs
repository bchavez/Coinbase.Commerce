using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// To request a cryptocurrency payment, you create a charge. You can create
   /// and view charges. Since cryptocurrency payments are push payments,
   /// a charge will expire after a waiting period (payment window) if no payment
   /// has been detected. Charges are identified by a unique code.
   /// </summary>
   public partial class Charge : Json
   {
      /// <summary>
      /// Charge UUID
      /// </summary>
      [JsonProperty("id")]
      public string Id { get; set; }

      /// <summary>
      /// Resource name: "charge"
      /// </summary>
      [JsonProperty("resource")]
      public string Resource { get; set; }

      /// <summary>
      /// Charge user-friendly primary key
      /// </summary>
      [JsonProperty("code")]
      public string Code { get; set; }

      /// <summary>
      /// Charge name
      /// </summary>
      [JsonProperty("name")]
      public string Name { get; set; }

      /// <summary>
      /// Charge description
      /// </summary>
      [JsonProperty("description")]
      public string Description { get; set; }

      /// <summary>
      /// Charge image URL
      /// </summary>
      [JsonProperty("logo_url")]
      public string LogoUrl { get; set; }

      /// <summary>
      /// Hosted charge URL
      /// </summary>
      [JsonProperty("hosted_url")]
      public string HostedUrl { get; set; }

      /// <summary>
      /// Charge creation time
      /// </summary>
      [JsonProperty("created_at")]
      public System.DateTimeOffset CreatedAt { get; set; }

      /// <summary>
      /// Charge expiration time
      /// </summary>
      [JsonProperty("expires_at")]
      public System.DateTimeOffset ExpiresAt { get; set; }

      /// <summary>
      /// Charge confirmation time
      /// </summary>
      [JsonProperty("confirmed_at")]
      public System.DateTimeOffset ConfirmedAt { get; set; }

      /// <summary>
      /// Associated checkout resource
      /// </summary>
      [JsonProperty("checkout")]
      public Checkout Checkout { get; set; }

      /// <summary>
      /// Array of status update objects. Every charge object has a timeline of status updates.
      /// </summary>
      [JsonProperty("timeline")]
      public Timeline[] Timeline { get; set; }

      /// <summary>
      /// Metadata associated with the charge
      /// </summary>
      [JsonProperty("metadata")]
      public JObject Metadata { get; set; }

      /// <summary>
      /// If a charge has a fixed_price pricing type, then there will be a pricing object 
      /// associated with it. Pricing object is composed of local price which is set by 
      /// the merchant in their native fiat currency and corresponding prices in every 
      /// cryptocurrency that the merchant has activated for their account
      /// </summary>
      [JsonProperty("pricing_type")]
      public PricingType PricingType { get; set; }

      /// <summary>
      /// Charge price information object
      /// </summary>
      [JsonProperty("pricing")]
      public Dictionary<string, Money> Pricing { get; set; }

      /// <summary>
      /// Array of charge payment objects
      /// </summary>
      [JsonProperty("payments")]
      public Payment[] Payments { get; set; }

      /// <summary>
      /// Set of addresses associated with the charge. For every active cryptocurrency 
      /// for a charge addresses object will contain an address that the buyer will 
      /// be expected to pay to
      /// </summary>
      [JsonProperty("addresses")]
      public Dictionary<string,string> Addresses { get; set; }
   }
}
