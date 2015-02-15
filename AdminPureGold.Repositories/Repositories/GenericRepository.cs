using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AdminPureGold.Repositories.EF.Helpers;
using AdminPureGold.Repositories.Interfaces;

namespace AdminPureGold.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal DbContext Context;
        internal DbSet<T> DbSet;

        public GenericRepository(DbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(T entity) 
        {
            // All Elements State = Added
            // Insert for each element, even if pre-existing in DB            
            DbSet.Add(entity);                     
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {            
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            // Tell Context to be aware of the object and any related object
            // All elements State = Unchanged
            // Watch out, missing foreign keys will throw an error
            DbSet.Attach(entityToUpdate);
            Context.ApplyStateChanges();            
        }      
    }
}
