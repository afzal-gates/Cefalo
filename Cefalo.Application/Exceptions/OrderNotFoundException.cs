namespace Cefalo.Application.Exceptions;

public class EmployeeNotFoundException : ApplicationException
{
    public EmployeeNotFoundException(string name, object key) : base($"Entity {name} - {key} is not found.")
    {
        
    }
}