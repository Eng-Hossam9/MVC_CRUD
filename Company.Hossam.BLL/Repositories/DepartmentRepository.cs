using Company.Hossam.BLL.InterFaces;
using Company.Hossam.DAL.Data.Contexts;
using Company.Hossam.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.BLL.Repositories
{
    public class DepartmentRepository : Genaric_Repository<Departments>,IDepartmentRepository
    {
        private readonly HossamCompanyDB _Context;

        public DepartmentRepository (HossamCompanyDB Context):base(Context)
        {
           
        }
       

    }
}
