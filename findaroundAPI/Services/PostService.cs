using System;
using System.Linq;
using AutoMapper;
using findaroundAPI.Authorization;
using findaroundAPI.Entities;
using findaroundAPI.Exceptions;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace findaroundAPI.Services
{
	public class PostService : IPostService
	{
        readonly DatabaseContext _dbContext;
        readonly IMapper _mapper;
        readonly IPostService _postService;
        readonly IAuthorizationService _authorizationService;
        readonly IUserContextService _userContextService;

        public PostService(DatabaseContext dbContext, IMapper mapper, IPostService postService,
            IAuthorizationService authorizationService, IUserContextService userContextService)
		{
            _dbContext = dbContext;
            _mapper = mapper;
            _postService = postService;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
		}

        public int AddPost(Post post)
        {
            var postModel = _mapper.Map<PostEntity>(post);

            var userId = _userContextService.GetUserId;

            if (userId is null)
                throw new ArgumentException("Cannot add post. Something went wrong");

            postModel.AuthorId = (int)userId;

            _dbContext.Posts.Add(postModel);
            _dbContext.SaveChanges();

            return postModel.Id;
        }

        public void AddPostComment(Comment comment)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == comment.PostId);

            if (post is null)
                throw new ArgumentException("Cannot find post");

            var author = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (author is null)
                throw new ArgumentException("Cannot find author");

            var commentModel = _mapper.Map<CommentEntity>(comment);
            commentModel.AuthorId = author.Id;

            _dbContext.PostsComments.Add(commentModel);
            _dbContext.SaveChanges();
        }

        public void DeletePost(int postId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
                throw new ArgumentException("Cannot find user");

            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);

            if (post is null)
                throw new ArgumentException("Cannot find post");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, post, new ResourceOperationRequirements()).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
        }

        public void DeletePostComment(int commentId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
                throw new ArgumentException("Cannot find user");

            var comment = _dbContext.PostsComments.FirstOrDefault(c => c.Id == commentId);

            if (comment is null)
                throw new ArgumentException("Cannot find comment");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, comment, new ResourceOperationRequirements()).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            _dbContext.PostsComments.Remove(comment);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _dbContext.Posts.ToList();

            var postsList = new List<Post>();

            foreach (var post in posts)
                postsList.Add(_mapper.Map<Post>(post));

            return postsList;
        }

        public Post GetPost(int postId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
                throw new ArgumentException("Cannot find user");

            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);

            if (post is null)
                throw new ArgumentException("Cannot find post");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, post, new ResourceOperationRequirements()).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            var mappedPost = _mapper.Map<Post>(post);

            return mappedPost;
        }

        public IEnumerable<Comment> GetPostComments(int postId)
        {
            var post = _dbContext.Posts.Include(p => p.Author).FirstOrDefault(p => p.Id == postId);

            if (post is null)
                throw new ArgumentException("Cannot find post");

            var comments = _dbContext.PostsComments.Where(c => c.PostId == postId);

            var commentsList = new List<Comment>();

            foreach (var comment in comments)
                commentsList.Add(_mapper.Map<Comment>(comment));

            return commentsList;
        }

        public IEnumerable<Post> GetUserPosts()
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
                throw new ArgumentException("Cannot find user");

            var posts = _dbContext.Posts.Where(p => p.AuthorId == user.Id);

            var postsList = new List<Post>();

            foreach (var post in posts)
                postsList.Add(_mapper.Map<Post>(post));

            return postsList;
        }

        public IEnumerable<Post> MatchPosts(PostMatchingDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

