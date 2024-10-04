/*using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.qualify;
using web_api.mock;
using entities_library.login;

namespace web_api.Controllers;

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
