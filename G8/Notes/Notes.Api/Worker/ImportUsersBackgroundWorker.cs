using Notes.Application.Services;

namespace Notes.Api.Worker
{
    public class ImportUsersBackgroundWorker // +
        : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ImportUsersBackgroundWorker> logger;

        public ImportUsersBackgroundWorker(IServiceProvider serviceProvider, ILogger<ImportUsersBackgroundWorker> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var externalService = scope.ServiceProvider.GetRequiredService<IUserExternalService>();
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                var users = await externalService.GetExternalUsers(stoppingToken);
                foreach(var user in users)
                {
                    userService.CreateUser(new Application.Models.CreateUserModel
                    {
                        Email = user.Email,
                        ConfirmPassword = user.Password,
                        Password = user.Password,
                        LastName = user.LastName,
                        Name = user.FirstName,
                        UserName = user.Username
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Import of users failed", ex);
            }

            await Task.Delay(1000 * 2, stoppingToken);
        }
    }
}
