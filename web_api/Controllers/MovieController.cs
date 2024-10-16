using Microsoft.AspNetCore.Mvc;
using web_api.dto.movie;
using web_api.dto.comment;
using dao_library.Interfaces.movie;
using dao_library.Interfaces;
using entities_library.movie;

namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController: ControllerBase
{
    private readonly ILogger _moviegger;
    private readonly IDAOFactory daoFactory;
    public MovieController(
        ILogger<MovieController> moviegger,
        IDAOFactory daoFactory)
    {
        _moviegger = moviegger;
        this.daoFactory = daoFactory;
    }

    [HttpGet(Name = "movies")]  // obtengo todos los elementos
    public async Task<IActionResult> GetAllMovies(MoviesRequestDTO moviesRequestDTO)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();

        var movies = await daoMovie.GetAll();

        if (!string.IsNullOrEmpty(moviesRequestDTO.Genre) && moviesRequestDTO.Genre.ToLower() != "all")
        {
            movies = movies.Where(m => m.Genre.Name == moviesRequestDTO.Genre).ToList(); //filtro por el nombre del genero
        }
        return Ok (movies.Select(movi => new MoviesResponseDTO
        {
            Id = movi.Id,
            ImageUrl = movi.Image.Path //¿duda con el path?
        }).ToList()
        );
    
    }

    [HttpGet("oscar-movies")] 
    public async Task<IActionResult> GetOscarNominatedMovies(MoviesRequestDTO moviesRequestDTO)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();

        var movies = await daoMovie.GetAll();

        movies = movies.Where(m => m.HasOscar == true).ToList();
        if (!string.IsNullOrEmpty(moviesRequestDTO.Genre) && moviesRequestDTO.Genre.ToLower() != "all")
        {
            movies = movies.Where(m => m.Genre.Name.ToLower() == moviesRequestDTO.Genre.ToLower()).ToList();
        }

        var responseMovies = movies.Select(movi => new MoviesResponseDTO
        {
            Id = movi.Id,
            ImageUrl = movi.Image.Path // Asegúrate de que esta propiedad sea la correcta
        }).ToList();

        // Retornar la lista de películas nominadas al Oscar
        return Ok(responseMovies);
    }

    [HttpGet("topRating-movies")] // Endpoint para obtener películas con mejor calificación
    public async Task<IActionResult> GetTopRatedMovies(MoviesRequestDTO moviesRequestDTO)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();

        var movies = await daoMovie.GetAll(); // Obtener todas las películas

        if (!string.IsNullOrEmpty(moviesRequestDTO.Genre) && moviesRequestDTO.Genre.ToLower() != "all")
        {
            movies = movies.Where(m => m.Genre.Name == moviesRequestDTO.Genre).ToList(); // Filtrar por género
        }
        // Ordenar por calificación de mayor a menor
        var topRatedMovies = movies.OrderByDescending(m => m.Star).ToList(); // Suponiendo que 'Star' es la propiedad que guarda la calificación

        return Ok(topRatedMovies.Select(movi => new MoviesResponseDTO
        {
            Id = movi.Id,
            ImageUrl = movi.Image.Path
        }).ToList());
    }


    [HttpGet("{id}", Name = "MovieId")]  // Obtener los detalles de una película por su ID
    public async Task<IActionResult> GetMovieById(long id)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();

        Movie movie = await daoMovie.GetById(id);

        if (movie == null)// Verifico si la película existe
        {
            return NotFound("La película solicitada no fue encontrada.");
        }

        return Ok (new MovieResponseDTO
        {
            success = true,
            message = "La peliculas es: ",
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre.Name,
            Description = movie.Description, 
            ImageUrl = movie.Image?.Path,
            VideoUrl = movie.Video?.Path, //¿se incluye el genero y los comentarios y calificacion?
            Star = movie.Star?.Star,
            Comments = movie.Comments.Select(c => new CommentResponseDTO
            {
                Id = c.Id,
                UserName = c.User.Name,
                Text = c.Text,
                CreatedAt = c.CreatedAt
                
            }).ToList() // Comentarios asociados
        });
    }
}   
