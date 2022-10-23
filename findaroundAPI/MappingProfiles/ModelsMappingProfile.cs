using System;
using AutoMapper;
using findaroundAPI.Entities;
using findaroundShared.Models;
using findaroundShared.Helpers;

namespace findaroundAPI.MappingProfiles
{
	public class ModelsMappingProfile : Profile
	{
		public ModelsMappingProfile()
		{
			CreateMap<UserEntity, User>()
				.ForMember(u => u.loggedIn, mp => mp.MapFrom(e => e.LoggedIn));

			var userToEntity = CreateMap<User, UserEntity>();

            userToEntity.ForAllMembers(options => options.Ignore());

			userToEntity
				.ForMember(e => e.Id, mp => mp.MapFrom(u => u.Id))
				.ForMember(e => e.Login, mp => mp.MapFrom(u => u.Login))
				.ForMember(e => e.ProfileImage, mp => mp.MapFrom(u => u.ProfileImage));

            CreateMap<Post, PostEntity>()
				.ForMember(e => e.Latitude, mp => mp.MapFrom(p => p.Location.Latitude))
				.ForMember(e => e.Longitude, mp => mp.MapFrom(p => p.Location.Longitude))
				.ForMember(e => e.Status, mp => mp.MapFrom(p => p.Status.ToString()))
                .ForMember(e => e.Category, mp => mp.MapFrom(p => p.Category.ToString()));


			CreateMap<PostEntity, Post>()
				.ForMember(p => p.Location, mp => mp.MapFrom(e => new PostLocation()
				{
					Latitude = e.Longitude,
					Longitude = e.Latitude
				}))
				.ForMember(p => p.Images, mp => mp.MapFrom(e => e.Images.Select(x => new PostImage()
				{
					Path = x.Image
				})))
				.ForMember(p => p.Status, mp => mp.MapFrom(e => EnumHelpers.ToPostStatus(e.Status)))
                .ForMember(p => p.Category, mp => mp.MapFrom(e => EnumHelpers.ToPostCategory(e.Category)))
				.ForMember(p => p.AuthorName, mp => mp.MapFrom(e => e.Author.Login));

			CreateMap<PostsImagesEntity, PostImage>()
				.ForMember(p => p.Path, mp => mp.MapFrom(e => e.Image));

			CreateMap<PostImage, PostsImagesEntity>()
				.ForMember(e => e.Image, mp => mp.MapFrom(i => i.Path));

			CreateMap<CommentEntity, Comment>()
				.ForMember(c => c.AuthorName, mp => mp.MapFrom(e => e.Author.Login));

            CreateMap<Comment, CommentEntity>();
        }
	}
}

