using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;
using WebSocket.SignalR.Models.DTOs;

namespace WebSocket.SignalR.Controllers
{
    //[Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;

        public GenresController(ILogger<GenresController> logger, IMoviesService moviesService, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(moviesService, nameof(moviesService));
            ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

            _logger = logger;
            _moviesService = moviesService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            { 
                var genres = _moviesService.GetGenres();

                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("pagination")]
        public IActionResult GetPaginated([FromQuery] PaginationInput pagination)
        {
            try
            {
                var genres = _moviesService.GetGenres(pagination, HttpContext?.Request?.Path);

                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateGenreDTO request)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var genre = _mapper.Map<Genre>(request);
                var genres = _moviesService.AddGenre(genre);

                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
