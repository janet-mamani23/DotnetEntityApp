/*using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using dao_library.Interfaces.comment;

[ApiController]
[Route("api/[controller]")]

public class DeleteCommentController : ControllerBase
{
    private readonly IDAOFactory daoFactory;
    public DeleteCommentController(IDAOFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        IDAOComment daoComment = daoFactory.CreateDAOComment();

        try
        {
            await daoComment.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}*/