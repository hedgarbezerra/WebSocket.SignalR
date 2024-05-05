using Asp.Versioning;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Extensions;
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
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;

        public GenresController(IMoviesService moviesService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            ArgumentNullException.ThrowIfNull(moviesService, nameof(moviesService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _moviesService = moviesService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultDTO<IEnumerable<Genre>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var genres = _moviesService.GetGenres().FromResult();

            return Ok(genres);
        }

        [HttpGet]
        [Route("{genreId:guid}")]
        [ProducesResponseType(typeof(ResultDTO<Genre>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] Guid genreId)
        {
            var genre = _moviesService.GetGenre(genreId).FromResult();
            if (!genre.Success)
                return NotFound(genre);

            return Ok(genre);
        }

        [HttpGet]
        [Route("pagination")]
        [ProducesResponseType(typeof(ResultDTO<PaginatedList<Genre>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            var genres = _moviesService.GetGenres(pagination, HttpContext?.Request?.Path).FromResult();

            return Ok(genres);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultDTO<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CreateGenreDTO request)
        {
            var genre = _mapper.Map<Genre>(request);
            var genreId = _moviesService.AddGenre(genre).FromResult();

            return Created(HttpContext.Request.Path, genreId);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UpdateGenreDTO request)
        {
            var genreExists = _moviesService.GenreExists(request.Id).FromResult();
            if (!genreExists.Success)
                return NotFound(genreExists);

            var genre = _mapper.Map<Genre>(request);
            var genres = _moviesService.UpdateGenre(genre).FromResult();

            return Ok(genres);
        }


        [HttpDelete]
        [Route("{genreId:guid}")]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultDTO),StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid genreId)
        {
            var genreExists = _moviesService.GenreExists(genreId).FromResult();
            if (!genreExists.Success)
                return NotFound(genreExists);

            var result = _moviesService.DeleteGenre(genreId).FromResult();

            return Ok(result);
        }
    }
}
