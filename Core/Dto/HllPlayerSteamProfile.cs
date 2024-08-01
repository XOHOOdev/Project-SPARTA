using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerSteamProfile
    {
        [JsonProperty(PropertyName = "avatar")]
        public string? Avatar { get; set; }

        [JsonProperty(PropertyName = "steamid")]
        public string? SteamId { get; set; }

        [JsonProperty(PropertyName = "loccityid")]
        public long LocCityId { get; set; }

        [JsonProperty(PropertyName = "avatarfull")]
        public string? AvatarFull { get; set; }

        [JsonProperty(PropertyName = "avatarhash")]
        public string? AvatarHash { get; set; }

        [JsonProperty(PropertyName = "profileurl")]
        public string? ProfileUrl { get; set; }

        [JsonProperty(PropertyName = "personaname")]
        public string? PersonaName { get; set; }

        [JsonProperty(PropertyName = "timecreated")]
        public long TimeCreated { get; set; }

        [JsonProperty(PropertyName = "avatarmedium")]
        public string? AvatarMedium { get; set; }

        [JsonProperty(PropertyName = "locstatecode")]
        public string? LocStateCode { get; set; }

        [JsonProperty(PropertyName = "personastate")]
        public int PersonaState { get; set; }

        [JsonProperty(PropertyName = "profilestate")]
        public int ProfileState { get; set; }

        [JsonProperty(PropertyName = "primaryclanid")]
        public string? PrimaryClanId { get; set; }

        [JsonProperty(PropertyName = "loccountrycode")]
        public string? LocCountryCode { get; set; }

        [JsonProperty(PropertyName = "commentpermission")]
        public int CommentPermission { get; set; }

        [JsonProperty(PropertyName = "personastateflags")]
        public int PersonaStateFlags { get; set; }

        [JsonProperty(PropertyName = "communityvisibilitystate")]
        public int CommunityVisibilityState { get; set; }
    }
}
