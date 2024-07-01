using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;
using web_api.mock;

namespace web_api.Controllers;

[ApiController] // Anotaciobn,Indica que es un controlador de API(manejan solicitudes y generan respuestas).
[Route("[controller]")] // Anotacion, Define la ruta base para las rutas de este controlador.

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "CreateUser")]
    public IActionResult Post(UserPostRequestDTO userPostRequestDTO)
    {
        if(userPostRequestDTO == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "Datos ingresados erroneos"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.name))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El nombre es un dato obligatorio"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.lastName))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El apellido es un dato obligatorio"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.mail))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El correo electrónico es un dato obligatorio"
            });
        }

        if(userPostRequestDTO.birthdate == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "La fecha de nacimiento es un dato obligatorio"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.password))
        {
            return BadRequest(new ErrorResponseDTO
            {
                success = false,
                message = "El password es un dato obligatorio"
            });
        }

        long id = UserMock.Instance.CreateUser(
            userPostRequestDTO.name, 
            userPostRequestDTO.lastName, 
            userPostRequestDTO.mail, 
            userPostRequestDTO.birthdate,
            userPostRequestDTO.password);

        return Ok(new UserPostResponseDTO
        {
            id = id,
            name = userPostRequestDTO.name,
            lastName = userPostRequestDTO.lastName,
            mail = userPostRequestDTO.mail
        });
    }
}