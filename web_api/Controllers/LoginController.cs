using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;
using web_api.mock;
using entities_library.login;
using dao_library.Interfaces.login;
using dao_library.Interfaces;

namespace web_api.Controllers;


[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IDAOFactory daoFactory;

    public LoginController(
        ILogger<LoginController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory;
    }

    [HttpPost(Name = "Login")]
    public async Task<IActionResult> Post(LoginRequestDTO loginRequestDTO)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        
        User user = await daoUser.Get(
            loginRequestDTO.email,
            loginRequestDTO.password
        );

        if( user != null &&
            user.IsPassword(loginRequestDTO.password))
        {
            return Ok(new LoginResponseDTO 
            {
                success = true,
                message = "",
                id = user.Id,
                name = user.Name,
                lastname = user.LastName,
                description = user.Description,
                urlAvatar = "",
                mail = user.Email
            });
        }
        
        return Unauthorized(new ErrorResponseDTO
        {
            success = false,
            message = "Invalid mail or password"
        });
    }
}