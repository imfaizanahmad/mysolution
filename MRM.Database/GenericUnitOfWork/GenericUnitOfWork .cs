using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRM.Database.Model;
using MRM.Database.GenericRepository;

namespace MRM.Database.GenericUnitOfWork
{
   public class GenericUnitOfWork : IDisposable
    {
        private MRMContext _context = null;

        public GenericUnitOfWork()
        {
            _context = new MRMContext();
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }
            IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(_context);
            repositories.Add(typeof(TEntity), repo);
            return repo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
