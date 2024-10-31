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
        public async Task<IActionResult> CreateQualify([FromBody] QualifyRequestDTO qualifyRequest, [FromQuery]LoginRequestDTO loginRequestDTO,  [FromServices] IDAOQualify dAOQualify)
        {
            IDAOQualify DAOQualify = daoFactory.CreateDAOQualify();

            // 1. Verificar si el usuario está logueado
            IDAOUser daoUser = daoFactory.CreateDAOUser();
            User user = await daoUser.Get(loginRequestDTO.EmailUser, loginRequestDTO.PasswordUser);

            if (user == null || !user.IsPassword(loginRequestDTO.PasswordUser))
            {
                return Unauthorized("User not logged in.");
            }

            // Obtener la película a calificar desde el DAO
            IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
            var movie = await daoMovie.GetById(qualifyRequest.MovieId);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            // 2. Verificar si el usuario ya ha calificado esta película
            bool hasQualified = await dAOQualify.HasUserQualifiedMovie(loginRequestDTO.EmailUser, qualifyRequest.Movie.Id);
            if (hasQualified)
            {
                return BadRequest("The user has already rated this movie.");
            }

            // 3. Crear la nueva calificación
            var qualify = new Qualify
            {
                User = user,
                Stars = qualifyRequest.Star, // Calificación
                MovieId = qualifyRequest.MovieId, // Película a calificar
                Movie = movie
            };

            // 4. Guardar la calificación en la base de datos
            await dAOQualify.Save(qualify);

            // 5. Obtener el nuevo promedio de calificaciones
            var averageStars = qualify.Movie.GetAverage(); // Llama al método de la entidad Movie para obtener el promedio
            
            return Ok(new QualifyResponseDTO 
            {
                Success = true,
                Message = "Rating created successfully",
                Id = qualify.Id,
                Star = qualify.Stars,
                AverageStars = (int)averageStars
            });
        }

        [HttpPut(Name = "UpdateQualify")]
        public async Task<IActionResult> UpdateQualify([FromBody] QualifyRequestDTO qualifyRequest, [FromQuery] LoginRequestDTO loginRequestDTO)
        {
            // 1. Verificar si el usuario está logueado
            IDAOUser daoUser = daoFactory.CreateDAOUser();
            User user = await daoUser.Get(loginRequestDTO.EmailUser, loginRequestDTO.PasswordUser);

            if (user == null || !user.IsPassword(loginRequestDTO.PasswordUser))
            {
                return Unauthorized("User not logged in.");
            }

            // 2. Buscar la calificación existente
            IDAOQualify daoQualify = daoFactory.CreateDAOQualify();
            var qualifyToUpdate = await daoQualify.GetQualifyByUserAndMovie(user.Id, qualifyRequest.MovieId);

            if (qualifyToUpdate == null)
            {
                return NotFound("Rating not found.");
            }

             // Actualizar la calificación
            qualifyToUpdate.Stars = qualifyRequest.Star;
            bool updated = await daoQualify.Update(qualifyToUpdate);

            if (!updated)
            {
                return StatusCode(500, "Failed to update rating.");
            }

            //return Ok("Rating successfully updated.");

            // 4. Obtener el promedio actualizado
            IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
            var movie = await daoMovie.GetById(qualifyRequest.MovieId);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            var newAverageStars = movie.GetAverage(); // Llama al método de la entidad Movie para obtener el promedio

            return Ok(new QualifyResponseDTO
            {
                Success = true,
                Message = "Rating successfully updated.",
                Id = qualifyToUpdate.Id,
                Star = qualifyToUpdate.Stars,
                AverageStars = (int)newAverageStars
            });
        }
    }
}