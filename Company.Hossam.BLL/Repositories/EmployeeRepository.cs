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



        public async Task<IEnumerable<Employees>> GetAllWithDepartmentAsync()
        {
            return await _Context.Employees.Include(e=>e.WorkFor).ToListAsync();

        }

        public async Task<IEnumerable<Employees>> SearchByNameAsync(string Name)
        {
            return await _Context.Employees.Where(e=>e.Name.ToLower().Contains(Name.ToLower())).Include(e=>e.WorkFor).AsNoTracking().ToListAsync();
        }
    }
}
