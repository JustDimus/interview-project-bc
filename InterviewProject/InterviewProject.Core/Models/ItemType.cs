using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InterviewProject.Core.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum ItemType
{
    Job,
    Story,
    Comment,
    Poll,
    Pollopt
}