using System.Linq.Expressions;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;

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

        public Guid AddMovie(Movie movie)
        {
            ArgumentNullException.ThrowIfNull(movie);

            var insertedMovie = _moviesRepository.Add(movie);
            _moviesRepository.SaveChanges();

            return insertedMovie.Id;
        }

        public bool UpdateMovie(Movie movie)
        {
            ArgumentNullException.ThrowIfNull(movie);

            var updatedMovie = _moviesRepository.Update(movie);

            return _moviesRepository.SaveChanges();
        }

        public bool DeleteMovie(Guid movieId)
        {
            if (movieId == Guid.Empty)
                throw new ArgumentNullException(nameof(movieId));

            _moviesRepository.Delete(movieId);
            return _moviesRepository.SaveChanges();
        }

        public Movie? GetMovie(Guid movieId)
        {
            if (movieId == Guid.Empty)
                throw new ArgumentNullException(nameof(movieId));

            var movie = _moviesRepository.Get(movieId);
            return movie;
        }

        public IReadOnlyList<Movie> GetMovies()
        {
            var movies = _moviesRepository.Get().ToList().AsReadOnly();

            return movies;
        }

        public IReadOnlyList<Movie> GetMovies(Expression<Func<Movie, bool>> filter)
        {
            var movies = _moviesRepository.Get(filter).ToList().AsReadOnly();

            return movies;
        }

        public PaginatedList<Movie> GetMovies(PaginationInput pagination, string route)
        {
            var movies = _moviesRepository.Get();
            if(!string.IsNullOrEmpty(pagination.SearchTerm))
            {
                var actualTerm = pagination.SearchTerm.Split(' ');
                for (int i = 0; i < actualTerm.Length; i++)
                {
                    var term = actualTerm[i];
                    movies = movies.Where(t => t.Name.Contains(term));
                }
            }

            var paginated = new PaginatedList<Movie>(movies, _uriService, route, pagination.Index, pagination.Size);
            return paginated;
        }

        public Guid AddGenre(Genre genre)
        {
            ArgumentNullException.ThrowIfNull(genre);

            var insertedGenre = _genresRepository.Add(genre);
            _genresRepository.SaveChanges();

            return insertedGenre.Id;
        }


        public bool DeleteGenre(Guid genreId)
        {
            if(genreId == Guid.Empty)
                throw new ArgumentNullException(nameof(genreId));

            _genresRepository.Delete(genreId);

            return _genresRepository.SaveChanges();
        }

        public Genre? GetGenre(Guid genreId)
        {
            if (genreId == Guid.Empty)
                throw new ArgumentNullException(nameof(genreId));

            return _genresRepository.Get(genreId);
        }

        public IReadOnlyList<Genre> GetGenres()
        {
            var genres = _genresRepository.Get().ToList().AsReadOnly();

            return genres;
        }

        public IReadOnlyList<Genre> GetGenres(Guid movieId)
        {
            var movie = _moviesRepository.Get(movieId);
            if (movie is null)
                return [];
            
            var  genres = movie.Genres.AsReadOnly();

            return genres;
        }

        public IReadOnlyList<Genre> GetGenres(Expression<Func<Genre, bool>> filter)
        {
            var genres = _genresRepository.Get(filter).ToList().AsReadOnly();

            return genres;
        }

        public PaginatedList<Genre> GetGenres(PaginationInput pagination, string route)
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
            return paginated;
        }

        public bool UpdateGenre(Genre genre)
        {
            ArgumentNullException.ThrowIfNull(genre);

            var updatedGenre = _genresRepository.Update(genre);
            return _genresRepository.SaveChanges();
        }

        public bool AddGenreToMovie(Genre genre, Movie movie)
        {
            ArgumentNullException.ThrowIfNull(genre);
            ArgumentNullException.ThrowIfNull(movie);

            movie.Genres.Add(genre);

            return _moviesRepository.SaveChanges();
        }

        public bool AddGenreToMovie(Guid genreId, Guid movieId)
        {
            var genre = _genresRepository.Get(genreId);
            var movie = _moviesRepository.Get(movieId);
            if (movie is null || genre is null)
                return false;

            movie.Genres.Add(genre);

            return _moviesRepository.SaveChanges();
        }
    }
}
