using System;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using Entity;

namespace Service
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly EntityDbContext _context;
        private IDbSet<T> _entity;

        public Repository(EntityDbContext entityDbContext)
        {
            _context = entityDbContext;
        }

        public T GetById(object id)
        {
            return this._entity.Find(id);
        }

        public void Insert(T entity)
        {
            EntityNullCheck(entity);

            _entity.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            EntityNullCheck(entity);

            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            EntityNullCheck(entity);

            _entity.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> Table => _entity;

        private IDbSet<T> Entities => _entity ?? (_entity = _context.Set<T>());

        private static void EntityNullCheck(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
