using System;
using AutoMapper;
using findaroundAPI.Entities;
using findaroundAPI.Services;
using findaroundShared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace findaroundAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/findaround/posts")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("all")]
        public ActionResult<List<Post>> GetAllPosts()
        {
            var result = _postService.GetAllPosts();

            return result.ToList();
        }

        [HttpGet("get/{postId}")]
        public ActionResult<Post> GetPost([FromRoute] int postId)
        {
            var result = _postService.GetPost(postId);

            return result.GetResult();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetUserPosts()
        {
            var result = _postService.GetUserPosts();

            return result.ToList();
        }

        [HttpPost]
        public ActionResult AddPost([FromBody] Post post)
        {
            var result = _postService.AddPost(post);

            return result.GetResult();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePost([FromRoute] int id)
        {
            var result = _postService.DeletePost(id);

            return result.GetResult();
        }

        [HttpGet("comments/{postId}")]
        public ActionResult<List<Comment>> GetPostComments([FromRoute] int postId)
        {
            var result = _postService.GetPostComments(postId);

            return result.ToList();
        }

        [HttpPost("comments")]
        public ActionResult AddPostComment([FromBody] Comment comment)
        {
            var result = _postService.AddPostComment(comment);

            return result.GetResult();
        }

        [HttpDelete("comments/{commentId}")]
        public ActionResult DeletePostComment([FromRoute] int commentId)
        {
            var result = _postService.DeletePostComment(commentId);

            return result.GetResult();
        }
	}
}

