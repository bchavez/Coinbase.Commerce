using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coinbase.Commerce.Models
{
   public partial class CreateCharge : Json
   {
      public CreateCharge()
      {
         this.Metadata = new JObject();
      }

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
      /// If <seealso cref="PricingType"/> is fixed_price, then this field will specify the price.
      /// Pricing object is composed of local price which is set by the
      /// merchant in their native fiat currency and corresponding prices in every
      /// cryptocurrency that the merchant has activated for their account
      /// </summary>
      [JsonProperty("local_price")]
      public Money LocalPrice { get; set; }

      /// <summary>
      /// If a charge has a fixed_price pricing type, then there will be a pricing object
      /// associated with it. Pricing object is composed of local price which is set by the
      /// merchant in their native fiat currency and corresponding prices in every
      /// cryptocurrency that the merchant has activated for their account
      /// </summary>
      [JsonProperty("pricing_type")]
      public PricingType PricingType { get; set; }

      /// <summary>
      /// Metadata associated with the charge
      /// </summary>
      [JsonProperty("metadata")]
      public JObject Metadata { get; set; }
   }
}