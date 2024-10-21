using TicketingSystem.ApiService.Repositories.SectionRepository;
using TicketingSystem.ApiService.Repositories.VenueRepository;
using TicketingSystem.Common.Model.DTOs.Output;

namespace TicketingSystem.ApiService.Services.VenueService
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        private readonly ISectionRepository _sectionRepository;

        public VenueService(IVenueRepository venueRepository, ISectionRepository sectionRepository)
        {
            _venueRepository = venueRepository;
            _sectionRepository = sectionRepository;
        }

        public async Task<List<VenueDto>> GetAllAsync()
        {
            var venues = await _venueRepository.GetAllAsync();
            var dtos = venues.Select(venue => new VenueDto(venue)).ToList();
            return dtos;
        }

        public async Task<List<SectionDto>> GetSectionsAsync(int venueId)
        {
            var sections = await _sectionRepository.GetWhereAsync(venue => venue.VenueId == venueId);
            var dtos = sections.Select(section => new SectionDto(section)).ToList();
            return dtos;
        }
    }
}
