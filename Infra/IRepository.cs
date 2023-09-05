using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyDockerApi.Infra;

public interface IRepository<T>
    {
        Task<T> Save(T entity);
        Task Delete(T entity);
        Task<ICollection<T>> All();
        Task<ICollection<T>> Find(Expression<Func<T, bool>> expression);
        Task<T> FindById(long id);
        Task<IDbContextTransaction> BeginTransaction();
}