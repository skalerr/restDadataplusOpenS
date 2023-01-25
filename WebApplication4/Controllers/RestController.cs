using System.Net;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;

namespace WebApplication4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestController : ControllerBase
{
    private readonly IDadataService _dadataService;
    private readonly IOpenStreetMapService _openStreetMapService;
    private readonly ILoggerMessager _logger;

    public RestController(IDadataService dadataService, IOpenStreetMapService openStreetMapService, ILoggerMessager logger)
    {
        _dadataService = dadataService;
        _openStreetMapService = openStreetMapService;
        _logger = logger;
    }
    // GET: api/Rest
    [HttpGet]
    public IActionResult Get()
    {
        return StatusCode((int) HttpStatusCode.MethodNotAllowed);
    }

    // GET: api/Rest/5
    [HttpGet("{name}", Name = "Get")]
    public async Task<IActionResult> Get(string country, string city, string street)
    {
        await _logger.AddLog(new Log
        {
            Message = "get ",
            StackTrace = null,
            InnerException = null,
            Source = null,
            TargetSite = null,
            Data = $"{country} {city} {street}",
            HelpLink = null,
            HResult = null,
            Date = DateTime.Now.ToString(),
        //todo: реализовать конструктор
        });
        if (country == null || city == null || street == null) 
        {
            return NotFound();
        }

        var geo = await _dadataService.GetGeo(country, street, city);

        if (geo.IsSuccess)
        {
            var locations = await _dadataService.GetAddress(geo.Data);
            if (locations.IsSuccess)
            {
                return Ok(locations);
            } else
            {
                return NotFound(locations?.Descripton);
            }
        }
        else
        {
            return NotFound(geo.Descripton);
        }
    }

    // POST: api/Rest
    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        return StatusCode((int) HttpStatusCode.MethodNotAllowed);
    }

    // PUT: api/RestApi/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        return StatusCode((int) HttpStatusCode.MethodNotAllowed);
 
    }

    // DELETE: api/Rest/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return StatusCode((int) HttpStatusCode.MethodNotAllowed);

    }
}