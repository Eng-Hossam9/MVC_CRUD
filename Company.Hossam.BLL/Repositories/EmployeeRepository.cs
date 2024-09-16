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
        private readonly HossamCompanyDB _context;

        public EmployeeRepository(HossamCompanyDB context) :base(context)
        {
        }



        public IEnumerable<Employees> GetAllWithDepartment()
        {
            return _context.Employees.Include(e=>e.WorkFor).ToList();

        }


    }
}
