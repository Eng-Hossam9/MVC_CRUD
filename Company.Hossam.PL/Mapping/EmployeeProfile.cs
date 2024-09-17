using AutoMapper;
using Company.Hossam.DAL.Model;
using Company.Hossam.PL.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.Hossam.PL.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employees,EmployeeViewModel>();
            CreateMap<EmployeeViewModel,Employees>();
        }
    }
}
