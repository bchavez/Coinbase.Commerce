using System;
using System.Security.Cryptography;
using System.Text;

namespace Coinbase.Commerce
{
   /// <summary>
   /// Coinbase Commerce signs every webhook event it sends to your endpoints.
   /// The signature is included as a X-CC-Webhook-Signature header. This header
   /// contains the SHA256 HMAC signature of the raw request payload, computed
   /// using your webhook shared secret as the key. You can obtain your shared
   /// webhook secret in settings. Always make sure that you verify the webhook
   /// signature before acting on it inside your system.
   /// </summary>
   public static class WebhookHelper
   {
      private static UTF8Encoding safeUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
      /// <summary>
      /// Checks if the Webhook callback is an authentic request from Coinbase.
      /// Coinbase Commerce signs every webhook event it sends to your endpoints.
      /// The signature is included as a <seealso cref="HeaderNames.WebhookSignature"/> header.
      /// This header contains the SHA256 HMAC signature of the raw request payload,
      /// computed using your webhook shared secret as the key. You can obtain your shared
      /// webhook secret in settings. Always make sure that you verify the webhook
      /// signature before acting on it inside your system.
      /// </summary>
      /// <param name="sharedSecret">The Webhook shared secret</param>
      /// <param name="headerValue">The header value from the Webhook request</param>
      /// <param name="jsonBody">The HTTP JSON body of the Webhook request</param>
      /// <returns></returns>
      public static bool IsValid(string sharedSecret, string headerValue, string jsonBody)
      {
         var computed = GetHmacInHex(sharedSecret, jsonBody);
         return computed.Equals(headerValue, StringComparison.OrdinalIgnoreCase);
      }

      private static string GetHmacInHex(string key, string data)
      {
         var hmacKey = safeUtf8.GetBytes(key);

         var dataBytes = safeUtf8.GetBytes(data);

         using( var hmac = new HMACSHA256(hmacKey) )
         {
            var hash = hmac.ComputeHash(dataBytes);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
         }
      }
   }
}