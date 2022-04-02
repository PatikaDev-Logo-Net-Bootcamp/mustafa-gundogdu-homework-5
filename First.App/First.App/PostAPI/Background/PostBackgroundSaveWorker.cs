using System.Threading;
using System.Threading.Tasks;
using First.App.Business.Abstract;
using First.App.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BackgroundWorker = System.ComponentModel.BackgroundWorker;

namespace PostAPI
{
    public class PostBackgroundSaveWorker:BackgroundService
    {
        private readonly BackgroundQueue.API.Background.IBackgroundQueue<Post> queue;
        private readonly IServiceScopeFactory serviceFactory;
        private readonly ILogger<BackgroundWorker> logger;

        public PostBackgroundSaveWorker(BackgroundQueue.API.Background.IBackgroundQueue<Post> queue, IServiceScopeFactory serviceFactory, ILogger<BackgroundWorker> logger)
        {
            this.queue = queue;
            this.serviceFactory = serviceFactory;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("{Type} is now running in the background.", nameof(BackgroundWorker));
            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogCritical("The {Type} is stopping due to a host shutdown, queued items might not be processed anymore.", nameof(BackgroundWorker));
            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                  //  await Task.Delay(500, stoppingToken);
                    var post = queue.Dequeue();
                    if (post == null) continue;
                    
                    logger.LogInformation("Post Found! Starting to process");
                    using (var scope = serviceFactory.CreateScope())
                    {
                        var saver = scope.ServiceProvider.GetRequiredService<IPostService>(); 
                        saver.Add(post);
                    }

                }
                catch (System.Exception ex)
                {
                    logger.LogError("An Error when publishing a book. Exception", ex);
                }
            }
        }
    }
}
