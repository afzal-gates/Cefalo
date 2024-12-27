using MediatR;
using Microsoft.Extensions.Logging;
using Cefalo.Application.Commands;
using Cefalo.Application.Exceptions;
using Cefalo.Core.Entities;
using Cefalo.Core.Repositories;

namespace Cefalo.Application.Handlers;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployeeRepository _EmployeeRepository;
    private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

    public DeleteEmployeeCommandHandler(IEmployeeRepository EmployeeRepository, ILogger<DeleteEmployeeCommandHandler> logger)
    {
        _EmployeeRepository = EmployeeRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var EmployeeToDelete = await _EmployeeRepository.GetByIdAsync(request.Id);
        if (EmployeeToDelete == null)
        {
            throw new EmployeeNotFoundException(nameof(Employee), request.Id);
        }

        await _EmployeeRepository.DeleteAsync(EmployeeToDelete);
        _logger.LogInformation($"Employee with Id {request.Id} is deleted successfully.");
        return Unit.Value;
    }
}