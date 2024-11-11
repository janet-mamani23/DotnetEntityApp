using dao_library.Interfaces.qualify;
using web_api.dto.qualify;
using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.login;
using web_api.dto.login;
using entities_library.Qualify;
using dao_library.Interfaces.movie;

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
            IDAOQualify DAOQualify = daoFactory.CreateDAOQualify();

            // 1. Verificar si el usuario está logueado
            IDAOUser daoUser = daoFactory.CreateDAOUser();
            User user = await daoUser.GetById(qualifyRequest.userId);

            if (user == null)
            {
                return Unauthorized("User not logged in.");
            }

            // Obtener la película a calificar desde el DAO
            IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
            var movie = await daoMovie.GetById(qualifyRequest.movieId);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            // 2. Verificar si el usuario ya ha calificado esta película
            bool hasQualified = 
                await this.daoFactory.CreateDAOQualify()
                .HasUserQualifiedMovie(user, movie);

            if (hasQualified)
            {
                return BadRequest("The user has already rated this movie.");
            }

            // 3. Crear la nueva calificación
            var qualify = new Qualify
            {
                User = user,
                Stars = qualifyRequest.star, // Calificación
                Movie = movie
            };

            // 4. Guardar la calificación en la base de datos
            await this.daoFactory.CreateDAOQualify().Save(qualify);

            // 5. Obtener el nuevo promedio de calificaciones
            var averageStars = qualify.Movie.GetAverage(); // Llama al método de la entidad Movie para obtener el promedio
            
            return Ok(new QualifyResponseDTO 
            {
                Success = true,
                Message = "Rating created successfully",
                id = qualify.Id,
                star = qualify.Stars,
                averageStars = (int)averageStars
            });
        }
    }
}