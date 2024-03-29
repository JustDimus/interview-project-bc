using System.ComponentModel.DataAnnotations;

namespace InterviewProject.Core.Configuration;

public class HackerNewsOptions
{
    [Required]
    public required string HackerNewsApiUrl { get; set; }

    [Required]
    public required string BestStoriesEndpoint { get; set; }

    [Required]
    public required string GetItemEndpoint { get; set; }

    [Required]
    public int BestStoriesExpirationInSeconds { get; set; }

    [Required]
    public int StoryExpirationInSeconds { get; set; }

    [Required]
    public int BestStoriesRefreshTimeInSeconds { get; set; }
}