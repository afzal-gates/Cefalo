using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Cefalo.Application.Commands;
using Cefalo.Application.Exceptions;
using Cefalo.Core.Entities;
using Cefalo.Core.Repositories;

namespace Cefalo.Application.Handlers;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<UpdateEmployeeCommandHandler> logger)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var EmployeeToUpdate = await _employeeRepository.GetByIdAsync(request.Id);
        if (EmployeeToUpdate == null)
        {
            throw new EmployeeNotFoundException(nameof(Employee), request.Id);
        }

        _mapper.Map(request, EmployeeToUpdate, typeof(UpdateEmployeeCommand), typeof(Employee));
        await _employeeRepository.UpdateAsync(EmployeeToUpdate);
        _logger.LogInformation($"Employee {EmployeeToUpdate.Id} is successfully updated");
        return Unit.Value;
    }
}