using System;
using Newtonsoft.Json;

namespace Coinbase.Commerce.Models
{
   /// <summary>
   /// Every charge object has a timeline of status updates.
   /// </summary>
   public partial class Timeline : Json
   {
      /// <summary>
      /// Time of the status update
      /// </summary>
      [JsonProperty("time")]
      public System.DateTimeOffset Time { get; set; }

      /// <summary>
      /// One of the following statuses: NEW, PENDING, COMPLETED, EXPIRED, UNRESOLVED, RESOLVED, CANCELED
      /// </summary>
      [JsonProperty("status")]
      public string Status { get; set; }

      /// <summary>
      /// For charges with UNRESOLVED status, additional context is provided. Context can be one of the following: UNDERPAID, OVERPAID, DELAYED, MULTIPLE, MANUAL, OTHER
      /// </summary>
      [JsonProperty("context")]
      public string Context { get; set; }
   }

   public partial class Timeline
   {
      //NEW, PENDING, COMPLETED, EXPIRED, UNRESOLVED, RESOLVED, CANCELED
      [JsonIgnore]
      public bool IsStatusNew => "NEW".Equals(this.Status, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsStatusPending => "PENDING".Equals(this.Status, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsStatusCompleted => "COMPLETED".Equals(this.Status, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsStatusExpired => "EXPIRED".Equals(this.Status, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsStatusUnresolved => "UNRESOLVED".Equals(this.Status, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsStatusResolved => "RESOLVED".Equals(this.Status, StringComparison.OrdinalIgnoreCase);
      
      [JsonIgnore]
      public bool IsStatusCanceled => "CANCELED".Equals(this.Status, StringComparison.OrdinalIgnoreCase);


      //UNDERPAID, OVERPAID, DELAYED, MULTIPLE, MANUAL, OTHER
      [JsonIgnore]
      public bool IsContextUnderpaid => "UNDERPAID".Equals(this.Context, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsContextOverpaid => "OVERPAID".Equals(this.Context, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsContextDelayed => "DELAYED".Equals(this.Context, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsContextMultiple => "MULTIPLE".Equals(this.Context, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsContextManual => "MANUAL".Equals(this.Context, StringComparison.OrdinalIgnoreCase);

      [JsonIgnore]
      public bool IsContextOther => "OTHER".Equals(this.Context, StringComparison.OrdinalIgnoreCase);
   }
}
