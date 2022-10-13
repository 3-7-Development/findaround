using System;
using System.Text.Json.Serialization;

namespace findaroundShared.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PostStatus
	{
        Active,
        Ended,
        Banned,
        Hidden,
        Removed,
        Null
    }
}

