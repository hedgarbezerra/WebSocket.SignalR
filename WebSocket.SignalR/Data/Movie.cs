namespace WebSocket.SignalR.Data
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sinopsys { get; set; } = string.Empty;
        public string Classification { get; set; } = string.Empty;
        public string DirectorName { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public DateTime Release { get; set; }
        public virtual List<Genre> Genres { get; set; } = new List<Genre>();
        public virtual List<Session> Sessions { get; set; } = new List<Session>();
        public List<string> Starring { get; set; } = new List<string>();
    }

    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
