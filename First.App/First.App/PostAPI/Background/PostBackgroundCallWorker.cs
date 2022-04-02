using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue.API.Background;
using BackgroundQueue.API.Service;
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
                var request = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
                
                if (request.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Request status code {StatusCode}", request.StatusCode);
                    var content = await request.Content.ReadAsStringAsync();
                    var posts = JsonSerializer.Deserialize<List<Post>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    foreach (var post in posts)
                    {
                        _queue.Enqueue(new Post
                        {
                            Id = post.Id,
                            UserId = post.UserId,
                            Title = post.Title,
                            Body = post.Body
                        });
                    }
                }
                else
                {
                    _logger.LogError("JsonPlaceholder  is down Status Code {StatusCode}", request.StatusCode);
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
