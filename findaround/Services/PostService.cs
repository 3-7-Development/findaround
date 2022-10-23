using System;
using findaround.Utilities;
using findaround.Helpers;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using MonkeyCache.FileStore;
using Newtonsoft.Json;

namespace findaround.Services
{
	public class PostService : IPostService
	{
        readonly HttpClient _client;

        public PostService()
		{
            _client = BackendUtilities.ProduceHttpClient();
        }

        public async Task<bool> AddPost(Post post)
        {            
            _client.SetAuthenticationToken();

            var content = this.GetRequestContent(post);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/posts", content);
            }
            catch (Exception e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<bool> AddPostComment(Comment comment)
        {            
            _client.SetAuthenticationToken();

            var content = this.GetRequestContent(comment);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/posts/comments", content);
            }
            catch (Exception e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<bool> DeletePost(int postId)
        {            
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.DeleteAsync($"api/v1/findaround/posts/{postId}");
            }
            catch (Exception e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<bool> DeletePostComment(int commentId)
        {            
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.DeleteAsync($"api/v1/findaround/posts/comments/{commentId}");
            }
            catch (Exception e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<Post> GetPost(int postId)
        {            
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.GetAsync($"api/v1/findaround/posts/get/{postId}");
            }
            catch (Exception e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<Post>(responseContent);

            return post;
        }

        public async Task<List<Comment>> GetPostComments(int postId)
        {            
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                response = await _client.GetAsync($"api/v1/findaround/posts/comments/{postId}");
            }
            catch (Exception e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var comments = JsonConvert.DeserializeObject<List<Comment>>(responseContent);

            return comments;
        }

        public async Task<List<Post>> GetUserPosts()
        {            
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                response = await _client.GetAsync($"api/v1/findaround/posts");
            }
            catch (Exception e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<Post>>(responseContent);

            return posts;
        }

        public async Task<List<Post>> MatchPosts(PostMatchingDto dto)
        {            
            _client.SetAuthenticationToken();

            var content = this.GetRequestContent(dto);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/posts/comments/match", content);
            }
            catch (Exception e)
            {
                return new List<Post>();
            }

            if (!response.IsSuccessStatusCode)
                return new List<Post>();

            var responseContent = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<Post>>(responseContent);

            var sortedPosts = SortPosts(posts);

            return sortedPosts;
        }

        private List<Post> SortPosts(List<Post> posts)
        {
            var sortedPosts = new List<Post>();

            var userId = UserHelpers.CurrentUser.Id;

            foreach (var post in posts)
            {
                if (post.AuthorId != userId)
                    sortedPosts.Add(post);
            }

            return sortedPosts;
        }
    }
}

