

using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Entity;

namespace Service
{
    public class UnitOfWork : IDisposable
    {
        public readonly EntityDbContext _context;
        private bool disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(EntityDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (Repository<T>)_repositories[type];
        }
    }
}
