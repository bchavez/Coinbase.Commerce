using System.Net.Http;
using System.Threading.Tasks;
using Coinbase.Commerce.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Coinbase.Tests.ApiTests
{
   public class EventTests : CommerceApiTests
   {
      [Test]
      public async Task can_list_events()
      {
         SetupServerPagedResponse(PageData.Pagination, EventData.Event);

         var events = await api.ListEventsAsync();

         var truth = new PagedResponse<Event>()
            {
               Pagination = PageData.PaginationModel,
               Data = new[] { EventData.EventModel }
            };

         truth.Should().BeEquivalentTo(events);

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/events")
            .WithVerb(HttpMethod.Get);

      }

      [Test]
      public async Task list_events_with_different_ordering()
      {
         SetupServerPagedResponse(PageData.Pagination, EventData.Event);

         await api.ListEventsAsync(ListOrder.Asc, 75, "ffff", "gggg");

         server.ShouldHaveCalled("https://api.commerce.coinbase.com/events?order=asc&limit=75&starting_after=ffff&ending_before=gggg")
            .WithVerb(HttpMethod.Get);
      }

      [Test]
      public async Task can_get_a_single_event()
      {
         SetupServerSingleResponse(EventData.Event);

         var charge = await api.GetEventAsync(EventData.EventModel.Id);

         var truth = new Response<Event>
            {
               Data = EventData.EventModel
            };

         charge.Should().BeEquivalentTo(truth);

         server.ShouldHaveCalled($"https://api.commerce.coinbase.com/events/{EventData.EventModel.Id}")
            .WithVerb(HttpMethod.Get);
      }
   }
}
