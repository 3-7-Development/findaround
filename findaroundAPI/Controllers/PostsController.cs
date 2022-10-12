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
            var posts = _postService.GetAllPosts();

            return Ok(posts);
        }

        [HttpGet("get/{postId}")]
        public ActionResult<Post> GetPost([FromRoute] int postId)
        {
            var post = _postService.GetPost(postId);

            return Ok(post);
        }

        [HttpGet]
        public ActionResult<List<Post>> GetUserPosts()
        {
            var posts = _postService.GetUserPosts();

            return Ok(posts);
        }

        [HttpPost]
        public ActionResult AddPost([FromBody] Post post)
        {
            _postService.AddPost(post);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePost([FromRoute] int id)
        {
            _postService.DeletePost(id);

            return Ok();
        }

        [HttpGet("comments/{postId}")]
        public ActionResult<List<Comment>> GetPostComments([FromRoute] int postId)
        {
            var comments = _postService.GetPostComments(postId);

            return Ok(comments);
        }

        [HttpPost("comments")]
        public ActionResult AddPostComment([FromBody] Comment comment)
        {
            _postService.AddPostComment(comment);

            return Ok();
        }

        [HttpDelete("comments/{commentId}")]
        public ActionResult DeletePostComment([FromRoute] int commentId)
        {
            _postService.DeletePostComment(commentId);

            return Ok();
        }
	}
}

