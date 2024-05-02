using System.Text.Json.Serialization;

namespace WebSocket.SignalR.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }
        public string Language { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsFull { get => Room?.Seats.Count == SeatsTaken.Count; }
        [JsonIgnore]
        public virtual List<SeatTaken> SeatsTaken { get; set; } = new List<SeatTaken>();
        [JsonIgnore]
        public virtual Movie? Movie { get; set; }
        [JsonIgnore]
        public virtual Room? Room { get; set; }

        public static Session Create(Guid movieId, Guid roomId, string language, DateTime date)
        {
            var session = new Session()
            {
                Id = Guid.NewGuid(),
                MovieId = movieId,
                RoomId = roomId,
                Language = language,
                Date = date
            };

            return session;
        }
    }
}
