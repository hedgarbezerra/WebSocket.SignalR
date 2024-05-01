namespace WebSocket.SignalR.Models
{
    public class PaginationInput
    {
        public PaginationInput(int index = 1, int size = 10, string searchTerm = "")
        {
            Index = index;
            Size = size;
            SearchTerm = searchTerm;
        }

        public int Index { get; }
        public int Size { get; } 
        public string SearchTerm { get; } 
    }
}
