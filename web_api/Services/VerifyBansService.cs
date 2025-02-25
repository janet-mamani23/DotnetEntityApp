using dao_library.Interfaces.login;
using web_api.Controllers;

namespace web_api.Services
{
    public class VerifyBansService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public VerifyBansService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await VerifyBansAsync();
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private async Task VerifyBansAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userBanController = scope.ServiceProvider.GetRequiredService<UserBanController>();
                await userBanController.VerifyBansCallback();    
            }
        }
    }
}
