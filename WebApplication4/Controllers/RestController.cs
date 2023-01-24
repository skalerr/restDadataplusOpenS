using System.Net;
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

    public RestController(IDadataService dadataService, IOpenStreetMapService openStreetMapService)
    {
        _dadataService = dadataService;
        _openStreetMapService = openStreetMapService;
    }
    // GET: api/Rest
    [HttpGet]
    // public IEnumerable<string> Get()
    // {
    //     return new string[] { "value1", "value2" };
    // }

    // GET: api/Rest/5
    [HttpGet("{name}", Name = "Get")]
    public async Task<IActionResult> Get(string country, string city, string street)
    {
       var geo = await _openStreetMapService.GetAddress(country, street, city);
       if (geo.IsSuccess)
       {
           var locations = await _dadataService.GetAddress(geo.Data);
           if (locations != null)
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