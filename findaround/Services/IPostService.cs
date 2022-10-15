using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.Services
{
	public interface IPostService : IWebService
	{
		Task<bool> AddPost(Post post);
		Task<bool> DeletePost(int postId);
		Task<Post> GetPost(int postId);
		Task<List<Post>> GetUserPosts(int userId);
		Task<List<Post>> MatchPosts(PostMatchingDto dto);
		Task<bool> AddPostComment(Comment comment);
		Task<bool> DeletePostComment(int commentId);
		Task<List<Comment>> GetPostComments(int postId);
	}
}

