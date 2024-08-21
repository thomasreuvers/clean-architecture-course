using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Services;

public class UserService(
    UserManager<ApplicationUser> userManager
    ) : IUserService
{
    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(employee => new Employee
        {
            Email = employee.Email,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        }).ToList();
    }

    public async Task<Employee> GetEmployee(string id)
    {
        var employee = await userManager.FindByIdAsync(id);
        return new Employee
        {
            Email = employee.Email,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }
}