using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaroundAPI.Services
{
	public interface IPostService
	{
		int AddPost(Post post);
		void DeletePost(int postId);
		Post GetPost(int postId);
		IEnumerable<Post> GetUserPosts();
		IEnumerable<Post> GetAllPosts();
		IEnumerable<Post> MatchPosts(PostMatchingDto dto);
		void AddPostComment(Comment comment);
		void DeletePostComment(int commentId);
		IEnumerable<Comment> GetPostComments(int postId);
	}
}

