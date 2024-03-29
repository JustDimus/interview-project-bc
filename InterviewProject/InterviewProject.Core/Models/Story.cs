using Newtonsoft.Json;

namespace InterviewProject.Core.Models;

public class Story
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("postedBy")]
    public required string Author { get; set; }

    [JsonProperty("time")]
    public DateTime Created { get; set; }

    [JsonProperty("text")]
    public string? Text { get; set; }

    [JsonProperty("deleted")]
    public bool? Deleted { get; set; }

    [JsonProperty("dead")]
    public bool? Dead { get; set; }

    [JsonProperty("kids")]
    public required long[] Kids { get; set; }

    [JsonProperty("uri")]
    public required string Url { get; set; }

    [JsonProperty("score")]
    public int Score { get; set; }

    [JsonProperty("title")]
    public required string Title { get; set; }

    [JsonProperty("commentCount")]
    public int Descendants { get; set; }
}