using System.Linq.Expressions;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Interfaces
{
    public interface IMoviesService
    {
        Movie? GetMovie(Guid id);
        IReadOnlyList<Movie> GetMovies();
        IReadOnlyList<Movie> GetMovies(Expression<Func<Movie, bool>> filter);
        PaginatedList<Movie> GetMovies(PaginationInput pagination, string route);
        Guid AddMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Guid movie);
        bool AddGenreToMovie(Genre genre, Movie movie);
        bool AddGenreToMovie(Guid genreId, Guid movieId);

        Genre? GetGenre(Guid genreId);
        IReadOnlyList<Genre> GetGenres();
        IReadOnlyList<Genre> GetGenres(Guid movieId);
        IReadOnlyList<Genre> GetGenres(Expression<Func<Genre, bool>> filter);
        PaginatedList<Genre> GetGenres(PaginationInput pagination, string route);
        Guid AddGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Guid genreId);        

    }
}
