using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using MRM.Database.Model;

namespace MRM.Database.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal MRMContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(MRMContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {

            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
            Context.SaveChanges();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
        }

        //generic method to get many record on the basis of a condition.
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        // generic method to get many record on the basis of a condition but query able.

        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }

        //generic get method , fetches data for the entities on the basis of condition.
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        // generic delete method , deletes data for the entities on the basis of condition.
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }

        //fetch all the records
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        //Inclue multiple :TODO: Might need some changes 
        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        // Fetch a single record by the specified criteria (usually the unique identifier)

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.Single<TEntity>(predicate);
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }
    }
}
