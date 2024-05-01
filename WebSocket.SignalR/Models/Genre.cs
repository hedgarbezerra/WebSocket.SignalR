namespace WebSocket.SignalR.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Movie> Movies { get; set; } = new List<Movie>();

        public static Genre Create(string name) => new Genre { Id = Guid.NewGuid(), Name = name };
    }
}
