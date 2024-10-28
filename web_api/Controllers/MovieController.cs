using Microsoft.AspNetCore.Mvc;
using web_api.dto.movie;
using web_api.dto.comment;
using dao_library.Interfaces.movie;
using dao_library.Interfaces;
using entities_library.movie;
using web_api.dto.common;

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

    [HttpGet(Name = "movies")] 
    public async Task<IActionResult> GetAllMovies(MoviesRequestDTO moviesRequestDTO, [FromQuery] MovieGetAllRequestDTO request)
    {
        IDAOMovie daoMovie = this.daoFactory.CreateDAOMovie();

        var (movies, totalRecords) = await daoMovie.GetAll(
            request.query,
            request.page,
            request.pageSize);

        if (!string.IsNullOrEmpty(moviesRequestDTO.Genre) && moviesRequestDTO.Genre.ToLower() != "all") //aca no especifico el genero
        {
            movies = movies.Where(m => m.Genre.Name == moviesRequestDTO.Genre).ToList(); // caso contrario se filtra por genero
        }

        var moviesResponse = movies.Select(movi => new MoviesResponseDTO
        {
            Id = movi.Id,
            ImageUrl = movi.Image.Path
        }).ToList();

        return Ok(new  // Retornar las películas paginadas mas la cantidad total de registros
        {
            Movies = moviesResponse,  
            TotalRecords = totalRecords
        });
    }

    [HttpGet("oscar-movies")] 
    public async Task<IActionResult> GetOscarNominatedMovies(MoviesRequestDTO moviesRequestDTO, [FromQuery] MovieGetAllRequestDTO request)
    {
        IDAOMovie daoMovie = this.daoFactory.CreateDAOMovie();

        var (movies, totalRecords) = await daoMovie.GetAll(
            request.query,
            request.page,
            request.pageSize
        );

        movies = movies.Where(m => m.HasOscar == true).ToList();
        if (!string.IsNullOrEmpty(moviesRequestDTO.Genre) && moviesRequestDTO.Genre.ToLower() != "all")
        {
            movies = movies.Where(m => m.Genre.Name.ToLower() == moviesRequestDTO.Genre.ToLower()).ToList();
        }

        var responseMovies = movies.Select(movi => new MoviesResponseDTO
        {
            Id = movi.Id,
            ImageUrl = movi.Image.Path
        }).ToList();
        
        return Ok( new
        {
            Movies = responseMovies,
            TotalRecords = totalRecords // Devuelvo también la cantidad total de registros
        });
    }

    [HttpGet("topRating-movies")] // Endpoint
    public async Task<IActionResult> GetTopRatedMovies(MoviesRequestDTO moviesRequestDTO, [FromQuery] MovieGetAllRequestDTO request)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();

        var (movies, totalRecords) = await daoMovie.GetAll(
            request.query,
            request.page,
            request.pageSize

        ); // Obtener todas las películas

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

         // Obtener promedio de calificaciones usando el método GetAverage
        double averageRating = movie.GetAverage();

        return Ok (new MovieResponseDTO
        {
            Success = true,
            Message = "La peliculas es: ",
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre.Name,
            Description = movie.Description, 
            ImageUrl = movie.Image?.Path,
            VideoUrl = movie.Video?.Path, //¿se incluye el genero y los comentarios y calificacion?
            //Star = movie.Star?.Star,
            AverageQualify = averageRating, // Promedio de calificaciones

            //TODO-ENZO aplicar paginacioncomentarios(lo que paso el profe) dao.comment(get all)
            Comments = movie.Comments.Select(c => new CommentResponseDTO
            {
                Id = c.Id,
                UserName = c.User.Name,
                Text = c.Text,
                CreatedAt = c.CreatedAt
                
            }).ToList() // Comentarios asociados
        });
    }

    [HttpPost(Name = "CreateMovie")]
        public async Task<IActionResult> Post(MovieRequestDTO movieRequestDTO)
        {
            IDAOMovie daoMovie = daoFactory.CreateDAOMovie(); //usamos la interfaz para crear un movie/post
            Genre genre = await daoFactory.CreateDAOGenre().GetById(movieRequestDTO.GenreId); //obtengo el objeto Genre

            Movie? movie = await daoMovie.Create(movieRequestDTO.TitleMovie, genre); //aseguramos que la peli esta asociado a un gnre

            if (movie != null && movie.Title == movieRequestDTO.TitleMovie)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Success = false,
                    Message = "There is already a post with that title." //ya hay un post con ese titulo
                });
            }

            if(movieRequestDTO == null)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Success = false, 
                    Message = "The fields must be completed." //Se deben completar los campos.
                });
            }

            if(string.IsNullOrEmpty(movieRequestDTO.TitleMovie))
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Success = false,
                    Message = "The title is a required field." //El titulo es un campo obligatorio.
                });
            }

            // corroboramos si existe la pelicula;
            Movie? existingMovie = await daoMovie.GetByTitle(movieRequestDTO.TitleMovie);

            if(existingMovie != null)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Success=false,
                    Message = "The title of already exists." //El titulo ya existe.
                });
            }

            Genre genreUno = await daoFactory.CreateDAOGenre().GetById(movieRequestDTO.GenreId);

            //creo una nueva entidad
            Movie newMovie = new Movie
            {
                Title = movieRequestDTO.TitleMovie,
                Description = movieRequestDTO.DescriptionMovie,
                Genre = genreUno,
                Image = movieRequestDTO.Image,
                Video = movieRequestDTO.VideoMovie,
                
            };

            await daoMovie.Save(newMovie);

            return Ok(new MovieResponseDTO
            {
                Success = true,
                Message = "El post fue creado exitosamente.",
                Id = newMovie.Id,
                Title = newMovie.Title,
                Description = newMovie.Description,
                Genre = newMovie.Genre.Name,
                ImageUrl = newMovie != null && newMovie != null ? newMovie.Image.Path : "",
                VideoUrl= newMovie.Video.Path,
            });   
        }
}   
