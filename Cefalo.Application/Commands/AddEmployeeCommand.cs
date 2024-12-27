using Cefalo.Core.Enums;
using MediatR;

namespace Cefalo.Application.Commands;

public class AddEmployeeCommand : IRequest<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBrith { get; set; }
    public Gender Gender { get; set; }
    public int DepartmentId { get; set; }
    public string PhotoPath { get; set; }
}