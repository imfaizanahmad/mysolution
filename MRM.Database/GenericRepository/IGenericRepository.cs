using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using MRM.Database.Model;
using System.Linq.Expressions;

namespace MRM.Database.GenericRepository
{
   public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Table { get; set; }
        IEnumerable<TEntity> Get();
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        TEntity Update(TEntity entityToUpdate);
        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);
        IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where);
        TEntity Get(Func<TEntity, Boolean> where);
        void Delete(Func<TEntity, Boolean> where);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include);
        bool Exists(object primaryKey);
        TEntity GetSingle(Func<TEntity, bool> predicate);
        TEntity GetFirst(Func<TEntity, bool> predicate);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        

    }
}
