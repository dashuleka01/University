using System.Collections.Generic;

namespace DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void New(TEntity entity);
        void Edit(TEntity entity);
        bool Delete(int id);
    }
}
