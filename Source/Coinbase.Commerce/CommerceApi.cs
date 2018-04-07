using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using Flurl;
using Flurl.Http;

[assembly:InternalsVisibleTo("Coinbase.Tests")]

namespace Coinbase.Commerce
{
   /// <summary>
   /// The main class to use when accessing the Coinbase Commerce API. API Docs: https://commerce.coinbase.com/docs/api/
   /// </summary>
   public class CommerceApi
   {
      /// <summary>
      /// All API calls should be made with a X-CC-Version header which guarantees that your call is using the correct API version. Version is passed in as a date (UTC) of the implementation in YYYY-MM-DD format.
      /// If no version is passed, the latest API version will be used and a warning will be included in the response.Under no circumstance should you always pass in the current date, as that will return the current version which might break your implementation.
      /// </summary>
      protected internal const string ApiVersionDate = "2018-03-22";

      /// <summary>
      /// API endpoint at coinbase
      /// </summary>
      protected internal const string Endpoint = "https://api.commerce.coinbase.com";
     
      /// <summary>
      /// API Endpoint
      /// </summary>
      protected internal Url ChargesEndpoint;
      /// <summary>
      /// API Endpoint
      /// </summary>
      protected internal Url CheckoutEndpoint;
      /// <summary>
      /// API Endpoint
      /// </summary>
      protected internal Url EventsEndpoint;


      /// <summary>
      /// User's API Key
      /// </summary>
      protected internal readonly string apiKey;


      /// <summary>
      /// The main class to make calls to the coinbase commerce API.
      /// API Documentation here: https://commerce.coinbase.com/docs/api/
      /// GitHub Project URL here: https://github.com/bchavez/Coinbase.Commerce
      /// </summary>
      /// <param name="apiKey">Your secret API key. See: https://commerce.coinbase.com/docs/api/#authentication </param>
      public CommerceApi(string apiKey)
      {
         this.apiKey = apiKey;
         ConfigureClient();
         
         ChargesEndpoint = Endpoint.AppendPathSegment("charges");
         CheckoutEndpoint = Endpoint.AppendPathSegment("checkouts");
         EventsEndpoint = Endpoint.AppendPathSegment("events");
      }


      internal static readonly string UserAgent =
         $"{AssemblyVersionInformation.AssemblyProduct}/{AssemblyVersionInformation.AssemblyVersion} ({AssemblyVersionInformation.AssemblyTitle}; {AssemblyVersionInformation.AssemblyDescription})";
         
      
      protected internal void ConfigureClient()
      {
         FlurlHttp.ConfigureClient(Endpoint, client =>
            {
               client
                  .WithHeader(HeaderNames.ApiKey, this.apiKey)
                  .WithHeader(HeaderNames.Version, ApiVersionDate)
                  .WithHeader("User-Agent", UserAgent);
            });
      }

      /// <summary>
      /// List all the charges. All GET endpoints which return an object list
      /// support cursor based pagination with pagination information
      /// inside a pagination object. This means that to get all objects,
      /// you need to paginate through the results by always using the id
      /// of the last resource in the list as a starting_after parameter
      /// for the next call. To make it easier, the API will construct
      /// the next call into next_uri together with all the currently
      /// used pagination parameters. You know that you have paginated
      /// all the results when the response’s next_uri is empty.
      /// </summary>
      /// <param name="listOrder">Order of the resources in the response. desc (default), asc</param>
      /// <param name="limit">umber of results per call. Accepted values: 0 - 100. Default 25</param>
      /// <param name="startingAfter">A cursor for use in pagination. starting_after is a resource ID that defines your place in the list.</param>
      /// <param name="endingBefore">A cursor for use in pagination. ending_before is a resource ID that defines your place in the list.</param>
      public virtual Task<PagedResponse<Charge>> ListChargesAsync(ListOrder? listOrder = null, int? limit = null, string startingAfter = null, string endingBefore = null, CancellationToken cancellationToken = default)
      {
         return ChargesEndpoint
            .SetQueryParam("order", listOrder?.ToString().ToLower())
            .SetQueryParam("limit", limit)
            .SetQueryParam("starting_after", startingAfter)
            .SetQueryParam("ending_before", endingBefore)
            .GetJsonAsync<PagedResponse<Charge>>(cancellationToken);
      }

      /// <summary>
      /// Retrieves the details of a charge that has been previously
      /// created. Supply the unique charge code that was returned when
      /// the charge was created. This information is also returned when
      /// a charge is first created.
      /// </summary>
      /// <param name="chargeCode">The Charge.Code</param>
      public virtual Task<Response<Charge>> GetChargeAsync(string chargeCode, CancellationToken cancellationToken = default)
      {
         return ChargesEndpoint
            .AppendPathSegment(chargeCode)
            .GetJsonAsync<Response<Charge>>(cancellationToken);
      }

      /// <summary>
      /// To get paid in cryptocurrency, you need to create a charge object 
      /// and provide the user with a cryptocurrency address to 
      /// which they must send cryptocurrency. Once a charge is 
      /// created a customer must broadcast a payment to the
      /// blockchain before the charge expires.
      /// </summary>
      /// <param name="charge">The <seealso cref="CreateCharge"/> object.</param>
      public virtual Task<Response<Charge>> CreateChargeAsync(CreateCharge charge, CancellationToken cancellationToken = default)
      {
         return ChargesEndpoint
            .PostJsonAsync(charge, cancellationToken)
            .ReceiveJson<Response<Charge>>();
      }


