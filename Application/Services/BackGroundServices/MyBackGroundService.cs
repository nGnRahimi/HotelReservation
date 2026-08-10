using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.BackGroundService
{
    public class MyBackGroundService : BackgroundService
    {
        private readonly ILogger<MyBackGroundService> _logger;

        public MyBackGroundService(ILogger<MyBackGroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyBackgroundService is starting.");

            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("MyBackgroundService is doing background work.");

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    // وقتی سرویس متوقف میشه اینجا میاد
                    break;
                }
            }

            _logger.LogInformation("MyBackgroundService has stopped.");
        }
    }
}