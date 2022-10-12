using System;
using System.Text.Json.Serialization;

namespace findaroundShared.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PostCategory
	{
        Spotted,
        Lost,
        HelpUkraine,
        Other
    }
}

