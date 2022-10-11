using System;
using System.ComponentModel.DataAnnotations;

namespace findaroundAPI.Entities
{
	public class UserEntity
	{
		public int Id { get; set; }

		[Required]
		[MinLength(8)]
		public string Login { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		[Required]
		public string Salt { get; set; }

		[Required]
		public string ProfileImage { get; set; }

		public virtual List<PostEntity> Posts { get; set; }

		public virtual List<CommentEntity> Comments { get; set; }
	}
}

