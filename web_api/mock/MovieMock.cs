using entities_library.movie;
using web_api.dto.movie;
/*
namespace web_api.mock
{
    public class MoviesMock
    {
        private static MoviesMock? _instance;

        private MoviesMock()
        {
            AddMovies();
        }

        public static MoviesMock Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MoviesMock();
                }
                return _instance;
            }
        }

        private List<Movie> _movies = new List<Movie>();
    }};
        private void AddMovies()
        {
            _movies.Add(new Movie { Id = 1, Title = "Inception", Genre = "Action" });
            _movies.Add(new Movie { Id = 2, Title = "The Matrix", Genre = "Sci-Fi" });
            _movies.Add(new Movie { Id = 3, Title = "Titanic", Genre = "Drama" });
        }

        // Método para devolver las  películas
        public List<MovieResponseDTO> GetMovies()
        {
            return _movies.Select(m => new MovieResponseDTO
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre
            }).ToList();
        }

        // Método para devolver películas por género.
        public List<MovieResponseDTO> GetMoviesByGenero(string genero)
        {
            return _movies.Where(m => m.Genre == genero)
                          .Select(m => new MovieResponseDTO
                          {
                              Id = m.Id,
                              Title = m.title,
                              Genre = m.Genero
                          }).ToList();
        }
    }
}*/


