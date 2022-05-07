using CRUD.Context;
using CRUD.Models;
using AutoMapper;
using System.Linq;

namespace CRUD
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<EvenTask, ToDoTaskViewModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(m => m.TaskId, opt => opt.MapFrom(src => src.EvenTaskId))
                .ForMember(m => m.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(m => m.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(m => m.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(m => m.IsEdited, opt => opt.MapFrom(src => src.EvenTaskHistory.Any()))
                .ReverseMap();
            CreateMap<OddTask, ToDoTaskViewModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(m => m.TaskId, opt => opt.MapFrom(src => src.OddTaskId))
                .ForMember(m => m.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(m => m.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(m => m.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(m => m.IsEdited, opt => opt.MapFrom(src => src.OddTaskHistory.Any()))
                .ReverseMap();
            CreateMap<EvenTask, TaskHistory>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.TaskId, opt => opt.MapFrom(src => src.EvenTaskId))
                .ForMember(m => m.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(m => m.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ReverseMap();
            CreateMap<OddTask, TaskHistory>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.TaskId, opt => opt.MapFrom(src => src.OddTaskId))
                .ForMember(m => m.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(m => m.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ReverseMap();
        }
    }
}
