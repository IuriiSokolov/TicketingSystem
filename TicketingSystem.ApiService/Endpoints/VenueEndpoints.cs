
using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Repositories.VenueRepository;
using TicketingSystem.Common.Model.Database.Entities;
using TicketingSystem.Common.Model.DTOs;

namespace TicketingSystem.ApiService.Endpoints
{
    public class VenueEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var venueGroup = app.MapGroup("api/venues");
            venueGroup.MapGet("", GetVenues);
            venueGroup.MapGet("{venue_id}/sections", GetSectionsOfVenue);
        }

        private async Task<Ok<List<Venue>>> GetVenues(IVenueRepository repo)
        {
            var venues = await repo.GetAllAsync();
            return TypedResults.Ok(venues);
        }


        private async Task<Ok<List<Section>>> GetSectionsOfVenue(int venue_id, IVenueRepository repo)
        {
            var sections = await repo.GetSectionsAsync(venue_id);
            return TypedResults.Ok(sections);
        }
    }
}
