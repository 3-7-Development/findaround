using System;
using AutoMapper;
using findaroundAPI.Entities;
using findaroundShared.Models;
using findaroundAPI.Helpers;

namespace findaroundAPI.MappingProfiles
{
	public class ModelsMappingProfile : Profile
	{
		public ModelsMappingProfile()
		{
			CreateMap<UserEntity, User>();

			CreateMap<Post, PostEntity>()
				.ForMember(e => e.Latitude, mp => mp.MapFrom(p => p.Location.Latitude))
				.ForMember(e => e.Longitude, mp => mp.MapFrom(p => p.Location.Longitute))
				.ForMember(e => e.Images, mp => mp.MapFrom(p => p.Images.Select(x => new PostsImagesEntity()
				{
					Image = x
				})))
				.ForMember(e => e.Status, mp => mp.MapFrom(p => p.Status.ToString()))
                .ForMember(e => e.Category, mp => mp.MapFrom(p => p.Category.ToString()));


			CreateMap<PostEntity, Post>()
				.ForMember(p => p.Images, mp => mp.MapFrom(e => new PostLocation()
				{
					Latitude = e.Latitude,
					Longitute = e.Longitude
				}))
				.ForMember(p => p.Images, mp => mp.MapFrom(e => e.Images.Select(x => new string(x.Image))))
				.ForMember(p => p.Status, mp => mp.MapFrom(e => EnumHelpers.ToPostStatus(e.Status)))
                .ForMember(p => p.Category, mp => mp.MapFrom(e => EnumHelpers.ToPostCategory(e.Category)));
		}
	}
}

