using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using web_api.dto;
using web_api.dto.genre;
using dao_library.Interfaces.movie;
using entities_library.movie;
using web_api.dto.login;
using web_api.dto.common;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GenreController : ControllerBase
{
    private readonly IDAOFactory daoFactory;
    public GenreController(IDAOFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }

    [HttpPost(Name = "Create Genre")]
    public async Task<IActionResult> Post([FromBody]RequestPostGenreDTO  RequestDTO )
    {
        IDAOGenre daoGenre = daoFactory.CreateDAOGenre();
        var genre = new Genre
        {
            Id = RequestDTO.Id,
            Name = RequestDTO.Name
        };
     
        var id = await daoGenre.Save(genre);
       
        return Ok(new ResponsePostGenreDTO
        {
            GenreName = RequestDTO.Name,
            Success = true,
            Message = "Registro exitoso"
        }
        );
    }

    [HttpGet(Name = "GetAllGenres")]
    public async Task<IActionResult> GetGenres()
    {
        IDAOGenre daoGenre = daoFactory.CreateDAOGenre();
        var genres = await daoGenre.GetAll();

        return Ok(genres);
    }

    [HttpDelete(Name = "DeleteGenre")]
    public async Task<IActionResult> Delete([FromBody]RequestDeleteDTO request)
    {
        IDAOGenre daoGenre = daoFactory.CreateDAOGenre();
        try{
            var success = await daoGenre.Delete(request.Id);
            if (success)
                {
                    return Ok(new ResponseDTO
                    {
                        Success = true,
                        Message = "Eliminación exitosa."
                    });
                }
            else
                {
                    return NotFound(new ResponseDTO
                    {
                        Success = false,
                        Message = "Género no encontrado"
                    });
                }
            }
        catch (Exception ex)
        {
            return StatusCode(500,  $"Internal server error: {ex.Message}");
        }
    }
}
