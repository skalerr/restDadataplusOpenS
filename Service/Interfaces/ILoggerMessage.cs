using Domain.Entities;

namespace Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
public interface ILoggerMessage
{
    Task AddLog(Log log);
}