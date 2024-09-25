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
    public class Genaric_Repository<T> : IGenaricRepository<T> where  T :BaseEntity
    {
        protected readonly HossamCompanyDB _Context;
        public Genaric_Repository(HossamCompanyDB Context)
        {
            _Context = Context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employees))

            return (IEnumerable<T>) await _Context.Employees.Include(e=>e.WorkFor).AsNoTracking().ToListAsync(); 
            else
            {
                return await _Context.Set<T>().AsNoTracking().ToListAsync();

            }

        }


        public async Task<T?> GetSpacificByIdAsync(int? id)
        {
            return await _Context.Set<T>().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> AddAsync(T entity)
        {
           await  _Context.Set<T>().AddAsync(entity);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _Context.Set<T>().Update(entity);
            return await _Context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> DeletebYiDAsync(int? id)
        {
            var result = await _Context.Set<T>().FirstOrDefaultAsync(d => d.Id == id);
            _Context.Set<T>().Remove(result);
            return await _Context.SaveChangesAsync();
        }
    }
}
