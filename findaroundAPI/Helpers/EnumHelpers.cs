using System;
using findaroundAPI.Models;

namespace findaroundAPI.Helpers
{
	public static class EnumHelpers
	{
		public static PostStatus ToPostStatus(string value)
		{
			var result = Enum.TryParse(value, out PostStatus postStatus);

			if (!result)
                throw new ArgumentException($"Cannot convert {value} to PostStatus");

            return postStatus;
        }

		public static PostCategory ToPostCategory(string value)
		{
			var result = Enum.TryParse(value, out PostCategory postCategory);

			if (!result)
				throw new ArgumentException($"Cannot convert {value} to PostCategory");

			return postCategory;
		}
	}
}

