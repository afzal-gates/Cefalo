using MediatR;
using Cefalo.Application.Responses;

namespace Cefalo.Application.Queries;

public class GetEmployeeListQuery : IRequest<List<EmployeeResponse>>
{
    //public int Id { get; set; }
}