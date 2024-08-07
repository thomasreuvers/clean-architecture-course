using AutoMapper;
using CleanArchitecture.Application.Features.LeaveAllocation.Commands;
using CleanArchitecture.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using CleanArchitecture.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using CleanArchitecture.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using CleanArchitecture.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}