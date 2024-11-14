namespace TicketingSystem.Tests.Integration.Helpers
{
    public static class IdGenerator
    {
        private static readonly Random _rng = new(Guid.NewGuid().GetHashCode());
        public static int Next()
        {
            return _rng.Next();
        }
    }
}
