using dao_library.Interfaces.qualify;
using web_api.dto.qualify;
using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.Qualify;
using dao_library.Interfaces.movie;
using web_api.dto.common;
namespace web_api.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QualifyController : ControllerBase
    {
        private readonly ILogger<QualifyController> _logger;
        private readonly IDAOFactory daoFactory;
        public QualifyController(
            ILogger<QualifyController> logger,
            IDAOFactory daoFactory)
        {
            _logger = logger;
            this.daoFactory = daoFactory;
        }

        [HttpPost(Name = "CreateQualify")]
        public async Task<IActionResult> CreateQualify([FromBody] QualifyRequestDTO qualifyRequest)
        {
            IDAOQualify daoQualify = daoFactory.CreateDAOQualify();

            IDAOUser daoUser = daoFactory.CreateDAOUser();
            var user = await daoUser.GetById(qualifyRequest.UserId);

            if (user == null)
            {
                return Unauthorized("Se necesita estar logueado para calificar la pelicula.");
            }

            IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
            var movie = await daoMovie.GetById(qualifyRequest.MovieId);
            if (movie == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Success = false,
                    Message = "Error la pelicula no existe."
                });
            }

            // 2. Verificar si el usuario ya ha calificado esta película
            bool hasQualified = 
                await daoFactory.CreateDAOQualify()
                .HasUserQualifiedMovie(user, movie);

            if (hasQualified)
            {
                return BadRequest("Este usuario ya ha calificado la pelicula.");
            }

            // 3. Crear la nueva calificación
            var qualify = new Qualify
            {
                User = user,
                Stars = qualifyRequest.Star, // Calificación
                Movie = movie
            };

            // 4. Guardar la calificación en la base de datos
            await daoQualify.Save(qualify);

            // 5. Obtener el nuevo promedio de calificaciones
            var averageStars = qualify.Movie.GetAverage(); // Llama al método de la entidad Movie para obtener el promedio
            
            return Ok(new QualifyResponseDTO 
            {
                Success = true,
                Message = "Calificación guardada.",
                Id = qualify.Id,
                Star = qualify.Stars,
                AverageStars = (double)averageStars
            });
        }
    }
}