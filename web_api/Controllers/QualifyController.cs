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

            if (user == null || user.UserStatus == entities_library.login.UserStatus.Banned)
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

            bool hasQualified = 
                await daoFactory.CreateDAOQualify()
                .HasUserQualifiedMovie(user, movie);

            if (hasQualified)
            {
                return BadRequest("Este usuario ya ha calificado la pelicula.");
            }

            var qualify = new Qualify
            {
                User = user,
                Stars = qualifyRequest.Star,
                Movie = movie
            };

            await daoQualify.Save(qualify);
            await daoMovie.UpdateQualify(movie.Id, qualify);
            var averageStars = movie.GetAverage(); 
            
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