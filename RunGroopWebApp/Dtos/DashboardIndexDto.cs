using RunGroopWebApp.Models;

namespace RunGroopWebApp.Dtos
{
    public class DashboardIndexDto
    {
        public List<Race> Races { get; set; }
        public List<Club> Clubs { get; set; }
    }
}
