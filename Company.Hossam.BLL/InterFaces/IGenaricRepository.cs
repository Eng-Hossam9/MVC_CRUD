using Company.Hossam.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.BLL.InterFaces
{
    public interface IGenaricRepository<T>
    {
        IEnumerable<T> GetAll();

        T? GetSpacificById(int? id);

        int Add(T department);
        int Update(T department);
        int Delete(T department);
        int DeletebYiD(int? id);
    }
}
