using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// Webhooks make it easier to integrate with Coinbase Commerce by allowing you to
   /// subscribe to a set of charge events. You can subscribe to the events by going to
   /// your settings page and adding a new webhook subscription. When you create a new
   /// subscription, you can specify what events you would like to receive updates for.
   /// </summary>
   public partial class Webhook : Json
   {
      /// <summary>
      /// The Webhook UUID
      /// </summary>
      [JsonProperty("id")]
      public string Id { get; set; }

      /// <summary>
      /// Delivery schedule time
      /// </summary>
      [JsonProperty("scheduled_for")]
      public System.DateTimeOffset ScheduledFor { get; set; }

      /// <summary>
      /// Delivery attempt number
      /// </summary>
      [JsonProperty("attempt_number")]
      public long AttemptNumber { get; set; }

      /// <summary>
      /// Corresponding event object
      /// </summary>
      [JsonProperty("event")]
      public Event Event { get; set; }
   }

   /// <summary>
   /// Events let you know when a charge is updated. When an event occurs we create a 
   /// new event object. Retrieve individual events or a list of events. You can also 
   /// subscribe to webhook notifications which send event objects directly to an 
   /// endpoint on your server.
   /// </summary>
   public partial class Event : Json
   {
      /// <summary>
      /// Event UUID
      /// </summary>
      [JsonProperty("id")]
      public string Id { get; set; }

      /// <summary>
      /// Resource name: "event"
      /// </summary>
      [JsonProperty("resource")]
      public string Resource { get; set; }

      /// <summary>
      /// Event type: charge:created, charge:confirmed, charge:failed, charge:delayed, charge:pending
      /// </summary>
      [JsonProperty("type")]
      public string Type { get; set; }

      /// <summary>
      /// API version of the data payload
      /// </summary>
      [JsonProperty("api_version")]
      public string ApiVersion { get; set; }

      /// <summary>
      /// Event creation time
      /// </summary>
      [JsonProperty("created_at")]
      public System.DateTimeOffset CreatedAt { get; set; }

      /// <summary>
      /// Event payload: resource of the associated object (e.g. charge) at the time of the event
      /// </summary>
      [JsonProperty("data")]
      public JObject Data { get; set; }

      /// <summary>
      /// Get <seealso cref="Data"/> as <typeparam name="T" />
      /// </summary>
      public T DataAs<T>()
      {
         return this.Data.ToObject<T>();
      }
   }


   /// <summary>
   /// Event Resource: https://commerce.coinbase.com/docs/api/#event-resource
   /// </summary>
   public partial class Event
   {
      /// <summary>
      /// New charge is created
      /// </summary>
      [JsonIgnore]
      public bool IsChargeCreated => "charge:created".Equals(this.Type, StringComparison.OrdinalIgnoreCase);

      /// <summary>
      /// Charge has been confirmed and the associated payment is completed
      /// </summary>
      [JsonIgnore]
      public bool IsChargeConfirmed => "charge:confirmed".Equals(this.Type, StringComparison.OrdinalIgnoreCase);

      /// <summary>
      /// Charge failed to complete
      /// </summary>
      [JsonIgnore]
      public bool IsChargeFailed => "charge:failed".Equals(this.Type, StringComparison.OrdinalIgnoreCase);

      /// <summary>
      /// The charge has been delayed
      /// </summary>
      [JsonIgnore]
      public bool IsChargeDelayed => "charge:delayed".Equals(this.Type, StringComparison.OrdinalIgnoreCase);

      /// <summary>
      /// Charge is pending
      /// </summary>
      [JsonIgnore]
      public bool IsChargePending => "charge:pending".Equals(this.Type, StringComparison.OrdinalIgnoreCase);

      /// <summary>
      /// Charge is resolved
      /// </summary>
      [JsonIgnore]
      public bool IsChargeResolved => "charge:resolved".Equals(this.Type, StringComparison.OrdinalIgnoreCase);
   }
}
