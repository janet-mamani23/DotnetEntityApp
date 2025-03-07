using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;
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

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Post(LoginRequestDTO loginRequestDTO)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        
        User? user = await daoUser.Get(
            loginRequestDTO.EmailUser,
            loginRequestDTO.PasswordUser
        );
        if(user != null)
        {
            if(user.UserStatus == UserStatus.Active && user.VerifyPassword(loginRequestDTO.PasswordUser))
            {
                web_api.helpers.VisitCounter visitCounter = web_api.helpers.VisitCounter.GetInstance();
                if(visitCounter != null)
                {
                    visitCounter.GetNextNumber();
                }
                return Ok(new LoginResponseDTO 
                {
                    Success = true,
                    Message = "Login Exitoso.",
                    Id = user.Id,
                    NameUser = user.Name,
                    LastnameUser = user.LastName,
                    DescriptionUser = user.Description,
                    EmailUser = user.Email,
                    UrlAvatar = user.GetUrlAvatar(),
        
                });
            }
            else
            {
                return Unauthorized(new ErrorResponseDTO
                {
                    Success = false,
                    Message = "El usuario se encuentra banneado"
                });
            }
        }  
        return Unauthorized(new ErrorResponseDTO
        {
            Success = false,
            Message = "Email o pasword incorrecto"
        });
    }

    [HttpPost]
    [Route("LogOut")]
    public IActionResult LogOut()
    {
        try
        {
            web_api.helpers.VisitCounter visitCounter = web_api.helpers.VisitCounter.GetInstance();
            visitCounter.GetRestarNumber();
            return Ok(new ResponseDTO
            { 
                Success = true,
                Message = "Sesion cerrada." 
            });
        }
        catch (InvalidOperationException ex)
        {
           return Conflict(new ErrorResponseDTO
            {
                Success = false,
                Message = ex.Message 
            });
        }
    }
}
    
