using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.qualify;
using web_api.mock;
using entities_library.login;
using dao_library.Interfaces;
using dao_library.Interfaces.qualify;

namespace web_api.Controllers;
/*
[ApiController] 
[Route("[controller]")]

public class QualifyController : ControllerBase
{

    [HttpPost]
        public ActionResult QualifyMovie(int userId, int movieId, int star)
        {

            var user = _context.Users.Include("Ratings").FirstOrDefault(u => u.Id == userId);
            var movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);

            if (user == null || movie == null)
            {
                return HttpNotFound();
            }

            RatingHelper.AddOrUpdateRating(user, movie, rating);
            _context.SaveChanges();

            return new HttpStatusCodeResult(200, "Rating added/updated successfully");
        }
}*/

[ApiController]
[Route("[controller]")]
public class QualifyController : ControllerBase
{
    private readonly ILogger<QualifyController> _logger;
    private readonly IDAOFactory daoFactory;

    public QualifyController(
        ILogger<LoginController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory;
    }

    [HttpPost(Name = "Qualify")]
    public async Task<IActionResult> Post(QualifyRequestDTO qualifyRequestDTO)
    {
        IDAOQualify daoQualify = daoFactory.CreateDAOQualify();
        
        User user = await daoQualify.Get(
            qualifyRequestDTO.Name,
            qualifyRequestDTO.Star
        );

        if( qualifyRequestDTO != null &&
            qualifyRequestDTO.IsPassword(loginRequestDTO.password))
        {
            return Ok(new QualifyResponseDTO
            {
                success = true,
                message = "",
                id = qualify.Id,
                Start = qualify.Star
            });
        }
        
        return Unauthorized(new ErrorResponseDTO
        {
            success = false,
            message = "Invalid mail or password"
        });
    }
}
