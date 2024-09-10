using web_api.dto.login;


public static class Rating
{
    public static void RatingMovie(UserPostRequestDTO user, MovieDTO movie, int rating)
    {
        var existingRating = user.Ratings.FirstOrDefault(r => r.MovieId == movie.Id);
        if (existingRating != null)
        {
            existingRating.Value = rating;
        }
        else
        {
            user.Ratings.Add(new Rating { MovieId = movie.Id, Value = rating });
        }
    }
}