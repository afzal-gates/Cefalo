using AutoMapper;
using Cefalo.Application.Commands;
using Cefalo.Application.Responses;
using Cefalo.Core.Entities;

namespace Cefalo.Application.Mappers;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Employee, EmployeeResponse>().ReverseMap();
        CreateMap<Employee, AddEmployeeCommand>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
    }
}