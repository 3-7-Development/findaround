using System;
namespace findaroundShared.Models.Dtos
{
	public class PostMatchingDto
	{
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public PostStatus? Status { get; set; }
        public PostCategory Category { get; set; }
        public PostLocation Location { get; set; }
        public double Distance { get; set; }
        public int? AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}

