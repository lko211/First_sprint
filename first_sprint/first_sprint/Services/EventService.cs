using first_sprint.Models;

namespace first_sprint.Services
{
    public class EventService : IEventService
    {
        private static readonly List<Event> _events = new();
        private readonly object _lock = new();

        public Task<IEnumerable<Event>> GetAsync()
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    return _events.ToList().AsEnumerable();
                }
            });
        }

        public Task<Event?> GetByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    return _events.FirstOrDefault(e => e.Id == id);
                }
            });
        }

        public Task<Event> CreateAsync(Event eventItem)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    eventItem.Id = Guid.NewGuid();
                    _events.Add(eventItem);
                    return eventItem;
                }
            });
        }
        public Task<Event?> UpdateAsync(Guid id, Event updatedEvent)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    var existingEvent = _events.FirstOrDefault(e => e.Id == id);
                    if (existingEvent == null)
                    {
                        return null;
                    }
                    else
                    {
                        existingEvent.Title = updatedEvent.Title;
                        existingEvent.Description = updatedEvent.Description;
                        existingEvent.StartAt = updatedEvent.StartAt;
                        existingEvent.EndAt = updatedEvent.EndAt;
                        return existingEvent;
                    }
                }
            });
        }
        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    var eventItem = _events.FirstOrDefault(e => e.Id == id);
                    if (eventItem == null)
                    {
                        return false;
                    }
                    else
                    {
                        return _events.Remove(eventItem);
                    }
                }
            });
        }
    }
}
