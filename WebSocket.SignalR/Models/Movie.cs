namespace WebSocket.SignalR.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sinopsys { get; set; } = string.Empty;
        public int Classification { get; set; }
        public string DirectorName { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public DateTime Release { get; set; }
        public List<string> Starring { get; set; } = new List<string>();
        public virtual List<Genre> Genres { get; set; } = new List<Genre>();

        public static Movie Create(string name, string sinopsys, int classification, string directorName, TimeSpan duration, DateTime release, List<string> starring)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Name = name,
                Sinopsys = sinopsys,
                Classification = classification,
                DirectorName = directorName,
                Duration = duration,
                Release = release,
                Starring = starring
            };
            return movie;
        }
    }
}
