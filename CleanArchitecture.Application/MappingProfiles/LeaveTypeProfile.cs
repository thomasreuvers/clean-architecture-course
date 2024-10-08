using AutoMapper;
using CleanArchitecture.Application.Features.LeaveType.Commands.CreateLeaveType;
using CleanArchitecture.Application.Features.LeaveType.Commands.UpdateLeaveType;
using CleanArchitecture.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using CleanArchitecture.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
}