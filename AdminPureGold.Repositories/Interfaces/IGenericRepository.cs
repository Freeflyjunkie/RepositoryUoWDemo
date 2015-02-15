﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdminPureGold.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T GetById(object id);
        void Insert(T entity);
        void Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);        
    }
}
