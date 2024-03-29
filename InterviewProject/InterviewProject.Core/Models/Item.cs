using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InterviewProject.Core.Models;

public class Item
{
    [Required]
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("deleted")]
    public bool? Deleted { get; set; }

    [Required]
    [JsonProperty("type")]
    public ItemType Type { get; set; }

    [Required]
    [JsonProperty("by")]
    public required string Author { get; set; }

    [Required]
    [JsonProperty("time")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Created { get; set; }

    [JsonProperty("text")]
    public string? Text { get; set; }

    [JsonProperty("dead")]
    public bool? Dead { get; set; }

    [JsonProperty("parent")]
    public long? Parent { get; set; }

    [JsonProperty("pool")]
    public long? Pool { get; set; }

    [JsonProperty("kids")]
    public long[]? Kids { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }

    [JsonProperty("score")]
    public int? Score { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("parts")]
    public long[]? Parts { get; set; }

    [JsonProperty("descendants")]
    public int? Descendants { get; set; }
}