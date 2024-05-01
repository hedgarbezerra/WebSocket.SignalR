namespace WebSocket.SignalR.Interfaces
{
    public interface IUriService
    {
        Uri GetPageUri(int pageIndex, int pageSize, string route);
        Uri GetUri(string route);
    }
}
