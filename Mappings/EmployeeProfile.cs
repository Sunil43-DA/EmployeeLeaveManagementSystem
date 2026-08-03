using AutoMapper;
using EmployeeLeaveManagement.API.DTOs;
using EmployeeLeaveManagement.API.Models;

namespace EmployeeLeaveManagement.API.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Entity -> DTO
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Department,
                    opt => opt.MapFrom(src => src.Department.DepartmentName));

            // DTO -> Entity
            CreateMap<CreateEmployeeDto, Employee>();

            // DTO -> Entity (Update)
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}