using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using web_api.dto.common;
using web_api.dto.comment;
using dao_library.Interfaces.comment;
using entities_library.comment;
using dao_library.Interfaces.login;
using dao_library.Interfaces.movie;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CommentController : ControllerBase
{
    private readonly IDAOFactory daoFactory;
    public CommentController(IDAOFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }


    [HttpPost(Name = "CreateComment")]
    public async Task<IActionResult> Post([FromBody] RequestPostCommentDTO requestPostCommentDTO)
    {
        if(requestPostCommentDTO == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Datos ingresados erroneos"
            });
        }

        if(string.IsNullOrEmpty(requestPostCommentDTO.text))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Debe contener caracteres en el campo"
            });
        }

        IDAOComment daoComment = daoFactory.CreateDAOComment();
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();

        var user = await daoUser.GetById(requestPostCommentDTO.idUser);
        var movie = await daoMovie.GetById(requestPostCommentDTO.idMovie); 
        
        if (user == null || movie == null)
        {
            return NotFound(new ErrorResponseDTO
            {
                Success = false,
                Message = "Usuario o película no encontrados"
            });
        }

        Comment comment = new Comment{
            Text= requestPostCommentDTO.text,
            User = user,
            Movie = movie,
            CreatedAt = DateTime.Now,
        };
        
        await daoComment.Save(comment);
        return Ok(new ResponseDTO
        {
            Success = true,
            Message = "Comentario creado"
        });
    }


    [HttpDelete(Name = "DeleteComment")]
    public async Task<IActionResult> Delete(long id)
    {
        IDAOComment daoComment = daoFactory.CreateDAOComment();

        try
        {
            await daoComment.Delete(id);
            return Ok(new ResponseDTO
            {
                Success = true,
                Message = "Comentario eliminado."
            });
        }
        catch (Exception)
        {
            return NotFound(new ErrorResponseDTO
            {
                Success = false,
                Message = "Error al borrar el comentario."
            });
        }
    }


    [HttpPut(Name = "UpdateComment")]
    public async Task<IActionResult> Put([FromBody] RequestPutCommentDTO  requestPutCommentDTO )
    {
        IDAOComment daoComment = daoFactory.CreateDAOComment();

        try
        {
            await daoComment.Update(requestPutCommentDTO.idComment, requestPutCommentDTO.text);
            return Ok(new ResponseDTO
            {
                Success = true,
                Message = "Comentario actualizado."
            }); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message }); 
        }
    }
}
