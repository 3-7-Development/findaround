using System;
using System.Linq;
using AutoMapper;
using findaroundAPI.Authorization;
using findaroundAPI.Entities;
using findaroundAPI.Exceptions;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace findaroundAPI.Services
{
	public class PostService : IPostService
	{
        readonly DatabaseContext _dbContext;
        readonly IMapper _mapper;
        readonly IAuthorizationService _authorizationService;
        readonly IUserContextService _userContextService;

        public PostService(DatabaseContext dbContext, IMapper mapper,
            IAuthorizationService authorizationService, IUserContextService userContextService)
		{
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
		}

        public Result<int> AddPost(Post post)
        {
            var postModel = _mapper.Map<PostEntity>(post);

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
            {
                var exception = new ArgumentException("Cannot add post. Something went wrong");
                return new Result<int>(exception);
            }

            if (!user.LoggedIn)
            {
                var exception = new UserNotLoggedInException();
                return new Result<int>(exception);
            }

            postModel.AuthorId = (int)user.Id;

            _dbContext.Posts.Add(postModel);
            _dbContext.SaveChanges();

            return new Result<int>(postModel.Id);
        }

        public Result<string> AddPostComment(Comment comment)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == comment.PostId);

            if (post is null)
            {
                var exception = new ArgumentException("Cannot find post");
                return new Result<string>(exception);
            }

            var author = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (author is null)
            {
                var exception = new ArgumentException("Cannot find author");
                return new Result<string>(exception);
            }

            if (!author.LoggedIn)
                throw new UserNotLoggedInException();

            var commentModel = _mapper.Map<CommentEntity>(comment);
            commentModel.AuthorId = (int)_userContextService.GetUserId;
            commentModel.PublicationDate = DateTime.Now;

            _dbContext.PostsComments.Add(commentModel);
            _dbContext.SaveChanges();

            return new Result<string>("Success");
        }

        public Result<string> DeletePost(int postId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
            {
                var exception = new ArgumentException("Cannot find user");
                return new Result<string>(exception);
            }

            if (!user.LoggedIn)
            {
                var exception = new UserNotLoggedInException();
                return new Result<string>(exception);
            }

            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);

            if (post is null)
            {
                var exception = new ArgumentException("Cannot find post");
                return new Result<string>(exception);
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, post, new ResourceOperationRequirements()).Result;

            if (!authorizationResult.Succeeded)
            {
                var exception = new ForbidException();
                return new Result<string>(exception);
            }

            var comments = _dbContext.PostsComments.Where(c => c.PostId == post.Id);

            foreach (var comment in comments)
                _dbContext.Remove(comment);

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();

            return new Result<string>("Success");
        }

        public Result<string> DeletePostComment(int commentId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
            {
                var exception = new ArgumentException("Cannot find user");
                return new Result<string>(exception);
            }

            if (!user.LoggedIn)
            {
                var exception = new UserNotLoggedInException();
                return new Result<string>(exception);
            }

            var comment = _dbContext.PostsComments.FirstOrDefault(c => c.Id == commentId);

            if (comment is null)
            {
                var exception = new ArgumentException("Cannot find comment");
                return new Result<string>(exception);
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, comment, new ResourceOperationRequirements()).Result;

            if (!authorizationResult.Succeeded)
            {
                var exception = new ForbidException();
                return new Result<string>(exception);
            }

            _dbContext.PostsComments.Remove(comment);
            _dbContext.SaveChanges();

            return new Result<string>("Success");
        }

        public Result<IEnumerable<Post>> GetAllPosts()
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null || !user.LoggedIn)
            {
                var exception = new ForbidException();
                return new Result<IEnumerable<Post>>(exception);
            }

            var posts = _dbContext.Posts.Include(p => p.Images).ToList();

            var postsList = new List<Post>();

            foreach (var post in posts)
                postsList.Add(_mapper.Map<Post>(post));

            return new Result<IEnumerable<Post>>(postsList);
        }

        public Result<Post> GetPost(int postId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
            {
                var exception = new ArgumentException("Cannot find user");
                return new Result<Post>(exception);
            }

            if (!user.LoggedIn)
            {
                var exception = new UserNotLoggedInException();
                return new Result<Post>(exception);
            }

            var post = _dbContext.Posts.Include(p => p.Images).FirstOrDefault(p => p.Id == postId);

            if (post is null)
            {
                var exception = new ArgumentException("Cannot find post");
                return new Result<Post>(exception);
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, post, new ResourceOperationRequirements()).Result;

            if (!authorizationResult.Succeeded)
            {
                var exception = new ForbidException();
                return new Result<Post>(exception);
            }

            var mappedPost = _mapper.Map<Post>(post);

            return new Result<Post>(mappedPost);
        }

        public Result<IEnumerable<Comment>> GetPostComments(int postId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null || !user.LoggedIn) {
                var exception = new ForbidException();
                return new Result<IEnumerable<Comment>>(exception);
            }

            var post = _dbContext.Posts.Include(p => p.Author).FirstOrDefault(p => p.Id == postId);

            if (post is null)
            {
                var exception = new ArgumentException("Cannot find post");
                return new Result<IEnumerable<Comment>>(exception);
            }

            var comments = _dbContext.PostsComments.Where(c => c.PostId == postId);

            var commentsList = new List<Comment>();

            foreach (var comment in comments)
                commentsList.Add(_mapper.Map<Comment>(comment));

            return new Result<IEnumerable<Comment>>(commentsList);
        }

        public Result<IEnumerable<Post>> GetUserPosts()
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);

            if (user is null)
            {
                var exception = new ArgumentException("Cannot find user");
                return new Result<IEnumerable<Post>>(exception);
            }

            if (!user.LoggedIn)
            {
                var exception = new UserNotLoggedInException();
                return new Result<IEnumerable<Post>>(exception);
            }

            var posts = _dbContext.Posts.Include(p => p.Images).Where(p => p.AuthorId == user.Id);

            var postsList = new List<Post>();

            foreach (var post in posts)
                postsList.Add(_mapper.Map<Post>(post));

            return new Result<IEnumerable<Post>>(postsList);
        }

        public Result<IEnumerable<Post>> MatchPosts(PostMatchingDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

