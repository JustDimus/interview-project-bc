using InterviewProject.Core.Configuration;
using InterviewProject.Core.Exceptions;
using InterviewProject.Core.Models;
using InterviewProject.DAL.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace InterviewProject.DAL.HackerNews;

public class HackerNewsClient : IHackerNewsClient
{
    private const string BestStoriesKey = "stories:best";
    private const string StoryKeyBase = "story:";

    private readonly ILogger<HackerNewsClient> _logger;

    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _cache;

    private readonly HackerNewsOptions _hackerNewsOptions;

    public HackerNewsClient(
        HttpClient httpClient,
        IDistributedCache cache,
        IOptions<HackerNewsOptions> hackerNewsOptions,
        ILogger<HackerNewsClient> logger)
    {
        _logger = logger;
        _hackerNewsOptions = hackerNewsOptions.Value;

        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task FetchBestStoriesAsync(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(_hackerNewsOptions.BestStoriesEndpoint, cancellationToken);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        var bestStories = JsonConvert.DeserializeObject<long[]>(responseString)
                          ?? throw new InvalidOperationException($"Could not parse response");

        await _cache.SetEntityAsync(
            BestStoriesKey,
            bestStories,
            new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromSeconds(_hackerNewsOptions.BestStoriesExpirationInSeconds)
            },
            cancellationToken);
    }

    public async Task<IEnumerable<Story>> GetBestStoriesAsync(int count, CancellationToken cancellationToken)
    {
        var bestStories = await _cache.GetEntityAsync<long[]>(BestStoriesKey, cancellationToken);

        if (bestStories is null)
        {
            _logger.LogError("No best stories found in cache");
            throw new ServiceException(ServiceErrorType.BestStoriesNotFound);
        }

        var requestedStories = bestStories.Take(count);

        return await GetStoriesAsync(requestedStories, cancellationToken);
    }

    private async Task<IEnumerable<Story>> GetStoriesAsync(IEnumerable<long> stories,
        CancellationToken cancellationToken)
    {
        var tasks = stories.Select(storyId => GetStoryAsync(storyId, cancellationToken));

        return await Task.WhenAll(tasks);
    }

    private async Task<Story> GetStoryAsync(long storyId, CancellationToken cancellationToken)
    {
        var story = await _cache.GetEntityAsync<Story>($"{StoryKeyBase}{storyId}", cancellationToken);

        if (story is not null)
        {
            return story;
        }

        var response = await _httpClient.GetAsync($"{string.Format(_hackerNewsOptions.GetItemEndpoint, storyId)}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        var item = JsonConvert.DeserializeObject<Item>(responseString)
                   ?? throw new InvalidOperationException($"Could not parse response");

        story = ConvertItemToStory(item);

        await _cache.SetEntityAsync(
            $"{StoryKeyBase}{storyId}",
            story,
            new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_hackerNewsOptions.StoryExpirationInSeconds)
            },
            cancellationToken);

        return story;
    }

    private Story ConvertItemToStory(Item item)
        => new()
        {
            Score = item.Score.GetValueOrDefault(),
            Descendants = item.Descendants.GetValueOrDefault(),
            Author = item.Author,
            Created = item.Created,
            Title = item.Title!,
            Url = item.Url!
        };
}