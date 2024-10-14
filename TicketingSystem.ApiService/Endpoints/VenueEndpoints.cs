using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Repositories.VenueRepository;
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

        private async Task<Ok<List<VenueDto>>> GetVenues(IVenueRepository repo)
        {
            var venues = await repo.GetAllAsync();
            var dtos = venues.Select(venue => new VenueDto(venue)).ToList();
            return TypedResults.Ok(dtos);
        }


        private async Task<Ok<List<SectionDto>>> GetSectionsOfVenue(int venue_id, IVenueRepository repo)
        {
            var sections = await repo.GetSectionsAsync(venue_id);
            var dtos = sections.Select(section => new SectionDto(section)).ToList();
            return TypedResults.Ok(dtos);
        }
    }
}
