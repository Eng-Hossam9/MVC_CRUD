using Company.Hossam.BLL.InterFaces;
using Company.Hossam.DAL.Data.Contexts;
using Company.Hossam.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.BLL.Repositories
{
    public class EmployeeRepository : Genaric_Repository<Employees>,IEmployeeRepository
    {

        public EmployeeRepository(HossamCompanyDB context) :base(context)
        {
        }



        public IEnumerable<Employees> GetAllWithDepartment()
        {
            return _Context.Employees.Include(e=>e.WorkFor).ToList();

        }

        public IEnumerable<Employees> SearchByName(string Name)
        {
            return _Context.Employees.Where(e=>e.Name.ToLower().Contains(Name.ToLower())).Include(e=>e.WorkFor).AsNoTracking().ToList();
        }
    }
}
