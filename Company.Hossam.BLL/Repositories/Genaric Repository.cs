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
        private readonly HossamCompanyDB _Context;
        public Genaric_Repository(HossamCompanyDB Context)
        {
            _Context = Context;
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employees))
            return (IEnumerable<T>) _Context.Employees.Include(e=>e.WorkFor).AsNoTracking().ToList();
            else
            {
                return _Context.Set<T>().AsNoTracking().ToList();

            }

        }


        public T? GetSpacificById(int? id)
        {
            return _Context.Set<T>().FirstOrDefault(d => d.Id == id);
        }

        public int Add(T entity)
        {
            _Context.Set<T>().Add(entity);
            return _Context.SaveChanges();
        }

        public int Update(T entity)
        {
            _Context.Set<T>().Update(entity);
            return _Context.SaveChanges();
        }
        public int Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return _Context.SaveChanges();
        }

        public int DeletebYiD(int? id)
        {
            var result = _Context.Set<T>().FirstOrDefault(d => d.Id == id);
            _Context.Set<T>().Remove(result);
            return _Context.SaveChanges();
        }
    }
}
