
using System.Linq.Expressions;

namespace Ecom.Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    //---------------------- Create ----------------------
    Task AddAsync(T entity);

    //---------------------- Read ----------------------
    Task<IReadOnlyList<T>>  GetAllAsync();
    //T is input , object is output
    Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdAsync(int id , params Expression<Func<T, object>>[] includes);


    //---------------------- Update ----------------------
    Task UpdateAsync(T entity);


    //---------------------- Delete ----------------------
    Task DeleteAsync(int id);



}
