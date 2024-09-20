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
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetSpacificByIdAsync(int? id);

        Task<int> AddAsync(T department);
        Task<int> UpdateAsync(T department);
        Task<int> DeleteAsync(T department);
        Task<int> DeletebYiDAsync(int? id);
    }
}
