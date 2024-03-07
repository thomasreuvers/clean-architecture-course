using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}