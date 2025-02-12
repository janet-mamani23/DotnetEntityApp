using Microsoft.AspNetCore.Mvc;
using web_api.dto.movie;
using web_api.dto.comment;
using dao_library.Interfaces.movie;
using dao_library.Interfaces;
using entities_library.movie;
using web_api.dto.common;
using entities_library.file_system;
using dao_library.Interfaces.comment;
using web_api.Controllers.comment;
using entities_library.comment;

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

    [HttpGet(Name = "movies")]  //lo unico que cambia son los parametros, es una pelicula para todos.
    public async Task<IActionResult> GetAllMovies( [FromQuery] GetAllMoviesRequestDTO requestMovie)
    {
        IDAOMovie daoMovie = this.daoFactory.CreateDAOMovie();

        var (movies, totalRecords) = await daoMovie.GetAll(
            requestMovie.query,
            requestMovie.page,
            requestMovie.pageSize);

        if (!string.IsNullOrEmpty(requestMovie.Genre) && requestMovie.Genre.ToLower() != "all") //aca no especifico el genero
        {
            movies = movies.Where(m => m.Genre.Name == requestMovie.Genre).ToList(); // caso contrario se filtra por genero
        }

        if (requestMovie.HasOscar)
        {
            movies = movies.Where(m => m.HasOscar).ToList(); // Filtra solo las que tienen Oscar
        }

        if (requestMovie.IsTopRated)
        {
            movies = movies.OrderByDescending(m => m.Qualifies).ToList(); // Ordena de mayor a menor calificación
        }

        var moviesResponse = movies.Select(movi => new MoviesResponseDTO
        {
            Id = movi.Id,
            ImageUrl = movi.Image.Path
        }).ToList();

        return Ok(new  // Retornar las películas paginadas mas la cantidad total de registros
        {
            Success = true,
            Message = "Movies retrieved successfully",
            Movies = moviesResponse,  
            TotalRecords = totalRecords
        });
    }


    [HttpGet("{id}", Name = "MovieId")]  // Obtener los detalles de una película por su ID
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
            //Star = movie.Star?.Star,
            AverageQualify = averageRating,
            Comments = commentsResponse,
             });
    }

    [HttpPost(Name = "CreateMovie")]
        public async Task<IActionResult> Post(MovieRequestDTO movieRequestDTO)
        {
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

            IDAOMovie daoMovie = daoFactory.CreateDAOMovie(); //usamos la interfaz para crear un movie/post
            Movie? existingMovie = await daoMovie.GetByTitle(movieRequestDTO.TitleMovie);  // corroboramos si existe la pelicula;

            if(existingMovie != null)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Success=false,
                    Message = "The title of already exists." //El titulo ya existe.
                });
            }

            Genre? genre = await daoFactory.CreateDAOGenre().GetById(movieRequestDTO.GenreId); //obtengo el objeto Genre
            
            if (genre == null)
            {
             return BadRequest(new ErrorResponseDTO
             {
                Success = false,
                Message = "Invalid genre selected." // Género inválido.
                });
            }
            
            FileType imageType = await daoFactory.CreateDAOFileType().GetById(1);
            FileType videoType = await daoFactory.CreateDAOFileType().GetById(2);
            
            FileEntity image = new FileEntity {
                Path = movieRequestDTO.ImageUrl,
                FileType = imageType,
                Id = 0
            };

            await daoFactory.CreateDAOFileEntity().Save(image);

            FileEntity video = new FileEntity {
                Path = movieRequestDTO.VideoUrl,
                FileType = videoType,
                Id = 0
            };
            await daoFactory.CreateDAOFileEntity().Save(video);

            Movie newMovie = new Movie   //creo una nueva entidad
            {
                Title = movieRequestDTO.TitleMovie,
                Description = movieRequestDTO.DescriptionMovie,
                Genre = genre,
                Image = image,
                Video = video,
                
            };
            await daoMovie.Create(newMovie);
            
            return Ok(new MovieResponseDTO
            {
                Success = true,
                Message = "The post was created successfully.",
                Id = newMovie.Id,
                Title = newMovie.Title,
                Description = newMovie.Description,
                Genre = newMovie.Genre.Name,
                ImageUrl = newMovie.GetImage(),
                VideoUrl= newMovie.GetVideo(),
            });   
        }

        [HttpDelete(Name = "DeleteMovie")]
        public async Task<IActionResult> Delete(long id)
        {
            IDAOMovie daoMovie = daoFactory.CreateDAOMovie();
            
            // Buscamos si la película existe
            Movie? movie = await daoMovie.GetById(id);
            
            if (movie == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Success = false,
                    Message = "The movie was not found." // La película no fue encontrada.
                });
            }

            await daoMovie.Delete(id);  // Eliminamos la películac

            return Ok(new MovieResponseDTO
            {
                Success = true,
                Message = "The movie was deleted successfully." // La película se eliminó con éxito.
            });
        }
}   
