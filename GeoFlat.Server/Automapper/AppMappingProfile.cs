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

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Account.Role));

            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<UserRequest, Account>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));


            CreateMap<Favorite, FavoriteResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Record.Id))
                .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.Record.PublicationDate))
                .ForMember(dest => dest.PicturesPath, opt => opt.MapFrom(src => src.Record.PicturesPath))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Record.Description))
                .ForMember(dest => dest.RecordTitle, opt => opt.MapFrom(src => src.Record.RecordTitle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Record.Price))
                .ForMember(dest => dest.RentType, opt => opt.MapFrom(src => src.Record.RentType))
                .ForMember(dest => dest.RentStatus, opt => opt.MapFrom(src => src.Record.RentStatus))
                .ForMember(dest => dest.HasFurniture, opt => opt.MapFrom(src => src.Record.HasFurniture))
                .ForMember(dest => dest.NotForStudents, opt => opt.MapFrom(src => src.Record.NotForStudents))
                .ForMember(dest => dest.WithoutAnimals, opt => opt.MapFrom(src => src.Record.WithoutAnimals))
                .ForMember(dest => dest.WithoutChildren, opt => opt.MapFrom(src => src.Record.WithoutChildren))
                .ForMember(dest => dest.IsAgent, opt => opt.MapFrom(src => src.Record.IsAgent))
                .ForMember(dest => dest.WithInternet, opt => opt.MapFrom(src => src.Record.WithInternet))
                .ForMember(dest => dest.ForDay, opt => opt.MapFrom(src => src.Record.ForDay))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Record.Flat.Area))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Record.Flat.RoomNumber))
                .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Record.Flat.Floor))
                .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.Record.Flat.Geolocation.HouseNumber))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Record.Flat.Geolocation.HouseNumber))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Record.Flat.Geolocation.HouseNumber));

            CreateMap<Comparison, ComparisonResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Record.Id))
                .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.Record.PublicationDate))
                .ForMember(dest => dest.PicturesPath, opt => opt.MapFrom(src => src.Record.PicturesPath))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Record.Description))
                .ForMember(dest => dest.RecordTitle, opt => opt.MapFrom(src => src.Record.RecordTitle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Record.Price))
                .ForMember(dest => dest.RentType, opt => opt.MapFrom(src => src.Record.RentType))
                .ForMember(dest => dest.RentStatus, opt => opt.MapFrom(src => src.Record.RentStatus))
                .ForMember(dest => dest.HasFurniture, opt => opt.MapFrom(src => src.Record.HasFurniture))
                .ForMember(dest => dest.NotForStudents, opt => opt.MapFrom(src => src.Record.NotForStudents))
                .ForMember(dest => dest.WithoutAnimals, opt => opt.MapFrom(src => src.Record.WithoutAnimals))
                .ForMember(dest => dest.WithoutChildren, opt => opt.MapFrom(src => src.Record.WithoutChildren))
                .ForMember(dest => dest.IsAgent, opt => opt.MapFrom(src => src.Record.IsAgent))
                .ForMember(dest => dest.WithInternet, opt => opt.MapFrom(src => src.Record.WithInternet))
                .ForMember(dest => dest.ForDay, opt => opt.MapFrom(src => src.Record.ForDay))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Record.Flat.Area))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Record.Flat.RoomNumber))
                .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Record.Flat.Floor))
                .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.Record.Flat.Geolocation.HouseNumber))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Record.Flat.Geolocation.HouseNumber))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Record.Flat.Geolocation.HouseNumber));

            CreateMap<Message, MessageResponse>()
                .ForMember(dest => dest.messageId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.MessageText))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => src.IsRead))
                .ForMember(dest => dest.SendingDate, opt => opt.MapFrom(src => src.SendingDate))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender))
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.UserSender.Name))
                .ForMember(dest => dest.SenderSurname, opt => opt.MapFrom(src => src.UserSender.Surname))
                .ForMember(dest => dest.SenderPhoneNumber, opt => opt.MapFrom(src => src.UserSender.PhoneNumber))
                .ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.UserSender.Account.Email))
                .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.Recipient))
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(src => src.UserRecipient.Name))
                .ForMember(dest => dest.RecipientSurname, opt => opt.MapFrom(src => src.UserRecipient.Surname))
                .ForMember(dest => dest.RecipientPhoneNumber, opt => opt.MapFrom(src => src.UserRecipient.PhoneNumber))
                .ForMember(dest => dest.RecipientEmail, opt => opt.MapFrom(src => src.UserRecipient.Account.Email));
        }
    }
}
