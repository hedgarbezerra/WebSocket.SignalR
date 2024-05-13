using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;
using static WebSocket.SignalR.Data.Repository.Repositories;

namespace WebSocket.SignalR.Services
{
    [BindInterface(typeof(IMoviesService))]
    public class MoviesService : IMoviesService
    {
        private readonly IRepository<Movie> _moviesRepository;
        private readonly IRepository<Genre> _genresRepository;
        private readonly IUriService _uriService;

        public MoviesService(IRepository<Movie> moviesRepository, IRepository<Genre> genresRepository, IUriService uriService)
        {
            ArgumentNullException.ThrowIfNull(moviesRepository, nameof(moviesRepository));
            ArgumentNullException.ThrowIfNull(genresRepository, nameof(genresRepository));
            ArgumentNullException.ThrowIfNull(uriService, nameof(uriService));

            _moviesRepository = moviesRepository;
            _genresRepository = genresRepository;
            _uriService = uriService;
        }


        public Result<Guid> AddMovie(Movie movie)
        {
            if (movie is null)
                return Result.Fail($"The parameter '{nameof(movie)}' provided cannot be null.");

            var insertedMovie = _moviesRepository.Add(movie);

            return Result.Ok(_moviesRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok(insertedMovie.Id).WithSuccess($"Movie '{movie.Name}' created with identifier '{movie.Id}'.")
                    : Result.Fail($"Movie '{movie.Name}' was not created."));
        }
        public Result<Guid> AddMovie(Movie movie, IEnumerable<Guid> genresIds = null!)
        {
            if (movie is null)
                return Result.Fail($"The parameter '{nameof(movie)}' provided cannot be null.");

            var insertedMovie = _moviesRepository.Add(movie);
            if (genresIds is not null)
            {
                var genres = genresIds.Select(id => _genresRepository.Get(id)).Where(g => g is not null).ToList();
                movie.Genres = genres;
            }

            return Result.Ok(_moviesRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok(insertedMovie.Id).WithSuccess($"Movie '{movie.Name}' created successfully.")
                    : Result.Fail($"Movie '{movie.Name}' was not created."));
        }

        public Result UpdateMovie(Movie movie, IEnumerable<Guid> genresIds = null!)
        {
            if (movie is null)
                return Result.Fail($"The parameter '{nameof(movie)}' provided cannot be null.");

            if (genresIds is not null)
            {
                var genres = genresIds.Select(id => _genresRepository.Get(id)).Where(g => g is not null).ToList();
                movie.Genres = genres;
            }
            _moviesRepository.Update(movie);

            return Result.Ok(_moviesRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Movie '{movie.Name}' updated successfully.")
                    : Result.Fail($"Movie '{movie.Name}' was not updated."));
        }
        public Result DeleteMovie(Guid movieId)
        {
            var result = MovieExists(movieId);
            if (result.IsFailed)
                return result;
            _moviesRepository.Delete(movieId);

            return Result.Ok(_moviesRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Movie with identifier '{movieId}' successfully deleted.")
                    : Result.Fail($"Movie with identifier '{movieId}' was not deleted."));
        }
        public Result MovieExists(Guid movieId)
        {
            bool movieFound = _moviesRepository.Get(m => m.Id == movieId).Any();

            return Result.OkIf(movieFound, $"Movie with identifier '{movieId}' not found.");
        }
        public Result<Movie?> GetMovie(Guid movieId)
        {
            var movie = _moviesRepository.Get(movieId);

            return Result.Ok(movie is not null)
                .Bind(v => v ?
                    Result.Ok(movie)
                    : Result.Fail($"Movie with identifier '{movieId}' not found."));
        }
        public Result<List<Movie>> GetMovies()
        {
            var movies = _moviesRepository.Get().ToList();

            return Result.Ok(movies).WithSuccess($"Total count of movies: {movies.Count}.");
        }
        public Result<List<Movie>> GetMovies(Expression<Func<Movie, bool>> filter)
        {
            var movies = _moviesRepository.Get(filter).ToList();

            return Result.Ok(movies).WithSuccess($"Total count of movies: {movies.Count}.");
        }
        public Result<PaginatedList<Movie>> GetMovies(PaginationInput pagination, string route)
        {
            var movies = _moviesRepository.Get();
            if (!string.IsNullOrEmpty(pagination.SearchTerm))
            {
                var actualTerm = pagination.SearchTerm.Split(' ');
                for (int i = 0; i < actualTerm.Length; i++)
                {
                    var term = actualTerm[i];
                    movies = movies.Where(t => t.Name.Contains(term));
                }
            }

            var paginated = new PaginatedList<Movie>(movies, _uriService, route, pagination.Index, pagination.Size);
            return Result.Ok(paginated).WithSuccess($"page '{paginated.PageIndex}' from '{paginated.TotalPages}' - Total itens: {paginated.TotalCount}");
        }

