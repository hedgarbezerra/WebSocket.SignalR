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
        Guid AddMovie(Movie movie, IEnumerable<Guid> genresIds = null!);
        bool UpdateMovie(Movie movie, IEnumerable<Guid> genresIds = null!);
        bool DeleteMovie(Guid movie);
        bool MovieExists(Guid id);
        bool AddGenreToMovie(Genre genre, Movie movie);
        bool AddGenreToMovie(Guid genreId, Guid movieId);
        bool AddGenresToMovie(IEnumerable<Guid> genresIds, Guid movieId);

        Genre? GetGenre(Guid genreId);
        IReadOnlyList<Genre> GetGenres();
        IReadOnlyList<Genre> GetGenres(Guid movieId);
        IReadOnlyList<Genre> GetGenres(Expression<Func<Genre, bool>> filter);
        PaginatedList<Genre> GetGenres(PaginationInput pagination, string route);
        Guid AddGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Guid genreId);
        bool GenreExists(Guid id);
    }
}
