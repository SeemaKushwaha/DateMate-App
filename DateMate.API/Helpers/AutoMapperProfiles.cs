using AutoMapper;
using DateMate.API.Models;
using DateMate.API.Dtos;
using System.Linq;
namespace DateMate.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            .ForMember( dest => dest.PhotoUrl, opt => opt.MapFrom( src =>
            src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember( dest => dest.Age, opt => opt.MapFrom( src =>
            src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailsDto>()
            .ForMember( dest => dest.PhotoUrl, opt => opt.MapFrom( src =>
            src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember( dest => dest.Age, opt => opt.MapFrom( src =>
            src.DateOfBirth.CalculateAge()));
            
            CreateMap<Photo, PhotosForDetailsDto>();

            CreateMap<UserForUpdateDto, User>();

            CreateMap<Photo, PhotoForReturnDto>();

            CreateMap<PhotoForCreationDto, Photo>();

            CreateMap<UserForRegisterDto,User>();
        }
    }
}