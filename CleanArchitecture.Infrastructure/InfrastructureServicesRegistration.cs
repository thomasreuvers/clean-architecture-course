using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Logging;
using CleanArchitecture.Application.Models.Email;
using CleanArchitecture.Infrastructure.EmailService;
using CleanArchitecture.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        
        return services;
    }
}