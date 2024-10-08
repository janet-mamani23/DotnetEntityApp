/*using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces.comment;
using dao_library.Interfaces;
using web_api.dto.comment;

[ApiController]
[Route("api/[controller]")]


public class UpdateCommentController : ControllerBase
{
    private readonly IDAOFactory daoFactory;

    public UpdateCommentController(IDAOFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }

    [HttpPut(Name = "UpdateComment")]
    public async Task<IActionResult> Put(long id, [FromBody] RequestPutCommentDTO  requestPutCommentDTO )
    {
        IDAOComment daoComment = daoFactory.CreateDAOComment();

        try
        {
            await daoComment.Update(requestPutCommentDTO.idComment, requestPutCommentDTO.text);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message }); 
        }
    }
}*/

