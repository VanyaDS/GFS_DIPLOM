using AutoMapper;
using GeoFlat.Server.Automapper.RequestModels;
using GeoFlat.Server.Automapper.ResponseModels;
using GeoFlat.Server.Models.Database.Entities;

namespace GeoFlat.Server.Automapper
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<RecordRequest, Record>()
				.ForMember(dest => dest.PicturesPath, opt => opt.MapFrom(src => src.PicturesPath))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.RecordTitle, opt => opt.MapFrom(src => src.RecordTitle))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(dest => dest.RentType, opt => opt.MapFrom(src => src.RentType))
				.ForMember(dest => dest.RentStatus, opt => opt.MapFrom(src => src.RentStatus))
				.ForMember(dest => dest.HasFurniture, opt => opt.MapFrom(src => src.HasFurniture))
				.ForMember(dest => dest.NotForStudents, opt => opt.MapFrom(src => src.NotForStudents))
				.ForMember(dest => dest.WithoutAnimals, opt => opt.MapFrom(src => src.WithoutAnimals))
				.ForMember(dest => dest.WithoutChildren, opt => opt.MapFrom(src => src.WithoutChildren))
				.ForMember(dest => dest.IsAgent, opt => opt.MapFrom(src => src.IsAgent))
				.ForMember(dest => dest.WithInternet, opt => opt.MapFrom(src => src.WithInternet))
				.ForMember(dest => dest.ForDay, opt => opt.MapFrom(src => src.ForDay));

			CreateMap<RecordRequest, Flat>()	
				.ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
				.ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))
				.ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Floor));
				
			CreateMap<RecordRequest, Geolocation>()
				.ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
				.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
				.ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.StreetName));

			CreateMap<Record, RecordResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
				.ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
				.ForMember(dest => dest.PicturesPath, opt => opt.MapFrom(src => src.PicturesPath))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.RecordTitle, opt => opt.MapFrom(src => src.RecordTitle))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(dest => dest.RentType, opt => opt.MapFrom(src => src.RentType))
				.ForMember(dest => dest.RentStatus, opt => opt.MapFrom(src => src.RentStatus))
				.ForMember(dest => dest.HasFurniture, opt => opt.MapFrom(src => src.HasFurniture))
				.ForMember(dest => dest.NotForStudents, opt => opt.MapFrom(src => src.NotForStudents))
				.ForMember(dest => dest.WithoutAnimals, opt => opt.MapFrom(src => src.WithoutAnimals))
				.ForMember(dest => dest.WithoutChildren, opt => opt.MapFrom(src => src.WithoutChildren))
				.ForMember(dest => dest.IsAgent, opt => opt.MapFrom(src => src.IsAgent))
				.ForMember(dest => dest.WithInternet, opt => opt.MapFrom(src => src.WithInternet))
				.ForMember(dest => dest.ForDay, opt => opt.MapFrom(src => src.ForDay))
				.ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Flat.Area))
				.ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Flat.RoomNumber))
				.ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Flat.Floor))
				.ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.Flat.Geolocation.HouseNumber))
				.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Flat.Geolocation.HouseNumber))
				.ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Flat.Geolocation.HouseNumber));
		}
	}
}
