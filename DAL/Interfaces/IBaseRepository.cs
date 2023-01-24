namespace DAL.Interfaces;

public interface IBaseRepository
{
    Task Add<T>(T entity) where T : class;
    Task<bool> SaveAll();
}