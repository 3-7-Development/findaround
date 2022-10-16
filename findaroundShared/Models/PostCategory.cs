using System;
using System.Text.Json.Serialization;

namespace findaroundShared.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PostCategory
	{
        Spotted=1,
        Lost=2,
        HelpUkraine=3,
        Other=4
    }
}

