using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace findaroundAPI.Entities
{
	public class PostsImagesEntity
	{
        public int Id { get; set; }

        [Required]
        //[ForeignKey("PostEntity")]
        public int PostId { get; set; }

        public PostEntity Post { get; set; }

        [Required]
        public string Image { get; set; }
    }
}

