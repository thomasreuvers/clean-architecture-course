using CleanArchitecture.Domain;
using CleanArchitecture.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace CleanArchitecture.Persistence.IntegrationTests;

public class CaDatabaseContextTests
{
    private readonly CaDatabaseContext _caDatabaseContext;
    
    public CaDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<CaDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        
        _caDatabaseContext = new CaDatabaseContext(dbOptions);
    }
    
    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };
        
        // Act
        await _caDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _caDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateCreated.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };
        
        // Act
        await _caDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _caDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateModified.ShouldNotBeNull();
    }
}