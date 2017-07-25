using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using MRM.Database.Model;
using System.Linq.Expressions;

namespace MRM.Database.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal MRMContext Context;
        public DbSet<TEntity> Table { get; set; }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = Table;
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        public GenericRepository(MRMContext context)
        {
            this.Context = context;
            this.Table = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {

            IQueryable<TEntity> query = Table;
            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return Table.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            Table.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = Table.Find(id);
            Delete(entityToDelete);
            Context.SaveChanges();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                Table.Attach(entityToDelete);
            }
            Table.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public virtual TEntity Update(TEntity entityToUpdate)
        {
            Table.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
            return entityToUpdate;
        }

        //generic method to get many record on the basis of a condition.
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return Table.Where(where).ToList();
        }

        // generic method to get many record on the basis of a condition but query able.

        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return Table.Where(where).AsQueryable();
        }

        //generic get method , fetches data for the entities on the basis of condition.
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return Table.Where(where).FirstOrDefault<TEntity>();
        }

        // generic delete method , deletes data for the entities on the basis of condition.
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = Table.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                Table.Remove(obj);
        }

        //fetch all the records
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Table.ToList();
        }

        //Inclue multiple :TODO: Might need some changes 
        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.Table;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public bool Exists(object primaryKey)
        {
            return Table.Find(primaryKey) != null;
        }

        // Fetch a single record by the specified criteria (usually the unique identifier)

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return Table.Single<TEntity>(predicate);
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return Table.First<TEntity>(predicate);
        }
    }
}
