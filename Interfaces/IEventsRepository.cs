using RepasoAhoraSi.Data;

namespace RepasoAhoraSi.Interfaces
{
    public interface IEventsRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Events>> GetAll();
        Task<Events> GetOne(int id);
        Task<int> Insert(Events events);
        Task<bool> Update(Events events);
    }
}