using Microsoft.AspNetCore.Mvc;
using web_api.dto;
using web_api.dto.movie;
using web_api.dto.comment;
using dao_library.Interfaces.movie;
using dao_library.Interfaces;
using entities_library.movie;
using web_api.dto.common;
using entities_library.file_system;
using entities_library.comment;
using dao_library.Interfaces.file_system;


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

    [HttpGet]
    [Route("MovieGener")] 
    public async Task<IActionResult> GetMoviesGener( [FromQuery] GetAllRequestDTO requestMovie)
    {
        IDAOMovie daoMovie = this.daoFactory.CreateDAOMovie();

       /* if (requestMovie.IsTopRated)
        {
            movies = movies.OrderByDescending(m => m.Qualifies).ToList(); // Ordena de mayor a menor calificación
        }*/

        try
        {
            
            var (movies, totalRecords) = await daoMovie.GetAll(
                requestMovie.query,
                requestMovie.page,
                requestMovie.pageSize);

            var moviesResponse = movies.Select(movi => new MoviesResponseDTO
            {
                Id = movi.Id,
                ImageUrl = movi.GetImage(),
                Name = movi.Title,
            }).ToList();
            var response = new MovieGetAllResponseDTO
                {
                    Movies = moviesResponse,
                    TotalRecords = totalRecords,
                    Page = requestMovie.page,
                    PageSize = requestMovie.pageSize,
                    Success = true,
                    Message = "Lista peliculas entregada."
                };
            return Ok(response);
        }
        catch  (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponseDTO
            {
                Success = false,
                Message = ex.Message 
            });
            }
    }

    /*[HttpGet]
    [Route("all")] 
    public async Task<IActionResult> GetAllMovies( [FromQuery] GetAllRequestDTO requestMovie)
    {
        IDAOMovie daoMovie = this.daoFactory.CreateDAOMovie();
        try
        {
            var (movies, totalRecords) = await daoMovie.GetAllMovies(
                requestMovie.page,
                requestMovie.pageSize);

            var moviesResponse = movies.Select(movi => new MoviesResponseDTO
            {
                Id = movi.Id,
                ImageUrl = movi.Image.Path,
                Name = movi.Title,
            }).ToList();
            var response = new MovieGetAllResponseDTO
                {
                    Movies = moviesResponse,
                    TotalRecords = totalRecords,
                    Page = requestMovie.page,
                    PageSize = requestMovie.pageSize,
                    Success = true,
                    Message = "Lista peliculas entregada."
                };
            return Ok(response);
        }
        catch  (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponseDTO
            {
                Success = false,
                Message = ex.Message 
            });
            }
    }*/

    [HttpPost(Name = "CreateMovie")]
    public async Task<IActionResult> Post(MovieRequestDTO movieRequestDTO)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie(); 
        IDAOGenre daoGenre = daoFactory.CreateDAOGenre();
        IDAOFileEntity daoFileEntity = daoFactory.CreateDAOFileEntity();

        if(movieRequestDTO == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false, 
                Message = "Se deben completar los campos." 
            });
        }
        if (string.IsNullOrEmpty(movieRequestDTO.TitleMovie) ||
        string.IsNullOrEmpty(movieRequestDTO.DescriptionMovie) ||
        string.IsNullOrEmpty(movieRequestDTO.ImageUrl) ||
        string.IsNullOrEmpty(movieRequestDTO.VideoUrl))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Se deben completar todos los campos." // Se deben completar los campos.
            });
        }

        IDAOFileType fileType = daoFactory.CreateDAOFileType();

        var imageType = await fileType.GetById(1);
        var videoType = await fileType.GetById(2);

        var image = new FileEntity {
            Path = movieRequestDTO.ImageUrl,
            FileType = imageType,
        };

        var video = new FileEntity {
            Path = movieRequestDTO.VideoUrl,
            FileType = videoType,
        };
        
        Genre? genre = await daoGenre.GetById(movieRequestDTO.GenreId) ?? throw new KeyNotFoundException($"Pelicula no encontrada.");

        var movie = new Movie
            {
                Title = movieRequestDTO.TitleMovie,
                Description = movieRequestDTO.DescriptionMovie,
                Genre = genre,
                Image = image,
                Video = video,
                Comments = {},
                Qualifies = {},
                HasOscar = movieRequestDTO.HasOscar
            };

        var existMovie = await daoMovie.ExistMovie(movieRequestDTO.TitleMovie);
        if (existMovie != null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Ya existe una pelicula con ese título."
            });
        }

        var result = await daoMovie.Save(movie); 
        if (result != null)
        {
            return Ok(new MovieResponseDTO
            {
                Success = true,
                Message = "Película creada con éxito.",
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre.Name,
                ImageUrl = movie.GetImage(),
                VideoUrl = movie.GetVideo(),
            });
        }
        else
        {
            return StatusCode(500, new ErrorResponseDTO
            {
                Success = false,
                Message = "Ocurrió un error al guardar el post." 
            });
        }
    }

    [HttpDelete(Name = "DeleteMovie")]
    public async Task<IActionResult> Delete(RequestDeleteDTO request)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
        try
        {
            var movie = await daoMovie.GetById(request.Id);
            if(movie == null)
                {
                 return NotFound(new ResponseDTO
                 {
                    Success = false,
                    Message = "Película no encontrada"
                 });
                }
            
            var success = await daoMovie.Delete(request.Id);
            if (success)
                {
                    return Ok(new MovieDeleteResponseDTO
                        {
                            Id = request.Id,
                            ImageUrl = movie.Image.Path,
                            Name = movie.Title,
                            Success = true,
                            Message = "Pelicula eliminada."
                        });
                }
            else
                {
                return StatusCode(500, new ErrorResponseDTO
                    {
                        Success = false,
                        Message = "No se pudo eliminar la pelicula." 
                    });
                }
        }
        
        catch (Exception ex)
        {
            return StatusCode(500,  $"Internal server error: {ex.Message}");
        }
    }
 
    [HttpGet]
    [Route("byName")]   
    public async Task<IActionResult> SearchMovie([FromQuery]MovieSearchDTO search)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
        if (string.IsNullOrEmpty(search.Title))
        {
            return BadRequest(new { Message = "El nombre de la película no puede estar vacío" });
        }

        var movie = await daoMovie.ExistMovie(search.Title);

        if(movie == null)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Success = false,
                    Message = "La película no se encontró." 
                });
            }
 

        double averageRating = movie.GetAverage(); 

        List<CommentResponseDTO> commentsResponse = new List<CommentResponseDTO>();
        
        foreach (Comment comment in movie.Comments)
        {
            commentsResponse.Add(new CommentResponseDTO
            {
                Id = comment.Id,
                AvatarUser = comment.UrlAvatar(),
                UserName = comment.GetName(),
                Text = comment.Text,
                CreatedAt = comment.CreatedAt
            });
        }

        return Ok (new MovieResponseDTO
        {
            Success = true,
            Message = "Película encontrada.",
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre.Name,
            Description = movie.Description, 
            ImageUrl = movie.GetImage(),
            VideoUrl = movie.GetVideo(),
            //Star = movie.Star?.Star, Como se representa con respecto a el front las estrellas.
            AverageQualify = averageRating,
            Comments = commentsResponse,
             });
    }

    
    /*[HttpGet("{id}", Name = "MovieId")]  // Obtener los detalles de una película por su ID
    public async Task<IActionResult> GetMovieById(long id)
    {
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
        Movie movie = await daoMovie.GetById(id);

        if (movie == null)// Verifico si la película existe
        {
            return NotFound("The requested movie was not found.");
        }

        double averageRating = movie.GetAverage();   // Obtener promedio de calificaciones usando el método GetAverage

        List<CommentResponseDTO> commentsResponse = new List<CommentResponseDTO>();
        
        foreach (Comment comment in movie.Comments)
        {
            commentsResponse.Add(new CommentResponseDTO
            {
                Id = comment.Id,
                AvatarUser = comment.UrlAvatar(),
                UserName = comment.GetName(),
                Text = comment.Text,
                CreatedAt = comment.CreatedAt
            });
        }

        return Ok (new MovieResponseDTO
        {
            Success = true,
            Message = "The movies is: ",
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre.Name,
            Description = movie.Description, 
            ImageUrl = movie.Image?.Path,
            VideoUrl = movie.Video?.Path,
            //Star = movie.Star?.Star, Como se representa con respecto a el front las estrellas.
            AverageQualify = averageRating,
            Comments = commentsResponse,
             });
    }*/

}   
