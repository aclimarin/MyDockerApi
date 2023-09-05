using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyDockerApi.Infra;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    #region SAVE
    public async Task<T> Save(T entity)
    {
        try
        {
            Type type = typeof(T);
            long id = Convert.ToInt64(type.GetProperty("Id").GetValue(entity));

            if (id == 0)
            {
                return await Create(entity);
            }
            else
            {
                return await Update(entity);
            }
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Erro ao salvar registro", dbEx);
        }

    }

    private async Task<T> Create(T entity)
    {
        var newEntity = this._dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return newEntity.Entity;
    }

    private async Task<T> Update(T entity)
    {
        //DetachLocal();

        var newEntity = this._dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return newEntity.Entity;
    }
    #endregion

    public async Task Delete(T entity)
    {
        this._dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }


    #region SEARCH
    public async Task<ICollection<T>> All()
    {
        var response = _dbContext.Set<T>();

        return await response.ToListAsync();
    }

    public async Task<ICollection<T>> Find(Expression<Func<T, bool>> expression)
    {
        var response = this._dbContext.Set<T>().Where(expression);
        return await response.ToListAsync();
    }

    public async Task<T> FindById(long id)
    {
        var reponse = await _dbContext.Set<T>().FindAsync(id);
        return reponse;

    }
    #endregion
    
    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

}