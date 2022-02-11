using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRestChallenge.Abstractions
{

        public interface ICrudAsync<T>
        {
            Task<T> SaveAsync(T entity);
            Task<IList<T>> GetAllAsync();
            Task<T> GetByIdAsync(int id);
            Task<T> GetByMailAsync(string mail);
            void DeleteAsync(int id);
        }

        public interface ICrud<T> : ICrudAsync<T>
        {
            T Save(T entity);
            IList<T> GetAll();
            T GetById(int id);
            void Delete(int id);

        }
    
}
