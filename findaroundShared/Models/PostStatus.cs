using System;
using System.Text.Json.Serialization;

namespace findaroundShared.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PostStatus
	{
        Active=1,
        Ended=2,
        Banned=3,
        Hidden=4,
        Removed=5,
        Null=6
    }
}