      /// <summary>
      /// List all the checkouts. All GET endpoints which return an object list
      /// support cursor based pagination with pagination information
      /// inside a pagination object. This means that to get all objects,
      /// you need to paginate through the results by always using the id
      /// of the last resource in the list as a starting_after parameter
      /// for the next call. To make it easier, the API will construct
      /// the next call into next_uri together with all the currently
      /// used pagination parameters. You know that you have paginated
      /// all the results when the response’s next_uri is empty.
      /// </summary>
      /// <param name="listOrder">Order of the resources in the response. desc (default), asc</param>
      /// <param name="limit">umber of results per call. Accepted values: 0 - 100. Default 25</param>
      /// <param name="startingAfter">A cursor for use in pagination. starting_after is a resource ID that defines your place in the list.</param>
      /// <param name="endingBefore">A cursor for use in pagination. ending_before is a resource ID that defines your place in the list.</param>
      public virtual Task<PagedResponse<Checkout>> ListCheckoutsAsync(ListOrder? listOrder = null, int? limit = null, string startingAfter = null, string endingBefore = null, CancellationToken cancellationToken = default)
      {
         return CheckoutEndpoint
            .SetQueryParam("order", listOrder?.ToString().ToLower())
            .SetQueryParam("limit", limit)
            .SetQueryParam("starting_after", startingAfter)
            .SetQueryParam("ending_before", endingBefore)
            .GetJsonAsync<PagedResponse<Checkout>>(cancellationToken);
      }

      /// <summary>
      /// Show a single checkout
      /// </summary>
      /// <param name="checkoutId">The checkout id</param>
      public virtual Task<Response<Checkout>> GetCheckoutAsync(string checkoutId, CancellationToken cancellationToken = default)
      {
         return CheckoutEndpoint
            .AppendPathSegment(checkoutId)
            .GetJsonAsync<Response<Checkout>>(cancellationToken);
      }

      /// <summary>
      /// Create a new checkout.
      /// </summary>
      /// <param name="checkout">The checkout to create</param>
      public virtual Task<Response<Checkout>> CreateCheckoutAsync(CreateCheckout checkout)
      {
         return CheckoutEndpoint
            .PostJsonAsync(checkout)
            .ReceiveJson<Response<Checkout>>();
      }

      /// <summary>
      /// Update a checkout.
      /// </summary>
      /// <param name="checkoutId">The checkout id</param>
      /// <param name="checkout">The checkout to update</param>
      public virtual Task<Response<Checkout>> UpdateCheckoutAsync(string checkoutId, UpdateCheckout checkout, CancellationToken cancellationToken = default)
      {
         return CheckoutEndpoint
            .AppendPathSegment(checkoutId)
            .PutJsonAsync(checkout, cancellationToken)
            .ReceiveJson<Response<Checkout>>();
      }

      /// <summary>
      /// Delete a checkout.
      /// </summary>
      /// <param name="checkoutId">The checkout id.</param>
      public virtual Task DeleteCheckoutAsync(string checkoutId, CancellationToken cancellationToken = default)
      {
         return CheckoutEndpoint
            .AppendPathSegment(checkoutId)
            .DeleteAsync(cancellationToken);
      }


      /// <summary>
      /// List all events. All GET endpoints which return an object list
      /// support cursor based pagination with pagination information
      /// inside a pagination object. This means that to get all objects,
      /// you need to paginate through the results by always using the id
      /// of the last resource in the list as a starting_after parameter
      /// for the next call. To make it easier, the API will construct
      /// the next call into next_uri together with all the currently
      /// used pagination parameters. You know that you have paginated
      /// all the results when the response’s next_uri is empty.
      /// </summary>
      /// <param name="listOrder">Order of the resources in the response. desc (default), asc</param>
      /// <param name="limit">umber of results per call. Accepted values: 0 - 100. Default 25</param>
      /// <param name="startingAfter">A cursor for use in pagination. starting_after is a resource ID that defines your place in the list.</param>
      /// <param name="endingBefore">A cursor for use in pagination. ending_before is a resource ID that defines your place in the list.</param>
      public virtual Task<PagedResponse<Event>> ListEventsAsync(ListOrder? listOrder = null, int? limit = null, string startingAfter = null, string endingBefore = null, CancellationToken cancellationToken = default)
      {
         return EventsEndpoint
            .SetQueryParam("order", listOrder?.ToString().ToLower())
            .SetQueryParam("limit", limit)
            .SetQueryParam("starting_after", startingAfter)
            .SetQueryParam("ending_before", endingBefore)
            .GetJsonAsync<PagedResponse<Event>>(cancellationToken);
      }

      /// <summary>
      /// Get an event with a specific id.
      /// </summary>
      /// <param name="eventId"></param>
      /// <returns></returns>
      public virtual Task<Response<Event>> GetEventAsync(string eventId, CancellationToken cancellationToken = default)
      {
         return EventsEndpoint
            .AppendPathSegment(eventId)
            .GetJsonAsync<Response<Event>>(cancellationToken);
      }
   }
}
