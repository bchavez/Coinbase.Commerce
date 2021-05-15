using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   public partial class CreateInvoice : Json
   {
      /// <summary>
      /// Your business name
      /// </summary>
      [JsonProperty("business_name")]
      public string BusinessName { get; set; }

      /// <summary>
      /// The email address of the customer
      /// </summary>
      [JsonProperty("customer_email")]
      public string CustomerEmail { get; set; }

      /// <summary>
      /// The name of the customer
      /// </summary>
      [JsonProperty("customer_name")]
      public string CustomerName { get; set; }

      /// <summary>
      /// Price in local fiat currency
      /// </summary>
      [JsonProperty("local_price")]
      public Money LocalPrice { get; set; }

      /// <summary>
      /// A memo for the invoice
      /// </summary>
      [JsonProperty("memo")]
      public string Memo { get; set; }
   }
}
