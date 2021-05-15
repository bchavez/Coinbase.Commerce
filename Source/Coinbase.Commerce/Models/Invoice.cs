using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// An invoice can be created and sent to customers for payment by constructing a url with the
   /// generated 8 character short-code. Invoice url's have the format https://commerce.coinbase.com/invoices/:code.
   /// Invoice urls can be sent to the payee to collect payment, and will associate a new charge on the invoice
   /// object once it has been viewed. Charges associated with invoices will automatically refresh their 1-hour
   /// payment window if they expire.
   /// </summary>
   public partial class Invoice : Json
   {
      /// <summary>
      /// Charge UUID
      /// </summary>
      [JsonProperty("id")]
      public Guid Id { get; set; }

      /// <summary>
      /// Resource name: "invoice"
      /// </summary>
      [JsonProperty("resource")]
      public string Resource { get; set; }

      /// <summary>
      /// Charge user-friendly primary key
      /// </summary>
      [JsonProperty("code")]
      public string Code { get; set; }

      /// <summary>
      /// Status of the invoice
      /// </summary>
      [JsonProperty("status")]
      public string Status { get; set; }

      /// <summary>
      /// Your business name
      /// </summary>
      [JsonProperty("business_name")]
      public string BusinessName { get; set; }

      /// <summary>
      /// Customer's name (optional)
      /// </summary>
      [JsonProperty("customer_name")]
      public string CustomerName { get; set; }

      /// <summary>
      /// Customer's email
      /// </summary>
      [JsonProperty("customer_email")]
      public string CustomerEmail { get; set; }

      /// <summary>
      /// Invoice memo
      /// </summary>
      [JsonProperty("memo")]
      public string Memo { get; set; }

      /// <summary>
      /// Invoice price information object
      /// </summary>
      [JsonProperty("local_price")]
      public Money LocalPrice { get; set; }

      /// <summary>
      /// Hosted invoice URL
      /// </summary>
      [JsonProperty("hosted_url")]
      public Uri HostedUrl { get; set; }

      /// <summary>
      /// Invoice creation time
      /// </summary>
      [JsonProperty("created_at")]
      public DateTimeOffset CreatedAt { get; set; }

      /// <summary>
      /// Invoice updated time
      /// </summary>
      [JsonProperty("updated_at")]
      public DateTimeOffset UpdatedAt { get; set; }

      /// <summary>
      /// Associated charge resource (only exists if viewed)
      /// </summary>
      [JsonProperty("charge")]
      public JObject Charge { get; set; }
   }
}
