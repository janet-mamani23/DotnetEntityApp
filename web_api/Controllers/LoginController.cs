using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Login")]

    public IActionResult Post(LoginRequestDTO loginRequestDTO)
    {
        return Ok(new LoginResponseDTO
        {
            id= 1,
            name = "Enzo",
            surname = "Piedrasanta",
            description = "Soyestudiante de programacion",
            urlAvatar = "https://m.media-amazon.com/images/I/7186AqfE-ML._AC_SL1500_.jpg"
            mail = "enzomedina630@gmail.com"
        });
    }

    
}
