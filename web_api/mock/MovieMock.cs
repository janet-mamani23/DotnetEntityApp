using entities_library.comment;
using entities_library.movie;

namespace web_api.mock;

public class MoviesMock
{
    private  static MoviesMock? _instance;

    //private MoviesMock() { }
    private MoviesMock()
    {
        AddMovie();
    }

    public static MoviesMock Instance
    {
        get
        {
            if(_instance == null) _instance = new MoviesMock();
            return _instance;
        }
    }
    private List<Movie> _movies = new List<Movie>();

    public long CreateMovieMock(string genre,string name,string actor, string director, bool oscar, long star,List<Comment> comments)
    {
        Movie movie = new Movie
        {
         Genre = genre,
         Name = name,
         Actor = actor,
         Director = director,
         Oscar = oscar,
         Star = star,
         Comments = comments
        };

        movie.Id = this._movies.Count + 1;
        this._movies.Add(movie);

        return movie.Id;
    }

    // Agregar películas inicial

    private void AddMovie()
    {
        CreateMovieMock("Action", "Inception", "Leonardo DiCaprio", "Christopher Nolan", true, 5, new List<Comment>());
        CreateMovieMock("Sci-Fi", "The Matrix", "Keanu Reeves", "The Wachowskis", true, 5, new List<Comment>());
    }

    public Movie GetMovieById(int id)
    {
        return _movies.FirstOrDefault(m => m.Id == id);
    }

    public void AddCommentToMovie(long movieId, Comment comment)
    {
        // Buscar la película por su Id
        var movie = _movies.FirstOrDefault(m => m.Id == movieId);
        if (movie != null)
        {
            Comment newComment = new Comment
            {
                Text = comment.Text,
                User = comment.User,
                Movie = comment.Movie,
                CreatedAt = comment.CreatedAt
            };
            // Agregar el nuevo comentario a la lista de comentarios
            movie.Comments.Add(newComment);
        }
    }

    public List<Movie> GetMovies()
    {
        return _movies;
    }
}

