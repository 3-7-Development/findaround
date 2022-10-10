using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace findaroundAPI.Entities
{
	public class PostEntity
	{
		public int Id { get; set; }

		[Required]
		//[ForeignKey("UserEntity")]
		public int AuthorId { get; set; }

		public UserEnitity Author { get; set; }

		public int ResponderId { get; set; } = -1;

		[Required]
		[MinLength(2)]
		public string Title { get; set; }

		[MaxLength(1000)]
		public string Description { get; set; }

		[Required]
		public string Status { get; set; }

		[Required]
		public string Category { get; set; }

		[Required]
		public DateTime CreationDate { get; set; }

		[Required]
		public double Latitude { get; set; }

		[Required]
		public double Longitude { get; set; }

		public virtual List<CommentEntity> Comments { get; set; }

        public virtual List<PostsImagesEntity> Images { get; set; }
    }
}

