using AutoMapper;
using MediatR;
using Cefalo.Application.Queries;
using Cefalo.Application.Responses;
using Cefalo.Core.Repositories;

namespace Cefalo.Application.Handlers;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id);
        return _mapper.Map<EmployeeResponse>(employee);
    }
}