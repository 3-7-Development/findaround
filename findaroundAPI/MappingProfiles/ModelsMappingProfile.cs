using System;
using AutoMapper;
using findaroundAPI.Entities;
using findaroundShared.Models;

namespace findaroundAPI.MappingProfiles
{
	public class ModelsMappingProfile : Profile
	{
		public ModelsMappingProfile()
		{
			CreateMap<Post, PostEntity>()
				.ForMember(e => e.Latitude, mp => mp.MapFrom(p => p.Location.Latitude))
				.ForMember(e => e.Longitude, mp => mp.MapFrom(p => p.Location.Longitute))
				.ForMember(e => e.Images, mp => mp.MapFrom(p => p.Images.Select(x => new PostsImagesEntity()
				{
					Image = x
				})));


			CreateMap<PostEntity, Post>()
				.ForMember(p => p.Images, mp => mp.MapFrom(e => new PostLocation()
				{
					Latitude = e.Latitude,
					Longitute = e.Longitude
				}))
				.ForMember(p => p.Images, mp => mp.MapFrom(e => e.Images.Select(x => new string(x.Image))));
		}
	}
}

