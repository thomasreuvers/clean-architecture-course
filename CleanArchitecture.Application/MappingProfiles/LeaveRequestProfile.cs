using AutoMapper;
using CleanArchitecture.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using CleanArchitecture.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestDetailsDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
}