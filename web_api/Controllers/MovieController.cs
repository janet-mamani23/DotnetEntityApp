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

            FileType imageType = await this.daoFactory.CreateDAOFileType().GetById(1);
            FileType videoType = await this.daoFactory.CreateDAOFileType().GetById(2);
            
            FileEntity image = new FileEntity {
                Path = movieRequestDTO.ImageUrl,
                FileType = imageType,
                Id = 0
            };

            await this.daoFactory.CreateDAOFileEntity().Save(image);

            FileEntity video = new FileEntity {
                Path = movieRequestDTO.VideoUrl,
                FileType = videoType,
                Id = 0
            };

            await this.daoFactory.CreateDAOFileEntity().Save(video);

            //creo una nueva entidad
            Movie newMovie = new Movie
            {
                Title = movieRequestDTO.TitleMovie,
                Description = movieRequestDTO.DescriptionMovie,
                Genre = genre,
                Image = image,
                Video = video,
                
            };

            await daoMovie.Save(newMovie);

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
}   
