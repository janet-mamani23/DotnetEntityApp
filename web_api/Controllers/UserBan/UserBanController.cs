using dao_library.Interfaces;
using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.AspNetCore.Mvc;
using web_api.dto.common;
using web_api.dto.UserBanned;

namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserBanController : ControllerBase
{
    private readonly ILogger<UserBanController> _logger;
    private readonly IDAOFactory daoFactory;
    //private readonly Timer _timer;

    public UserBanController(
        ILogger<UserBanController> logger,
        IDAOFactory daoFactory)
    {
        _logger = logger;
        this.daoFactory = daoFactory;
        //_timer = new Timer(VerifyBansCallback, null, 0, 3600000);
    }     

    [HttpPost]
    [Route("BanUser")]
    public async Task<IActionResult> Banned(UserRequestBannedDTO request)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan();
        User? user = await daoUser.GetByUsername(request.NameUser, request.LastNameUser);
        if (user != null)
        {
            try
            {
                var userBan = new UserBan
                {
                    User = user,
                    Reason = request.Reason
                };

                userBan.SetBanDuration();
                await daoUser.UpdateStatus(user.Id,"activate");
                await daoUserBan.Save(userBan);

                return Ok(new ResponseDTO
                {
                    Success = true,
                    Message = "Usuario banneado con éxito."
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

    private async Task VerifyBans(IEnumerable<UserBan> usersBans)
    {
        IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan();
        foreach (var userBan in usersBans)
        {
            var userCheck = userBan.CheckBanStatus(); 
            if(userCheck == true)
            {          
                await daoUserBan.Delete(userBan.Id);               
            }        
        }
    }

    private async void VerifyBansCallback(object? state)
    {
        try
        {
            IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan();
            var (usersBans,totalBan) = await daoUserBan.GetAll();
            await VerifyBans(usersBans);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en VerifyBansCallback: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("GetAllUserBanned")]
    public async Task<IActionResult>Get()
    {
        IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan();
        
        try
        {
            var (usersBans, totalBan) = await daoUserBan.GetAll();
            List<GetAllResponseDTO> response = new List<GetAllResponseDTO>();
            foreach (var userBan in usersBans)
            {
                {
                    response.Add(new GetAllResponseDTO
                    {
                        Id = userBan.User.Id,
                        NameUser = userBan.User.Name,
                        LastName = userBan.User.LastName,
                        Avatar = userBan.User.GetUrlAvatar(),
                        Success = true,
                        Message = "Este usuario está banneado."
                    });
                }
            }
            return Ok(new TotalResponseDTO
            {
                UsersBan = response,
                Success = true,
                Message = $"Total de usuarios banneados {totalBan}."
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

    [HttpPut]
    [Route("PutBanner")]
    public async Task<IActionResult> Put(UserRequestBannedDTO request)
    {
        IDAOUser daoUser = daoFactory.CreateDAOUser();
        IDAOUserBan daoUserBan = daoFactory.CreateDAOUserBan(); 
        var user = await daoUser.GetByUsername(request.NameUser, request.LastNameUser);
        var userBan = await daoUserBan.GetByName(request.NameUser, request.LastNameUser);
        if(userBan != null && user != null)
        {
            userBan.DissBanned();
            await daoUserBan.Delete(userBan.Id);
            await daoUser.UpdateStatus(user.Id,"deactivate");
            return Ok(new GetAllResponseDTO
            {
                Id = user.Id,
                NameUser = user.Name,
                LastName = user.LastName,
                Avatar = user.GetUrlAvatar(),
                Success = true,
                Message = "Usuario desbaneado."
            });
        }
        else
        {
            return NotFound(new ResponseDTO
            {
                Success = false,
                Message = "El usuario no existe registrado."
            });
        }    
    }
}
//TODO - aplicar servicio de autoverificacion de banneo.