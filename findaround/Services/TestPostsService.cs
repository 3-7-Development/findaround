using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.Services
{
	public class TestPostsService 
	{
		public TestPostsService()
		{
		}

        public Task<bool> AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddPostComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetPostComments(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetUserPosts(int userId)
        {
            await Task.Delay(1);

            return new List<Post>()
            {
                new Post()
                {
                    Title = "Test Post 1.0",
                    Description = "This is a test post 1.0",
                    Images = new List<PostImage>()
                    {
                        new PostImage()
                        {
                            Path = "defaultPostImage.png"
                        },
                        new PostImage()
                        {
                            Path = "defaultPostImage.png"
                        },
                        new PostImage()
                        {
                            Path = "defaultPostImage.png"
                        }
                    },
                    Status = PostStatus.Active,
                    Category = PostCategory.Lost,
                    Location = new PostLocation
                    {
                        Latitude = 19.96230,
                        Longitude = 52.12222
                    }
                },
                new Post()
                {
                    Title = "Test Post 2.0",
                    Description = "This is a test post 2.0",
                    Images = new List<PostImage>()
                    {
                        new PostImage()
                        {
                            Path = "defaultPostImage.png"
                        },
                        new PostImage()
                        {
                            Path = "defaultPostImage.png"
                        },
                        new PostImage()
                        {
                            Path = "defaultPostImage.png"
                        }
                    },
                    Status = PostStatus.Active,
                    Category = PostCategory.Lost,
                    Location = new PostLocation
                    {
                        Latitude = 19.96230,
                        Longitude = 52.12222
                    }
                }
            };
        }

        public Task<List<Post>> MatchPosts(PostMatchingDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

