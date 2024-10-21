using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Services.VenueService;
using TicketingSystem.Common.Model.DTOs.Output;

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

        private async Task<Ok<List<VenueDto>>> GetVenues(IVenueService service)
        {
            var dtos = await service.GetAllAsync();
            return TypedResults.Ok(dtos);
        }


        private async Task<Ok<List<SectionDto>>> GetSectionsOfVenue(int venue_id, IVenueService service)
        {
            var dtos = await service.GetSectionsAsync(venue_id);
            return TypedResults.Ok(dtos);
        }
    }
}
