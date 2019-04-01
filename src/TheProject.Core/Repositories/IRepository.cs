namespace TheProject.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public interface IRepository
    {

    }

    public interface IRepository<TContext, TEntity> : IRepository<TContext, TEntity,int> where TEntity : class, IEntity<int> 
    {

    }

    public interface IRepository<TContext, TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(TPrimaryKey id);
        Task<List<TEntity>> List();
        Task<TEntity> Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(TPrimaryKey id);
    }
}
