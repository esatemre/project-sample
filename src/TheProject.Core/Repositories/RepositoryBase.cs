namespace TheProject.Core
{
    using Entities;
    using Repositories;
    using Exceptions;
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RepositoryBase<TContext, TEntity> : RepositoryBase<TContext, TEntity, int>, IRepository<TContext, TEntity> where TEntity : class, IEntity<int> where TContext : DbContext
    {
        public RepositoryBase(TContext dbContext) : base(dbContext) { }
    }

    public class RepositoryBase<TContext, TEntity, TPrimaryKey> : IRepository<TContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey> where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public RepositoryBase(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> GetById(TPrimaryKey id)
        {
            var entity = await FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            return entity;
        }

        public async Task<List<TEntity>> List()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TPrimaryKey id)
        {
            var entity = await FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            _dbContext.Set<TEntity>().Remove(entity);
           await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> FirstOrDefault(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            Expression<Func<object>> closure = () => id;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
