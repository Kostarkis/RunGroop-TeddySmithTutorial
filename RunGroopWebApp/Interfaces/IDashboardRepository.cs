using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task<User> GetUserById(string id);
        Task<User> GetUserByIdNoTracking(string id);
        bool Update(User user);
        bool Save();
    }
}
