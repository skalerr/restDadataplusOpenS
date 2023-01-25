using Domain.Entities;

namespace Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
public interface ILoggerMessager
{
    Task AddLog(Log log);
}