using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models.DTOs;
using WebSocket.SignalR.Models;
using Microsoft.AspNetCore.Identity;
using FluentResults;
using WebSocket.SignalR.Extensions;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : BaseAuthenticatedController
    {
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesService moviesService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(moviesService, nameof(moviesService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _moviesService = moviesService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDTO<IEnumerable<Movie>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var movies = _moviesService.GetMovies().FromResult();

            return Ok(movies);
        }

        [HttpGet]
        [Route("{movieId:guid}")]
        [ProducesResponseType(typeof(ResultDTO<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] Guid movieId)
        {
            var movie = _moviesService.GetMovie(movieId).FromResult();
            if (!movie.Success)
                return NotFound(movie);

            return Ok(movie);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(ResultDTO<PaginatedList<Movie>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var movies = _moviesService.GetMovies(pagination, HttpContext?.Request?.Path).FromResult();

            return Ok(movies);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDTO<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateMovieDTO request)
        {
            var movie = _mapper.Map<Movie>(request);
            var movieId = _moviesService.AddMovie(movie, request.Genres).FromResult();

            return Created(HttpContext.Request.Path, movieId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateMovieDTO request)
        {
            var movieExists = _moviesService.MovieExists(request.Id).FromResult();
            if (!movieExists.Success)
                return NotFound(movieExists);

            var movie = _mapper.Map<Movie>(request);
            var movies = _moviesService.UpdateMovie(movie).FromResult();

            return Ok(movies);
        }

        [HttpDelete]
        [Route("{movieId:guid}")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid movieId)
        {
            var movieExists = _moviesService.MovieExists(movieId).FromResult();
            if (!movieExists.Success)
                return NotFound(movieExists);

            var result = _moviesService.DeleteMovie(movieId);

            return Ok(result.FromResult());
        }


        [HttpPost]
        [Route("genres/assign")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult AssignGenresToMovie([FromBody] AssignGenresToMovieDTO request)
        {
            var movieExists = _moviesService.MovieExists(request.Id).FromResult();
            if (!movieExists.Success)
                return NotFound(movieExists);

            var result = _moviesService.AddGenresToMovie(request.Genres, request.Id).FromResult();

            return Ok(result);
        }

        [HttpPost]
        [Route("{movieId:guid}/genre/assign/{genreId:guid}")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult AssignGenreToMovie([FromRoute] Guid movieId, [FromRoute] Guid genreId)
        {
            var movieExists = _moviesService.MovieExists(movieId).FromResult();
            if (!movieExists.Success)
                return NotFound(movieExists);

            var result = _moviesService.AddGenreToMovie(genreId, movieId).FromResult();

            return Ok(result);
        }
    }
}
