using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;
namespace web_api.Controllers;
using web_api.mock;
using entities_library.login;

[ApiController] // Anotaciobn,Indica que es un controlador de API(manejan solicitudes y generan respuestas).
[Route("[controller]")] // Anotacion, Define la ruta base para las rutas de este controlador.
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger; //variable privada de solo lectura. Tiene como tipo una interfaz porporcionada por ASP.NET Coer para registros de eventos (logging)
    public LoginController(ILogger<LoginController> logger) // registra informacion, advertencias o errores en el registro de la aplicacion.<LoginController> contexto en el que se registraràn los eventos.
    {
        _logger = logger;
    }

    [HttpPost(Name = "Login")] // Metodo que maneja solicitudes http post y se le da de nombre "Login" a la ruta.

    public IActionResult Post(LoginRequestDTO loginRequestDTO)
    {
        foreach(User user in UserMock.Instance.Users)
        {
            if(loginRequestDTO != null &&
               loginRequestDTO.mail.ToLower().Equals(user.Email) &&
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
        }
        
        
        return Unauthorized(new ErrorResponseDTO
        {
            success = false,
            message = "Invalid mail or password"
        });
    }
}   