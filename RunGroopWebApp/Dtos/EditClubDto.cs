using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Dtos
{
    public class EditClubDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Url { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
