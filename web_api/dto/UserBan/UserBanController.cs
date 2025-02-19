/*using dao_library.entity_framework;
using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.login;


namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserBanController : ControllerBase
{
    private readonly ILogger<UserBanController> _logger;
    private readonly IDAOFactory daoFactory;

    public UserBanController(
        ILogger<UserBanController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory;
    }     

    [HttpPut]
    [Route("BanUser")]
    public async Task<IActionResult> Banned(UserRequestBannedDTO request)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan();
        User? user = await daoUser.GetByUsername(request.NameUser);
        if (user != null)
        {
            try
            {
                //crear el UserBan(con sus propiedades)
                var userBan = new UserBan
                {
                    User = user,
                    Reason = request.Reason
                };

                userBan.SetBanDuration();
                user.UserStatus = UserStatus.Banned;
                await daoUser.Save(user);
                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "Usuario baneado."
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
        else
        {
            return NotFound(new ResponseDTO
            {
                Success = false,
                Message = "Usuario no encontrado."
            });
        }
    }

    public void VerifyBans(IEnumerable<UserBan> userBans)
    {
        IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan();
        var usersBans = daoUserBan.GetAll();
        foreach (var userBan in usersBans)
        {
            if (userBan.CheckBanStatus())
            {
                userBan.User.UserStatus = UserStatus.Active;// Actualizar el estado del usuario en la base de datos, etc.
            }
        }
    }

    // Método para iniciar una tarea programada.
    public void StartBanVerificationTask()
    {
        Timer timer = new Timer(VerifyBansCallback, null, 0, 3600000); // 3600000 ms = 1 hora
    }

    // Callback del temporizador.
    private void VerifyBansCallback(object state)
    {
        var userBans = GetAllUserBansFromDatabase();
        VerifyBans(userBans);
    }

    // Método para obtener todos los baneos de usuarios desde la base de datos.
    private IEnumerable<UserBan> GetAllUserBansFromDatabase()
    {
        // Implementa la lógica para obtener todos los registros de UserBan desde la base de datos.
    }
}*/