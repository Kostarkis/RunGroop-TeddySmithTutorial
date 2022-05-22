using RunGroopWebApp.Models;

namespace RunGroopWebApp.Dtos
{
    public class IndexDashboardDto
    {
        public List<Race> Races { get; set; }
        public List<Club> Clubs { get; set; }
    }
}
