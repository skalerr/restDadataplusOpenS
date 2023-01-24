using DAL.Interfaces;
using Domain.Entities;

namespace DAL.Repositories;

public class LoggerRepository : ILoggerRepository
{
    private readonly AppDbContext _context;

    public LoggerRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task Add<T>(T entity) where T : class
    {
        _context.Add(entity);
        return Task.CompletedTask;
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public async Task<bool> SaveAll()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}