using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypes()
    {
        var leaveTypes = new List<LeaveType>
        {
            new()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new()
            {
                Id = 2,
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new()
            {
                Id = 3,
                DefaultDays = 15,
                Name = "Test Maternity"
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });

        return mockRepo;
    }
}