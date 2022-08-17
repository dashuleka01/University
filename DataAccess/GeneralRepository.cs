using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DataAccess
{
    public class GeneralRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly UniContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;

        public GeneralRepository(UniContext context, ILogger<GeneralRepository<TEntity>> logger)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _logger = logger;
        }

        public bool Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            if (entityToDelete == null) return false;
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
            _logger.LogInformation($"{entityToDelete.GetType().Name} with index {id} was deleted");
            return _dbSet.Find(id) == null;
        }

        public void Edit(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"{entity.GetType().Name} was edited");
        }

        public TEntity Get(int id)
        {
            _logger.LogInformation($"{nameof(Get)} for {typeof(TEntity)} with index {id}");
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            _logger.LogInformation($"{nameof(GetAll)} for {typeof(TEntity)}");
            return _context.Set<TEntity>();
        }

        public void New(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            _logger.LogInformation($"{entity.GetType().Name} is created");
        }
    }
}
