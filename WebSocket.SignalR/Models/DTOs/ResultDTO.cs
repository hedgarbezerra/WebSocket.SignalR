namespace WebSocket.SignalR.Models.DTOs
{
    public class ResultDTO<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Successes { get; set; }
        public static ResultDTO<T> Create(bool success,  T? data, List<string> errors, List<string> successes) =>
            new ResultDTO<T> { Success = success, Data = data, Errors = errors, Successes = successes };
    }
    public class ResultDTO
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Successes { get; set; }
        public static ResultDTO Create(bool success, List<string> errors, List<string> successes) =>
            new ResultDTO { Success = success, Errors = errors, Successes = successes };
    }
}
