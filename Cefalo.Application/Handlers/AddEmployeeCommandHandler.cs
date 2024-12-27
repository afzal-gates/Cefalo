using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Cefalo.Application.Commands;
using Cefalo.Core.Entities;
using Cefalo.Core.Repositories;

namespace Cefalo.Application.Handlers;

public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, int>
{
    private readonly IEmployeeRepository _EmployeeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddEmployeeCommandHandler> _logger;

    public AddEmployeeCommandHandler(IEmployeeRepository EmployeeRepository, IMapper mapper, ILogger<AddEmployeeCommandHandler> logger)
    {
        _EmployeeRepository = EmployeeRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var EmployeeEntity = _mapper.Map<Employee>(request);
        var generatedEmployee = await _EmployeeRepository.AddAsync(EmployeeEntity);
        _logger.LogInformation(($"Employee {generatedEmployee} successfully created."));
        return generatedEmployee.Id;
    }
}