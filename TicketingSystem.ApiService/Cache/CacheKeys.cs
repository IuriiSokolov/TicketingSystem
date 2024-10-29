namespace TicketingSystem.ApiService.Cache
{
    public static class CacheKeys
    {
        public static string GetSeatsOfSectionOfEvent(int eventId, int sectionId) =>
            $"api/events/{eventId}/sections/{sectionId}/seats";
    }
}
