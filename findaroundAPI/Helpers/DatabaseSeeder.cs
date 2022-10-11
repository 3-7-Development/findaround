using System;
using findaroundAPI.Entities;
using findaroundAPI.Models;
using findaroundAPI.Utilities;
using findaroundShared.Models;

namespace findaroundAPI.Helpers
{
	public class DatabaseSeeder
	{
		readonly DatabaseContext dbContext;

		public DatabaseSeeder(DatabaseContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Seed()
		{
			if (dbContext.Database.CanConnect())
			{
                if (!dbContext.Users.Any())
                {
                    var defaultUsers = GetUsers().ToList();
                    dbContext.Users.AddRange(defaultUsers);
                    dbContext.SaveChanges();

                    var users = dbContext.Users.ToList().ToList();

                    var testUser = users.FirstOrDefault(u => u.Login == defaultUsers[0].Login);
                    var proPlayer = users.FirstOrDefault(u => u.Login == defaultUsers[1].Login);

                    // 1st user

                    var posts = GetPosts().ToList();

                    posts[0].AuthorId = testUser.Id;
                    posts[1].AuthorId = testUser.Id;

                    dbContext.Posts.AddRange(posts);
                    dbContext.SaveChanges();


                    posts = GetPosts().ToList();

                    posts[0].AuthorId = proPlayer.Id;
                    posts[1].AuthorId = proPlayer.Id;

                    dbContext.Posts.AddRange(posts);
                    dbContext.SaveChanges();

                    posts = dbContext.Posts.Where(p => p.AuthorId == testUser.Id).ToList();

                    foreach (var post in posts)
                    {
                        var comments = GetPostsComments();
                        var images = GetPostsImages();

                        foreach (var comment in comments)
                        {
                            comment.AuthorId = testUser.Id;
                            comment.PostId = post.Id;
                        }

                        dbContext.PostsComments.UpdateRange(comments);
                        dbContext.SaveChanges();

                        foreach (var image in images)
                        {
                            image.PostId = post.Id;
                        }

                        dbContext.PostsImages.AddRange(images);

                        post.Title += $" by {testUser.Login}";
                        dbContext.Posts.Update(post);

                        dbContext.SaveChanges();
                    }

                    // 2nd user

                    //posts = GetPosts().ToList();

                    //posts[0].AuthorId = proPlayer.Id;
                    //posts[1].AuthorId = proPlayer.Id;

                    //dbContext.Posts.AddRange(posts);
                    //dbContext.SaveChanges();


                    //posts = GetPosts().ToList();

                    //posts[0].AuthorId = proPlayer.Id;
                    //posts[1].AuthorId = proPlayer.Id;

                    //dbContext.Posts.AddRange(posts);
                    //dbContext.SaveChanges();

                    //posts = dbContext.Posts.Where(p => p.AuthorId == proPlayer.Id).ToList();

                    //foreach (var post in posts)
                    //{
                    //    var comments = GetPostsComments();
                    //    var images = GetPostsImages();

                    //    foreach (var comment in comments)
                    //    {
                    //        comment.AuthorId = proPlayer.Id;
                    //        comment.PostId = post.Id;
                    //    }

                    //    dbContext.PostsComments.UpdateRange(comments);
                    //    dbContext.SaveChanges();

                    //    foreach (var image in images)
                    //    {
                    //        image.PostId = post.Id;
                    //    }

                    //    dbContext.PostsImages.AddRange(images);

                    //    post.Title += $" by {proPlayer.Login}";
                    //    dbContext.Posts.Update(post);

                    //    dbContext.SaveChanges();
                    //}
                }

                if (!dbContext.Posts.Any())
                {
                    var defaultUsers = GetUsers().ToList();
                    var users = dbContext.Users.ToList().ToList();

                    var testUser = users.FirstOrDefault(u => u.Login == defaultUsers[0].Login);
                    var proPlayer = users.FirstOrDefault(u => u.Login == defaultUsers[1].Login);

                    // 1st user

                    var posts = GetPosts().ToList();

                    posts[0].AuthorId = testUser.Id;
                    posts[1].AuthorId = testUser.Id;

                    dbContext.Posts.AddRange(posts);
                    dbContext.SaveChanges();


                    posts = GetPosts().ToList();

                    posts[0].AuthorId = proPlayer.Id;
                    posts[1].AuthorId = proPlayer.Id;

                    dbContext.Posts.AddRange(posts);
                    dbContext.SaveChanges();

                    posts = dbContext.Posts.Where(p => p.AuthorId == testUser.Id).ToList();

                    foreach (var post in posts)
                    {
                        var comments = GetPostsComments();
                        var images = GetPostsImages();

                        foreach (var comment in comments)
                        {
                            comment.AuthorId = testUser.Id;
                            comment.PostId = post.Id;
                        }

                        dbContext.PostsComments.UpdateRange(comments);
                        dbContext.SaveChanges();

                        foreach (var image in images)
                        {
                            image.PostId = post.Id;
                        }

                        dbContext.PostsImages.AddRange(images);

                        post.Title += $" by {testUser.Login}";
                        dbContext.Posts.Update(post);

                        dbContext.SaveChanges();
                    }

                    // 2nd user

                    //posts = GetPosts().ToList();

                    //posts[0].AuthorId = proPlayer.Id;
                    //posts[1].AuthorId = proPlayer.Id;

                    //dbContext.Posts.AddRange(posts);
                    //dbContext.SaveChanges();


                    //posts = GetPosts().ToList();

                    //posts[0].AuthorId = proPlayer.Id;
                    //posts[1].AuthorId = proPlayer.Id;

                    //dbContext.Posts.AddRange(posts);
                    //dbContext.SaveChanges();

                    //posts = dbContext.Posts.Where(p => p.AuthorId == proPlayer.Id).ToList();

                    //foreach (var post in posts)
                    //{
                    //    var comments = GetPostsComments();
                    //    var images = GetPostsImages();

                    //    foreach (var comment in comments)
                    //    {
                    //        comment.AuthorId = proPlayer.Id;
                    //        comment.PostId = post.Id;
                    //    }

                    //    dbContext.PostsComments.UpdateRange(comments);
                    //    dbContext.SaveChanges();

                    //    foreach (var image in images)
                    //    {
                    //        image.PostId = post.Id;
                    //    }

                    //    dbContext.PostsImages.AddRange(images);

                    //    post.Title += $" by {proPlayer.Login}";
                    //    dbContext.Posts.Update(post);

                    //    dbContext.SaveChanges();
                    //}
                }
            }
		}

        private IEnumerable<UserEntity> GetUsers()
        {
            var testUserPasswordData = PasswordsUtilities.HashPassword("Th3B35tT35tU53r");
            var proPlayerPasswordData = PasswordsUtilities.HashPassword("Pr0Pl4y3r2137");

            return new List<UserEntity>()
            {
                new UserEntity()
                {
                    Login = "testUser",
                    PasswordHash = testUserPasswordData.PasswordHash,
                    Salt = testUserPasswordData.Salt,
                    ProfileImage = "defaultProfileImg.png",
                    LoggedIn = false
                },
                new UserEntity()
                {
                    Login = "proPlayer2137",
                    PasswordHash = proPlayerPasswordData.PasswordHash,
                    Salt = testUserPasswordData.Salt,
                    ProfileImage = "defaultProfileImg.png",
                    LoggedIn = false
                }
            };
        }

        private IEnumerable<PostsImagesEntity> GetPostsImages()
        {
            return new List<PostsImagesEntity>()
            {
                new PostsImagesEntity()
                {
                    Image = "defaultPostImage.png"
                },
                new PostsImagesEntity()
                {
                    Image = "defaultPostImage.png"
                },
                new PostsImagesEntity()
                {
                    Image = "defaultPostImage.png"
                }
            };
        }

        private IEnumerable<CommentEntity> GetPostsComments()
        {
            return new List<CommentEntity>()
            {
                new CommentEntity()
                {
                    Content = "This is a test comment",
                    PublicationDate = DateTime.Now
                },
                new CommentEntity()
                {
                    Content = "This is a seconds test comment",
                    PublicationDate = DateTime.Now
                }
            };
        }

        private IEnumerable<PostEntity> GetPosts()
		{
			return new List<PostEntity>()
			{
                new PostEntity()
                {
					Title = "Test Post 1.0",
                    Description = "This is a test post 1.0",
                    Images = new List<PostsImagesEntity>(),
                    Status = PostStatus.Active.ToString(),
                    Category = PostCategory.Lost.ToString(),
                    CreationDate = DateTime.Now,
                    Latitude = 19.96230,
                    Longitude = 52.12222
                },
                new PostEntity()
                {
                    Title = "Test Post 2.0",
                    Description = "This is a test post 2.0",
                    Images = new List<PostsImagesEntity>(),
                    Status = PostStatus.Active.ToString(),
                    Category = PostCategory.Lost.ToString(),
                    CreationDate = DateTime.Now,
                    Latitude = 19.96230,
                    Longitude = 52.12222
                }
            };
		}
	}
}

