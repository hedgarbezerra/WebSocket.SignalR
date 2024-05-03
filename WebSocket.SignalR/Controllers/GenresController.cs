using Asp.Versioning;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;
using WebSocket.SignalR.Models.DTOs;

namespace WebSocket.SignalR.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BaseAuthenticatedController
    {
        private readonly ILogger<GenresController> _logger;
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;

        public GenresController(ILogger<GenresController> logger, IMoviesService moviesService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(moviesService, nameof(moviesService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _logger = logger;
            _moviesService = moviesService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<Genre>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var genres = _moviesService.GetGenres();

            return Ok(genres);
        }

        [HttpGet]
        [Route("{genreId:guid}")]
        [ProducesResponseType(typeof(Result<Genre>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] Guid genreId)
        {
            var genre = _moviesService.GetGenre(genreId);
            if (genre is null)
                return NotFound();

            return Ok(genre);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(Result<PaginatedList<Genre>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var genres = _moviesService.GetGenres(pagination, HttpContext?.Request?.Path);

            return Ok(genres);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateGenreDTO request)
        {
            var genre = _mapper.Map<Genre>(request);
            var genreId = _moviesService.AddGenre(genre);

            return Ok(genreId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateGenreDTO request)
        {
            var genreExists = _moviesService.GenreExists(request.Id);
            if (genreExists.IsFailed)
                return NotFound(genreExists);

            var genre = _mapper.Map<Genre>(request);
            var genres = _moviesService.UpdateGenre(genre);

            return Ok(genres);
        }


        [HttpDelete]
        [Route("{genreId:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid genreId)
        {
            var genreExists = _moviesService.GenreExists(genreId);
            if (genreExists.IsFailed)
                return NotFound(genreExists);

            var result = _moviesService.DeleteGenre(genreId);

            return Ok(result);
        }
    }
}
