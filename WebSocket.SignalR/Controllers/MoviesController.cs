using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models.DTOs;
using WebSocket.SignalR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentResults;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : BaseAuthenticatedController
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;

        public MoviesController(ILogger<MoviesController> logger, IMoviesService moviesService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(moviesService, nameof(moviesService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _logger = logger;
            _moviesService = moviesService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<Movie>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var movies = _moviesService.GetMovies();

            return Ok(movies);
        }

        [HttpGet]
        [Route("{movieId:guid}")]
        [ProducesResponseType(typeof(Result<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] Guid movieId)
        {
            var movie = _moviesService.GetMovie(movieId);
            if (movie is null)
                return NotFound();

            return Ok(movie);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(Result<PaginatedList<Movie>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var movies = _moviesService.GetMovies(pagination, HttpContext?.Request?.Path);

            return Ok(movies);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateMovieDTO request)
        {
            var user = GetAuthentcatedUser();
            var movie = _mapper.Map<Movie>(request);
            var movieId = _moviesService.AddMovie(movie, request.Genres);

            return Ok(movieId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateMovieDTO request)
        {
            var movieExists = _moviesService.MovieExists(request.Id);
            if (movieExists.IsFailed)
                return NotFound(movieExists);

            var movie = _mapper.Map<Movie>(request);
            var movies = _moviesService.UpdateMovie(movie);

            return Ok(movies);
        }

        [HttpDelete]
        [Route("{movieId:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid movieId)
        {
            var movieExists = _moviesService.MovieExists(movieId);
            if (movieExists.IsFailed)
                return NotFound(movieExists);

            var result = _moviesService.DeleteMovie(movieId);

            return Ok(result);
        }


        [HttpPost]
        [Route("genres/assign")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AssignGenresToMovie([FromBody] AssignGenresToMovieDTO request)
        {
            var movieExists = _moviesService.MovieExists(request.Id);
            if (movieExists.IsFailed)
                return NotFound(movieExists);

            var result = _moviesService.AddGenresToMovie(request.Genres, request.Id);

            return Ok(result);
        }

        [HttpPost]
        [Route("{movieId:guid}/genre/assign/{genreId:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AssignGenreToMovie([FromRoute] Guid movieId, [FromRoute] Guid genreId)
        {
            var movieExists = _moviesService.MovieExists(movieId);
            if (movieExists.IsFailed)
                return NotFound(movieExists);

            var result = _moviesService.AddGenreToMovie(genreId, movieId);

            return Ok(result);
        }
    }
}
