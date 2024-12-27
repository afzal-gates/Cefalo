using AutoMapper;
using MediatR;
using Cefalo.Application.Queries;
using Cefalo.Application.Responses;
using Cefalo.Core.Repositories;

namespace Cefalo.Application.Handlers;

public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeListQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<List<EmployeeResponse>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _employeeRepository.GetAllAsync();
        return _mapper.Map<List<EmployeeResponse>>(orderList);
    }
}