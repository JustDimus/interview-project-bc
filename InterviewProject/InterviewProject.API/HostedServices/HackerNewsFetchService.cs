using InterviewProject.Core.Configuration;
using InterviewProject.DAL.HackerNews;
using Microsoft.Extensions.Options;

namespace InterviewProject.API.HostedServices;

public class HackerNewsFetchService : BackgroundService
{
    private readonly ILogger<HackerNewsFetchService> _logger;

    private readonly IHackerNewsClient _hackerNewsService;

    private readonly HackerNewsOptions _hackerNewsOptions;

    public HackerNewsFetchService(
        IOptions<HackerNewsOptions> hackerNewsOptions,
        IHackerNewsClient hackerNewsService,
        ILogger<HackerNewsFetchService> logger)
    {
        _logger = logger;
        _hackerNewsOptions = hackerNewsOptions.Value;

        _hackerNewsService = hackerNewsService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"{DateTime.UtcNow} : Fetching latest best stories");

            await _hackerNewsService.FetchBestStoriesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(_hackerNewsOptions.BestStoriesRefreshTimeInSeconds), stoppingToken);
        }
    }
}