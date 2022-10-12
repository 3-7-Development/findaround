using System;
using AutoMapper;
using findaroundAPI.Entities;
using findaroundAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace findaroundAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/findaround/posts")]
    [Authorize]
    public class PostsController
	{
        IPostService _postService;

        public PostsController(IPostService postService)
		{
            _postService = postService;
        }
	}
}

