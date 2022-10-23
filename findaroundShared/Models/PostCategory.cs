using System;
using System.Text.Json.Serialization;

namespace findaroundShared.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PostCategory
	{
        Spotted=1,
        Lost=2,
        Help=3,
        Events=4,
        HelpUkraine=5,
        Other=6
    }
}

