using DAL;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Service.Implementations;

public class LoggerMessage : ILoggerMessage
{
    private readonly ILoggerRepository _loggerRepository;

    public LoggerMessage(ILoggerRepository loggerRepository)
    {
        _loggerRepository = loggerRepository;
    }

    
    public async Task AddLog(Log log)
    {
         _loggerRepository.Add(log);
         await _loggerRepository.SaveAll();
    }

}