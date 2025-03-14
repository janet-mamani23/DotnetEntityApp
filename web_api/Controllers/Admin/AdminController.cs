using web_api.dto.user;
using web_api.dto;
using Microsoft.AspNetCore.Mvc;
using dao_library.Interfaces;
using web_api.dto.login;
using web_api.dto.common;
using dao_library.Interfaces.login;
using entities_library.login;

namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IDAOFactory daoFactory;

    public AdminController(
        ILogger<UserController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory;
    } 

    [HttpPost(Name = "CreateAdmin")]
    public async Task<IActionResult> PostAsync(UserPostRequestDTO userPostRequestDTO)
    {
        if(userPostRequestDTO == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Debe llenar el cuerpo de la solicitud."
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.Name))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "El nombre debe ser completado."
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.LastName))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "El apellido debe ser completado."
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.Email))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "El e-mail es requerido."
            });
        }

        if(userPostRequestDTO.Birthdate == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Fecha de cumpleaños requerida."
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.Password))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Debe colocar una contrseña."
            });
        }

        IDAOUser daoUser = daoFactory.CreateDAOUser();
        // Se crea un objeto de tipo User (este es el objeto)
        var user = new User
        {
            Name = userPostRequestDTO.Name,
            LastName = userPostRequestDTO.LastName,
            Email = userPostRequestDTO.Email,
            Password = userPostRequestDTO.Password,
            Birthdate = (DateTime)userPostRequestDTO.Birthdate,
            Description = userPostRequestDTO.Description,
            IsAdmin = true
        };
        // Aquí se llama a un método del objeto 'user'
        user.SetPassword(userPostRequestDTO.Password);
        // Se guarda el objeto 'user' en la base de datos
        long id = await daoUser.Save(user);

        return Ok(new UserPostResponseDTO
        {
            Id = id,
            Name = userPostRequestDTO.Name,
            LastName = userPostRequestDTO.LastName,
            Email = userPostRequestDTO.Email,
            Success = true,
            Message = "Usuario administrador creado con éxito."
        });
    }


    [HttpGet]
    [Route("AllUsersAdmin")]
    public async Task<IActionResult> Get([FromQuery]UserGetAllRequestDTO request)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();

        try
        {
            var (users, totalRecords) = await daoUser.GetAllAdmin(
                request.Query,
                request.Page,
                request.PageSize);

            var userResponse = users.Select(user => new UserGetResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Avatar = user.GetUrlAvatar(),
                UserStatus = user.GetUserStatus(),
                Success = true,
                Message = "Peticion exitosa."
            }).ToList();

            var response = new UserAllResponseDTO
            {
                Users = userResponse,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize,
                Success = true,
                Message = $"Usuarios encontrados {totalRecords}."
            };
            return Ok(response);
        }
        catch  (InvalidOperationException ex)
            {
                return NotFound(new ErrorResponseDTO
            {
                Success = false,
                Message = ex.Message 
            });
            }
    }

    [HttpPut(Name = "PutAdmin")]
    public async Task<IActionResult> PutUser([FromBody]UserPutRequestDTO request)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        try{
            await daoUser.Update(request.IdUser, request.Name,request.LastName,request.Birthdate,request.Email,request.Description);
            return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "Datos actualizados."
                });
            }
        catch  (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponseDTO
            {
                Success = false,
                Message = ex.Message 
            });
            }
    }

     [HttpGet]
    [Route("GetAdmin")]
    public async Task<IActionResult> GetUser([FromQuery]UserGetRequestDTO request)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        
        try{
            var user = await daoUser.GetById(request.UserId);
            if(user != null)
            {   return Ok(new LoginResponseDTO
                    {
                        Id = user.Id,
                        NameUser = user.Name,
                        LastnameUser = user.LastName,
                        DescriptionUser = user.Description,
                        UrlAvatar = user.GetUrlAvatar(),
                        EmailUser = user.Email,
                        Success = true,
                        Message = "Usuario encontrado."
                    });
            }
            else
                {
                    return NotFound( new ErrorResponseDTO
                    {
                        Success = false,
                        Message = "Usuario no encontrado."
                    });
                }
            }
        catch  (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponseDTO
                {
                    Success = false,
                    Message = ex.Message 
                });
            }
    } 

    [HttpDelete(Name = "DeleteAdmin")]
    public async Task<IActionResult> Delete(RequestDeleteDTO request)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        try
        {
            var success = await daoUser.Delete(request.Id);
            if (success)
                {
                    return Ok(new ResponseDTO
                        {
                            Success = true,
                            Message = "Usuario eliminado."
                        }
                        );
                }
            else
                {
                return NotFound(new ErrorResponseDTO
                    {
                        Success = false,
                        Message = "No se pudo eliminar el usuario ya que no esta registrado." 
                    });
                }
        }
        
        catch (Exception ex)
        {
            return StatusCode(500,  $"Internal server error: {ex.Message}");
        }
    } 


}


