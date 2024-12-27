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
    private readonly IDapperContext _dapper;

    public AddEmployeeCommandHandler(IEmployeeRepository EmployeeRepository, IMapper mapper, ILogger<AddEmployeeCommandHandler> logger, IDapperContext dapper)
    {
        _EmployeeRepository = EmployeeRepository;
        _mapper = mapper;
        _logger = logger;
        _dapper = dapper;
    }
    public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        //var obj = await _dapper.QueryAsync<dynamic>("SELECT * FROM Employees");
        var EmployeeEntity = _mapper.Map<Employee>(request);
        var generatedEmployee = await _EmployeeRepository.AddAsync(EmployeeEntity);
        _logger.LogInformation(($"Employee {generatedEmployee} successfully created."));
        return generatedEmployee.Id;
    }
}