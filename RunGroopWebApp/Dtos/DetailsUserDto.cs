namespace RunGroopWebApp.Dtos
{
    public class DetailsUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
    }
}
