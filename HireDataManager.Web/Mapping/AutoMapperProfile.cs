using AutoMapper;
using HireDataManager.Models.Dto;
using HireDataManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireDataManager.Web.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, Country>();

        CreateMap<DepartmentDto, Department>();
        CreateMap<Department, DepartmentDto>();

        CreateMap<DependentDto, Dependent>();
        CreateMap<Dependent, DependentDto>();

        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();

        CreateMap<Job, JobDto>();
        CreateMap<JobDto, Job>();

        CreateMap<LocationDto, Location>();
        CreateMap<Location, LocationDto>();

        CreateMap<Region, RegionDto>();
        CreateMap<RegionDto, Region>();
        
    }
}
