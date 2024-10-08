/*using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using web_api.dto.common;
using web_api.dto.comment;
using entities_library.login;
using dao_library.Interfaces.comment;
using entities_library.comment;
using dao_library.Interfaces.login;
using dao_library.Interfaces.movie;
using entities_library.movie;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CreateCommentController : ControllerBase
{
    private readonly IDAOFactory daoFactory;
    public CreateCommentController(IDAOFactory daoFactory)
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
                success = false,
                message = "Datos ingresados erroneos"
            });
        }

        if(string.IsNullOrEmpty(requestPostCommentDTO.text))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "Debe contener caracteres en el campo"
            });
        }

    
        IDAOComment daoComment = daoFactory.CreateDAOComment();
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        IDAOMovie daoMovie = daoFactory.CreateDAOMovie();

        User user = await daoUser.GetById(requestPostCommentDTO.idUser);
        Movie Movie = await daoMovie.GetById(requestPostCommentDTO.idMovie); 

        Comment comment = new Comment(
            requestPostCommentDTO.text,
            user,
            Movie,
            DateTime.Now
        );

        await daoComment.Save(comment);
        return CreatedAtAction(nameof(Post), new { id = comment.Id }, comment);
    }
}*/