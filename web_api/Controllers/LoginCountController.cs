using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;
using web_api.helpers;


namespace web_api.Controllers;


[ApiController]
[Route("[controller]")]
public class LoginCountController : ControllerBase
{
    private readonly ILogger<LoginCountController> _logger;
    
    public LoginCountController(ILogger<LoginCountController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetNumber")]
    public IActionResult Get()
    {
        VisitCounter visitCounter = VisitCounter.GetInstance();
        long logins = visitCounter.GetNumber();

        return Ok(new LogOutResponseDTO
            { 
                NumberLogin = logins,
                Success = true,
                Message = "Estas son el total de sesiones activas en la app." 
            });
    }
}