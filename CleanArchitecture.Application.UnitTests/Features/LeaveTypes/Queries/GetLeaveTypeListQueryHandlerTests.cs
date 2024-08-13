using AutoMapper;
using CleanArchitecture.Application.Contracts.Logging;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using CleanArchitecture.Application.MappingProfiles;
using CleanArchitecture.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;
    
    public GetLeaveTypeListQueryHandlerTests()
    {
        _mockLeaveTypeRepository = MockLeaveTypeRepository.GetLeaveTypes();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypesQueryHandler(_mapper, _mockLeaveTypeRepository.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}