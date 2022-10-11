using System;
using System.ComponentModel.DataAnnotations;

namespace findaroundShared.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public PostStatus Status { get; set; }
        public PostCategory Category { get; set; }
        public PostLocation Location { get; set; }
		public List<string> Images { get; set; }
		public int AuthorId { get; set; }
	}
}

