using AutoMapper;
using Company.Hossam.DAL.Model;
using Company.Hossam.PL.ViewModels;

namespace Company.Hossam.PL.Mapping
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Departments,DepartmentViewModel>();
            CreateMap<DepartmentViewModel,Departments>();

        }
    }
}
