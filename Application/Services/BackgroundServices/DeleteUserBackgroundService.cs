using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Common;

namespace Application.Services.BackgroundServices;

public class DeleteUserBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                try
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var userToDelete = _unitOfWork.UserRepository.GetAll().Where(u => u.CreatedDate == null && u.IsDeleted == false).ToList();

                    if (userToDelete.Count != 0)
                    {
                        foreach (var user in userToDelete)
                        {
                            user.IsDeleted = true;
                            user.DeletedDate = DateTime.Now;
                            user.DeletedBy = 1;
                        }

                        await _unitOfWork.SaveChanges();
                    }
                }
                catch (Exception ex)
                {

                }

                await Task.Delay(TimeSpan.FromDays(30), stoppingToken);
            }

    }
}
