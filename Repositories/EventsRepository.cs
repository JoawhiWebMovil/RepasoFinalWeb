using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepasoAhoraSi.Data;
using RepasoAhoraSi.Interfaces;

namespace RepasoAhoraSi.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventosDbContext _dbContext;

        public EventsRepository(EventosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Select Events
        public async Task<IEnumerable<Events>> GetAll()
        {
            var events = await _dbContext.Events.ToListAsync();
            return events;
        }

        // Select Events by id
        public async Task<Events> GetOne(int id)
        {
            var events = await _dbContext
                            .Events
                            .FirstOrDefaultAsync(a => a.Id == id);

            return events;
        }

        //Create Events
        public async Task<int> Insert(Events events)
        {
            await _dbContext.Events.AddAsync(events);
            int rows = await _dbContext.SaveChangesAsync();

            return rows > 0 ? events.Id : -1;
        }

        //Update Events
        public async Task<bool> Update(Events events)
        {
            _dbContext.Events.Update(events);
            int rows = await _dbContext.SaveChangesAsync();

            return rows > 0;
        }


        //Delete Events
        public async Task<bool> Delete(int id)
        {
            var events = await _dbContext
                            .Events
                            .FirstOrDefaultAsync(a => a.Id == id);

            if (events == null) return false;

            _dbContext.Events.Remove(events);
            int rows = await _dbContext.SaveChangesAsync();
            return (rows > 0);
        }
    }
}
