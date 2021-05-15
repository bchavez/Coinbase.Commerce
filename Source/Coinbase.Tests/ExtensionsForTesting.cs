using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Testing;
using Newtonsoft.Json;

namespace Coinbase.Tests
{
   internal static class ExtensionsForTesting
   {
      public static void Dump(this object obj)
      {
         Console.WriteLine(obj.DumpString());
      }

      public static string DumpString(this object obj)
      {
         return JsonConvert.SerializeObject(obj, Formatting.Indented);
      }

      public static HttpTest RespondWithJsonTestFile(this HttpTest server,
         object headers = null,
         [CallerMemberName] string methodName = "",
         [CallerFilePath] string filePath = "")
      {
         var responseFile = Path.ChangeExtension(filePath, $"{methodName}.server.json");

         if (!File.Exists(responseFile))
         {
            var p = Process.Start("notepad.exe", responseFile);
            p.WaitForExit();

            if (!File.Exists(responseFile))
            {
               throw new FileNotFoundException($"*.server.json test file not found '{responseFile}'", responseFile);
            }
         }

         var json = File.ReadAllText(responseFile);
         server.RespondWith(json, headers: headers);
         return server;
      }

      public static HttpCallAssertion2 ShouldHaveCalledSomePathAndQuery(this HttpTest test, string pathAndQuery)
      {
         var paths = test.CallLog.Select(c => c.Request.Url.ToUri().PathAndQuery);

         paths.Should().Contain(pathAndQuery);

         return new HttpCallAssertion2(test.CallLog);
      }

      public static HttpCallAssertion2 ShouldHaveCalledSomePath(this HttpTest test, string path)
      {
         var paths = test.CallLog.Select(c => c.Request.Url.Path);

         paths.Should().Contain(path);

         return new HttpCallAssertion2(test.CallLog);
      }

   }

   public class HttpCallAssertion2 : HttpCallAssertion
   {
      public IEnumerable<FlurlCall> LoggedCalls { get; }

      public HttpCallAssertion2(IEnumerable<FlurlCall> loggedCalls, bool negate = false) : base(loggedCalls, negate)
      {
         this.LoggedCalls = loggedCalls;
      }
   }

}
