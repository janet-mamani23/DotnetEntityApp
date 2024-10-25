using dao_library.Interfaces.qualify;
using web_api.dto.qualify;
using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.login;
using web_api.dto.login;
//using dao_library.entity_framework.ef_qualify;
using entities_library.Qualify;

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
        public async Task<IActionResult> CreateQualify([FromBody] QualifyRequestDTO qualifyRequest, LoginRequestDTO loginRequestDTO, IDAOQualify dAOQualify)
        {
            IDAOQualify DAOQualify = daoFactory.CreateDAOQualify();

            // 1. Verificar si el usuario está logueado
            IDAOUser daoUser = daoFactory.CreateDAOUser();
            User user = await daoUser.Get(loginRequestDTO.email, loginRequestDTO.password);

            if (user == null || !user.IsPassword(loginRequestDTO.password))
            {
                return Unauthorized("User not logged in.");
            }

            // 2. Verificar si el usuario ya ha calificado esta película
            bool hasQualified = await dAOQualify.HasUserQualifiedMovie(loginRequestDTO.email, qualifyRequest.Movie.Id);
            if (hasQualified)
            {
                return BadRequest("The user has already rated this movie.");
            }

            // 3. Crear la nueva calificación
            var qualify = new Qualify
            {
                User = user,
                Movie = qualifyRequest.Movie, // Película a calificar
                Star = qualifyRequest.Star // Calificación
            };

            // 4. Guardar la calificación en la base de datos
            await dAOQualify.Save(qualify);

            // 5. Obtener el nuevo promedio de calificaciones
            var averageStars = qualify.Movie.GetAverage(); // Llama al método de la entidad Movie para obtener el promedio
            
            return Ok(new QualifyResponseDTO 
            {
                Success = true,
                Message = "",
                Id = qualify.Id,
                Star = qualify.Star,
                AverageStars = (int)averageStars
            });
        }


        [HttpPut(Name = "UpdateQualify")]
        public async Task<IActionResult> UpdateQualify([FromBody] QualifyRequestDTO qualifyRequest, LoginRequestDTO loginRequestDTO)
        {
            // 1. Verificar si el usuario está logueado
            IDAOUser daoUser = daoFactory.CreateDAOUser();
            User user = await daoUser.Get(loginRequestDTO.email, loginRequestDTO.password);

            if (user == null || !user.IsPassword(loginRequestDTO.password))
            {
                return Unauthorized("User not logged in.");
            }

            // 2. Buscar la calificación existente
            IDAOQualify daoQualify = daoFactory.CreateDAOQualify();
            var qualifyToUpdate = new Qualify
            {
                User = user,
                Movie = qualifyRequest.Movie,
                Star = qualifyRequest.Star
            };

            // 3. Actualizar la calificación
            bool updated = await daoQualify.Update(qualifyToUpdate);

            if (!updated)
            {
                return NotFound("Rating not found.");
            }
            
            return Ok("Rating successfully updated.");
        }
    }
}
