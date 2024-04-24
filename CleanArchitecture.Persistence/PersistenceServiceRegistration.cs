using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Persistence.DatabaseContext;
using CleanArchitecture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CaDatabaseContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        
        return services;
    }
}