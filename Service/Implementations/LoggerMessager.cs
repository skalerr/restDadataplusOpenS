using DAL;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Service.Implementations;

public class LoggerMessager : ILoggerMessager
{
    private readonly ILoggerRepository _loggerRepository;

    public LoggerMessager(ILoggerRepository loggerRepository)
    {
        _loggerRepository = loggerRepository;
    }

    
    public async Task AddLog(Log log)
    {
         _loggerRepository.Add(log);
         await _loggerRepository.SaveAll();
    }

}