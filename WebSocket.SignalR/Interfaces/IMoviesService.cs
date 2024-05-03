using FluentResults;
using System.Linq.Expressions;
using WebSocket.SignalR.Models;

namespace WebSocket.SignalR.Interfaces
{
    public interface IMoviesService
    {
        Result<Movie?> GetMovie(Guid id);
        Result<List<Movie>> GetMovies();
        Result<List<Movie>> GetMovies(Expression<Func<Movie, bool>> filter);
        Result<PaginatedList<Movie>> GetMovies(PaginationInput pagination, string route);
        Result<Guid> AddMovie(Movie movie, IEnumerable<Guid> genresIds = null!);
        Result UpdateMovie(Movie movie, IEnumerable<Guid> genresIds = null!);
        Result DeleteMovie(Guid movie);
        Result MovieExists(Guid id);
        Result AddGenreToMovie(Genre genre, Movie movie);
        Result AddGenreToMovie(Guid genreId, Guid movieId);
        Result AddGenresToMovie(IEnumerable<Guid> genresIds, Guid movieId);

        Result<Genre?> GetGenre(Guid genreId);
        Result<List<Genre>> GetGenres();
        Result<List<Genre>> GetGenres(Guid movieId);
        Result<List<Genre>> GetGenres(Expression<Func<Genre, bool>> filter);
        Result<PaginatedList<Genre>> GetGenres(PaginationInput pagination, string route);
        Result<Guid> AddGenre(Genre genre);
        Result UpdateGenre(Genre genre);
        Result DeleteGenre(Guid genreId);
        Result GenreExists(Guid id);
    }
}
