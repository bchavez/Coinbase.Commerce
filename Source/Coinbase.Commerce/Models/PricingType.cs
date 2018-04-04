using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// If a charge has a fixed_price pricing type, then there will be a pricing object 
   /// associated with it. Pricing object is composed of local price which is set by the 
   /// merchant in their native fiat currency and corresponding prices in every cryptocurrency 
   /// that the merchant has activated for their account
   /// </summary>
   [JsonConverter(typeof(StringEnumConverter))]
   public enum PricingType
   {
      /// <summary>
      /// If a charge has a fixed_price pricing type, then there will be a pricing object 
      /// associated with it. Pricing object is composed of local price which is set by the 
      /// merchant in their native fiat currency and corresponding prices in every cryptocurrency 
      /// that the merchant has activated for their account
      /// </summary>
      [EnumMember(Value = "no_price")]
      NoPrice,
      /// <summary>
      /// If a charge has a fixed_price pricing type, then there will be a pricing object 
      /// associated with it. Pricing object is composed of local price which is set by the 
      /// merchant in their native fiat currency and corresponding prices in every cryptocurrency 
      /// that the merchant has activated for their account
      /// </summary>
      [EnumMember(Value = "fixed_price")]
      FixedPrice
   }
}