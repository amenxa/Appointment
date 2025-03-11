using Apoint_pro.Data.DTOS;
using Apoint_pro.Data.models;
using AutoMapper;
namespace Apoint_pro.cofig
{
    public class AutoMapperConfig :Profile
    {
          public AutoMapperConfig() 
          {
            CreateMap<Apointment, ApointmentDTO>().ReverseMap();
            CreateMap<LoginDto, User>();
            CreateMap<User, UserDTO>();
          
        }
    }
}
