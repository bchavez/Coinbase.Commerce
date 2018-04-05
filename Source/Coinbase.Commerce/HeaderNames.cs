namespace Coinbase.Commerce
{
   /// <summary>
   /// Well-known header names when communicating with Coinbase Commerce API over HTTP.
   /// </summary>
   public static class HeaderNames
   {
      /// <summary>
      /// Every Coinbase Commerce webhook request includes an X-CC-Webhook-Signature header.
      /// <seealso cref="WebhookHelper"/> class for verifying the request header signature value.
      /// This header contains the SHA256 HMAC signature of the raw request payload,
      /// computed using your webhook shared secret as the key.
      /// You can obtain your shared webhook secret from your settings page.
      /// Always make sure that you verify the webhook signature before acting
      /// on it inside your system.
      /// </summary>
      public const string WebhookSignature = "X-Cc-Webhook-Signature";

      /// <summary>
      /// Most requests to the Commerce API must be authenticated with an API key.
      /// You can create an API key in your Settings page after creating a Coinbase Commerce account
      /// Authenticated API requests should be made with a X-CC-Api-Key header. Your secret API key should be passed as the value.
      /// If authentication fails, a JSON object with an error message will be returned as a response along with HTTP status 401.
      /// </summary>
      public const string ApiKey = "X-CC-Api-Key";

      /// <summary>
      /// All API calls should be made with a X-CC-Version header which guarantees
      /// that your call is using the correct API version. Version is passed in as
      /// a date (UTC) of the implementation in YYYY-MM-DD format. If no version is
      /// passed, the latest API version will be used and a warning will be included
      /// in the response. Under no circumstance should you always pass in the
      /// current date, as that will return the current version which might break
      /// your implementation.
      /// </summary>
      public const string Version = "X-CC-Version";
   }
}