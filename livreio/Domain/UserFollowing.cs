using System.Text.Json.Serialization;
using livreio.API;

namespace livreio.Domain;

// an observer follows a target
public class UserFollowing
{
    public string ObserverId { get; set; }
    [JsonIgnore]
    public AppUser Observer { get; set; }
    public string TargetId { get; set; }
    [JsonIgnore]
    public AppUser Target { get; set; }
}