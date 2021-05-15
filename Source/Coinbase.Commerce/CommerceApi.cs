using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

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
      protected internal Url ChargesEndpoint => Endpoint.AppendPathSegment("charges");
      /// <summary>
      /// API Endpoint
      /// </summary>
      protected internal Url CheckoutEndpoint => Endpoint.AppendPathSegment("checkouts");
      /// <summary>
      /// API Endpoint
      /// </summary>
      protected internal Url InvoicesEndpoint => Endpoint.AppendPathSegment("invoices");
      /// <summary>
      /// API Endpoint
      /// </summary>
      protected internal Url EventsEndpoint => Endpoint.AppendPathSegment("events");

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
      /// Enable HTTP debugging via Fiddler. Ensure Tools > Fiddler Options... > Connections is enabled and has a port configured.
      /// Then, call this method with the following URL format: http://localhost.:PORT where PORT is the port number Fiddler proxy
      /// is listening on. (Be sure to include the period after the localhost).
      /// </summary>
      /// <param name="proxyUrl">The full proxy URL Fiddler proxy is listening on. IE: http://localhost.:8888 - The period after localhost is important to include.</param>
      public void EnableFiddlerDebugProxy(string proxyUrl)
      {
         var webProxy = new WebProxy(proxyUrl, BypassOnLocal: false);

         FlurlHttp.ConfigureClient(Endpoint, settings =>
            {
               settings.Settings.HttpClientFactory = new DebugProxyFactory(webProxy);
            });
      }

      private class DebugProxyFactory : DefaultHttpClientFactory
      {
         private readonly WebProxy proxy;

         public DebugProxyFactory(WebProxy proxy)
         {
            this.proxy = proxy;
         }

         public override HttpMessageHandler CreateMessageHandler()
         {
            return new HttpClientHandler
               {
                  Proxy = this.proxy,
                  UseProxy = true
               };
         }
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
      /// Cancels a charge that has been previously created.
      /// Supply the unique charge code that was returned when the charge was created.
      /// Note: Only new charges can be successfully canceled.
      /// Once payment is detected, charge can no longer be canceled.
      /// </summary>
      /// <param name="chargeCode">The Charge.Code</param>
      /// <returns></returns>
      public virtual Task<Response<Charge>> CancelChargeAsync(string chargeCode, CancellationToken cancellationToken = default)
      {
         return ChargesEndpoint
            .AppendPathSegment(chargeCode)
            .AppendPathSegment("cancel")
            .PostAsync(null, cancellationToken)
            .ReceiveJson<Response<Charge>>();
      }

      /// <summary>
      /// Resolve a charge that has been previously marked as unresolved.
      /// Supply the unique charge code that was returned when the charge was created.
      /// Note: Only unresolved charges can be successfully resolved.
      /// For more on unresolved charges, check out at <see href="https://commerce.coinbase.com/docs/api/#charge-timeline">Charge timeline</see>
      /// </summary>
      /// <param name="chargeCode">The Charge.Code</param>
      /// <returns></returns>
      public virtual Task<Response<Charge>> ResolveChargeAsync(string chargeCode, CancellationToken cancellationToken = default)
      {
         return ChargesEndpoint
            .AppendPathSegment(chargeCode)
            .AppendPathSegment("resolve")
            .PostAsync(null, cancellationToken)
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
      public virtual Task<Response<Checkout>> CreateCheckoutAsync(CreateCheckout checkout, CancellationToken cancellationToken = default)
      {
         return CheckoutEndpoint
            .PostJsonAsync(checkout, cancellationToken)
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
      /// Lists all the invoices
      /// </summary>
      /// <param name="listOrder">Order of the resources in the response. desc (default), asc</param>
      /// <param name="limit">umber of results per call. Accepted values: 0 - 100. Default 25</param>
      /// <param name="startingAfter">A cursor for use in pagination. starting_after is a resource ID that defines your place in the list.</param>
      /// <param name="endingBefore">A cursor for use in pagination. ending_before is a resource ID that defines your place in the list.</param>
      public virtual Task<PagedResponse<Invoice>> ListInvoicesAsync(ListOrder? listOrder = null, int? limit = null, string startingAfter = null, string endingBefore = null, CancellationToken cancellationToken = default)
      {
         return InvoicesEndpoint
            .SetQueryParam("order", listOrder?.ToString().ToLower())
            .SetQueryParam("limit", limit)
            .SetQueryParam("starting_after", startingAfter)
            .SetQueryParam("ending_before", endingBefore)
            .GetJsonAsync<PagedResponse<Invoice>>(cancellationToken);
      }

      /// <summary>
      /// Retrieves the details of an invoice that has been previously created. Supply the unique
      /// short code that was returned when the invoice was created. This information is
      /// also returned when an invoice is first created.
      /// </summary>
      /// <param name="codeOrId">Invoice code or ID</param>
      public virtual Task<Response<Invoice>> GetInvoiceAsync(string codeOrId, CancellationToken cancellationToken = default)
      {
         return InvoicesEndpoint
            .AppendPathSegment(codeOrId)
            .GetJsonAsync<Response<Invoice>>(cancellationToken);
      }

      /// <summary>
      /// To send an invoice in cryptocurrency, you need to create an invoice object and provide
      /// the user with the hosted url where they will be able to pay. Once an invoice is
      /// viewed at the hosted url, a charge will be generated on the invoice.
      /// </summary>
      /// <param name="invoice">The invoice to create</param>
      /// <returns></returns>
      public virtual Task<Response<Invoice>> CreateInvoiceAsync(CreateInvoice invoice, CancellationToken cancellationToken = default)
      {
         return InvoicesEndpoint
            .PostJsonAsync(invoice, cancellationToken)
            .ReceiveJson<Response<Invoice>>();
      }

      /// <summary>
      /// Voids an invoice that has been previously created. Supply the unique invoice code that
      /// was returned when the invoice was created.
      /// Note: Only invoices with OPEN or VIEWED status can be voided. Once a payment is detected,
      /// the invoice can no longer be voided.
      /// </summary>
      /// <param name="codeOrId">Invoice code or id</param>
      public virtual Task<Response<Invoice>> VoidInvoiceAsync(string codeOrId, CancellationToken cancellationToken = default)
      {
         return InvoicesEndpoint
            .AppendPathSegments(codeOrId, "void")
            .PostAsync(null, cancellationToken)
            .ReceiveJson<Response<Invoice>>();
      }
      

      /// <summary>
      /// Resolve an invoice that has been previously marked as unresolved. Supply the unique
      /// invoice code that was returned when the invoice was created.
      /// Note: Only invoices with an unresolved charge can be successfully resolved.For more
      /// on unresolved charges, check out at Charge timeline: https://commerce.coinbase.com/docs/api/#charge-timeline
      /// </summary>
      /// <param name="codeOrId">Invoice code or id</param>
      public virtual Task<Response<Invoice>> ResolveInvoiceAsync(string codeOrId, CancellationToken cancellationToken = default)
      {
         return InvoicesEndpoint
            .AppendPathSegments(codeOrId, "resolve")
            .PostAsync(null, cancellationToken)
            .ReceiveJson<Response<Invoice>>();
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
