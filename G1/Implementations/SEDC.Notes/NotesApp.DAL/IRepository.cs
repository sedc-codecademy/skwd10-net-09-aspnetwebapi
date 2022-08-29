using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetById(int id, int userId);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
