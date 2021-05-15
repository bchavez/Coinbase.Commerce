using Coinbase.Commerce;
using Flurl.Http.Testing;
using NUnit.Framework;
using VerifyTests;

namespace Coinbase.Tests.ApiTests
{
   public class CommerceApiTests
   {
      protected HttpTest server;
      protected CommerceApi api;
      public string apiKey = "DBBD0428-B818-4F53-A5F4-F553DC4C374C";

      [SetUp]
      public void BeforeEachTest()
      {
         VerifierSettings.UseStrictJson();
         server = new HttpTest();

         api = new CommerceApi(apiKey);
      }

      [TearDown]
      public void AfterEachTest()
      {
         EnsureEveryRequestHasCorrectHeaders();
         server.Dispose();
      }

      void EnsureEveryRequestHasCorrectHeaders()
      {
         server.ShouldHaveMadeACall()
            .WithHeader(HeaderNames.Version, CommerceApi.ApiVersionDate)
            .WithHeader(HeaderNames.ApiKey, apiKey)
            .WithHeader("User-Agent", CommerceApi.UserAgent);
      }

      protected void SetupServerPagedResponse(string pageJson, string dataJson)
      {
         var json = @"{
    ""pagination"": {pageJson},
    ""data"": [{dataJson}]
}
".Replace("{dataJson}", dataJson)
            .Replace("{pageJson}", pageJson);

         server.RespondWith(json);
      }

      protected void SetupServerSingleResponse(string dataJson)
      {
         var json = @"{
    ""data"": {dataJson}
}
".Replace("{dataJson}", dataJson);

         server.RespondWith(json);
      }
   }
}
