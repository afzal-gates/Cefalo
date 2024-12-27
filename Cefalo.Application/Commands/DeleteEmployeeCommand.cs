using MediatR;

namespace Cefalo.Application.Commands;

public class DeleteEmployeeCommand : IRequest
{
    public int Id { get; set; }

}