using MediatR;
using Cefalo.Application.Responses;

namespace Cefalo.Application.Queries;

public class GetEmployeeQuery : IRequest<EmployeeResponse>
{
    public int Id {  get; set; }
}
