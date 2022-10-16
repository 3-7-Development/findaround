using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using LanguageExt.Common;

namespace findaroundAPI.Services
{
	public interface IPostService : IService
	{
		Result<int> AddPost(Post post);
        Result<string> DeletePost(int postId);
		Result<Post> GetPost(int postId);
		Result<IEnumerable<Post>> GetUserPosts();
		Result<IEnumerable<Post>> GetAllPosts();
		Result<IEnumerable<Post>> MatchPosts(PostMatchingDto dto);
        Result<string> AddPostComment(Comment comment);
        Result<string> DeletePostComment(int commentId);
		Result<IEnumerable<Comment>> GetPostComments(int postId);
	}
}

