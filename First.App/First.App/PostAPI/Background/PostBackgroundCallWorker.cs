using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue.API.Background;
using BackgroundQueue.API.Service;
using First.App.Business.DTOs;
using First.App.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PostAPI.Background
{
    public class PostBackgroundCallWorker:BackgroundService
    {
        private readonly ILogger<PostBackgroundCallWorker> _logger;
        private HttpClient httpClient;
        private readonly IBackgroundQueue<Post> _queue;
          
        public PostBackgroundCallWorker(ILogger<PostBackgroundCallWorker> logger,IBackgroundQueue<Post> queue)
        {
            _logger = logger;
            _queue = queue;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
                List<PostDto> posts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PostDto>>(request);
                if (request.Length >0)
                {
                    _logger.LogInformation("{Count} Post found in the API", posts.Count);
                    foreach (var post in posts)
                    {
                        _queue.Enqueue(new Post()
                        {
                            Id = post.Id,
                            Title = post.Title,
                            Body = post.Body,
                            UserId = post.UserId
                        });
                    }
                }
                else
                {
                    _logger.LogError("JsonPlaceholder  is down...");
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
