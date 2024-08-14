using AutoMapper;
using CleanArchitecture.BlazorUI.Models.LeaveTypes;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
    }
}