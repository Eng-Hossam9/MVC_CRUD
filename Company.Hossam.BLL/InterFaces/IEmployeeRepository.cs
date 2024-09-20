using Company.Hossam.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.BLL.InterFaces
{
    public interface IEmployeeRepository:IGenaricRepository<Employees>
    {
        Task<IEnumerable<Employees>> SearchByNameAsync(string Name);   

    }
}
