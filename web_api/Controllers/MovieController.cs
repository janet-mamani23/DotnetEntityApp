using Microsoft.AspNetCore.Mvc;
using web_api.dto.movie;
using web_api.mock;
/*
namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MoviesMock _moviesMock = MoviesMock.Instance;

        // Obtener la lista de películas, con opción de filtrar por género
        [HttpGet(Name = "GetMovies")]
        public IActionResult GetMovies([FromQuery] MovieRequestDTO request)
        {
            List<MovieResponseDTO> movies;
            if (!string.IsNullOrEmpty(request.Genre))
            {
                movies = _moviesMock.GetMoviesByGenero(request.Genre);
            }
            else
            {
                movies = _moviesMock.GetMovies();
            }

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        // Obtener detalles de una película por ID
        [HttpGet("{id}")]
        public IActionResult GetMovieDetails(int id)
        {
            var movie = _moviesMock.GetMovies().FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
    }
}
*/

