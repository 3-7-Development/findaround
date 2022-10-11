using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace findaroundAPI.Entities
{
	public class CommentEntity
	{
		public int Id { get; set; }

		[Required]
		//[ForeignKey("PostEntity")]
		public int PostId { get; set; }

		public PostEntity Post { get; set; }

		[Required]
		//[ForeignKey("UserEntity")]
		public int AuthorId { get; set; }

		public UserEntity Author { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public DateTime PublicationDate { get; set; }
	}
}

