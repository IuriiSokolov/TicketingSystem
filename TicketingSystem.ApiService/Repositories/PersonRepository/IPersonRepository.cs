using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.ApiService.Repositories.PersonRepository
{
    public interface IPersonRepository
    {
        Task<Person> AddAsync(Person person);
        Task<bool> DeleteAsync(int id);
        Task<List<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<Person> UpdateAsync(Person person);
    }
}