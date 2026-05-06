using first_sprint.Models;

namespace first_sprint.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAsync();
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event> CreateAsync(Event eventItem);
        Task<Event?> UpdateAsync(Guid id, Event updatedEvent);
        Task<bool> DeleteAsync(Guid id);
    }
}