        public Result<Guid> AddGenre(Genre genre)
        {
            if (genre is null)
                return Result.Fail($"The parameter '{nameof(genre)}' provided cannot be null.");

            var insertedMovie = _genresRepository.Add(genre);
            _genresRepository.SaveChanges();

            return Result.Ok(insertedMovie.Id)
                .WithSuccess(new Success($"The genre at '{genre.Name}' was created with identifier '{insertedMovie.Id}'"));
        }
        public Result UpdateGenre(Genre genre)
        {
            if (genre is null)
                return Result.Fail($"The parameter '{nameof(genre)}' provided cannot be null.");

            var updatedMovie = _genresRepository.Update(genre);

            return Result.Ok(_genresRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Genre with identifier '{genre.Id}' updated successfully.")
                    : Result.Fail($"Genre with identifier '{genre.Id}' was not updated."));
        }
        public Result DeleteGenre(Guid genreId)
        {
            var result = GenreExists(genreId);
            if (result.IsFailed)
                return result;
            _genresRepository.Delete(genreId);

            return Result.Ok(_genresRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Genre with identifier '{genreId}' successfully deleted.")
                    : Result.Fail($"Genre with identifier '{genreId}' was not deleted."));
        }
        public Result GenreExists(Guid genreId)
        {
            bool genreFound = _genresRepository.Get(m => m.Id == genreId).Any();

            return Result.OkIf(genreFound, $"Genre with identifier '{genreId}' not found.");
        }
        public Result<Genre?> GetGenre(Guid genreId)
        {
            var genre = _genresRepository.Get(genreId);

            return Result.Ok(genre is not null)
                .Bind(v => v ?
                    Result.Ok(genre)
                    : Result.Fail($"Genre with identifier '{genreId}' not found."));
        }
        public Result<List<Genre>> GetGenres()
        {
            var genres = _genresRepository.Get().ToList();

            return Result.Ok(genres).WithSuccess($"Total count of genres: {genres.Count}.");
        }
        public Result<List<Genre>> GetGenres(Guid movieId)
        {
            var movieResult = GetMovie(movieId);
            if (movieResult.IsFailed)
                return movieResult.ToResult();

            return Result.Ok(movieResult.Value.Genres);
        }
        public Result<List<Genre>> GetGenres(Expression<Func<Genre, bool>> filter)
        {
            var genres = _genresRepository.Get(filter).ToList();

            return Result.Ok(genres).WithSuccess($"Total count of genres: {genres.Count}.");
        }
        public Result<PaginatedList<Genre>> GetGenres(PaginationInput pagination, string route)
        {
            var genres = _genresRepository.Get();
            if (!string.IsNullOrEmpty(pagination.SearchTerm))
            {
                var actualTerm = pagination.SearchTerm.Split(' ');
                for (int i = 0; i < actualTerm.Length; i++)
                {
                    var term = actualTerm[i];
                    genres = genres.Where(t => t.Name.Contains(term));
                }
            }

            var paginated = new PaginatedList<Genre>(genres, _uriService, route, pagination.Index, pagination.Size);
            return Result.Ok(paginated).WithSuccess($"page '{paginated.PageIndex}' from '{paginated.TotalPages}' - Total itens: {paginated.TotalCount}");
        }

        public Result AddGenreToMovie(Genre genre, Movie movie)
        {
            var result = Result.Ok();
            if (genre is null)
                result.WithError($"The parameter '{nameof(genre)}' cannot be null.");
            if (movie is null)
                result.WithError($"The parameter '{nameof(movie)}' cannot be null.");

            if (result.IsFailed)
                return result;

            if (GenreExists(genre.Id).IsFailed)
                _genresRepository.Add(genre);

            movie.Genres.Add(genre);

            return Result.Ok(_genresRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Genre '{genre.Name}' was assigned to '{movie.Name}'.")
                    : Result.Fail($"Genre '{genre.Name} was not assigned to '{movie.Name}'."));
        }

        public Result AddGenreToMovie(Guid genreId, Guid movieId)
        {
            var genreResult = GetGenre(genreId);
            var movieResult = GetMovie(movieId);
            if (movieResult.IsFailed || genreResult.IsFailed)
                return Result.Merge(genreResult, movieResult);

            if (movieResult.Value.Genres.Any(g => g.Id == genreId))
                return Result.Fail($"Genre '{genreResult.Value}' already associated with the movie '{movieResult.Value.Name}'.");

            movieResult.Value.Genres.Add(genreResult.Value);

            return Result.Ok(_genresRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Genre '{genreResult.Value.Name}' was assigned to '{movieResult.Value.Name}'.")
                    : Result.Fail($"Genre '{genreResult.Value.Name} was not assigned to '{movieResult.Value.Name}'."));
        }

        public Result AddGenresToMovie(IEnumerable<Guid> genresIds, Guid movieId)
        {
            var movieResult = GetMovie(movieId);
            if (movieResult.IsFailed)
                return movieResult.ToResult();

            var genres = genresIds.Select(_genresRepository.Get).Where(g => g is not null);
            movieResult.Value.Genres.AddRange(genres);

            return Result.Ok(_genresRepository.SaveChanges())
                .Bind(v => v ?
                    Result.Ok().WithSuccess($"Genres were assigned to '{movieResult.Value.Name}'.")
                    : Result.Fail($"Genre were not assigned to '{movieResult.Value.Name}'."));
        }
    }
}
