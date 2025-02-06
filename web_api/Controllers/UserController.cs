using dao_library.entity_framework;
using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;


namespace web_api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IDAOFactory daoFactory;

    public UserController(
        ILogger<UserController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory;
    } 

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> PostAsync(UserPostRequestDTO userPostRequestDTO)
    {
        if(userPostRequestDTO == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "I entered wrong data"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.Name))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "The name is mandatory information"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.LastName))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "The last name is mandatory information"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.Email))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Email is required"
            });
        }

        if(userPostRequestDTO.Birthdate == null)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "Date of birth is mandatory information"
            });
        }

        if(string.IsNullOrEmpty(userPostRequestDTO.Password))
        {
            return BadRequest(new ErrorResponseDTO
            {
                Success = false,
                Message = "The password is mandatory information"
            });
        }

       // Crea la instancia del DAO para usuarios
        IDAOUser daoUser = daoFactory.CreateDAOUser();


        var user = new User
        {
            Name = userPostRequestDTO.Name,
            LastName = userPostRequestDTO.LastName,
            Email = userPostRequestDTO.Email,
            Birthdate = (DateTime)userPostRequestDTO.Birthdate,
            Password = userPostRequestDTO.Password
        };
       // Llama a un método dentro del DAO para guardar el usuario
        long id = await daoUser.Save(user);


        return Ok(new UserPostResponseDTO
        {
            Id = id,
            Name = userPostRequestDTO.Name,
            LastName = userPostRequestDTO.LastName,
            Email = userPostRequestDTO.Email,
            Success = true,
        });
    }


    [HttpGet(Name = "GetAllUsers")]
    public async Task<IActionResult> Get(
        [FromQuery]UserGetAllRequestDTO request)
    {
        IDAOUser daoUser = this.daoFactory.CreateDAOUser();

        var (users, totalRecords) = await daoUser.GetAll(
            request.Query,
            request.Page,
            request.PageSize);

        return Ok(users);
    }
}
