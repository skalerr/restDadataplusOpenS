using System.Configuration;
using DAL;
using DAL.Repositories;
using Domain.Entities;
using Domain.Enum;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using Service.Implementations;
using WebApplication4.Controllers;

namespace Tests;
public class Tests
{
    private DbContextOptions<AppDbContext> options;
    private AppDbContext context;
    private LoggerRepository loggerRepository;
    private LoggerMessager _loggerMessager;
    private OpenStreetMapService OpenStreetMapService;
    private DadataService dadata;
    private RestController controller;
    private IOptions<DaDataConfig> _config;

    [SetUp]
    public void Setup()
    {
        options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        context = new AppDbContext(options);
        loggerRepository = new LoggerRepository(context);
        _loggerMessager = new LoggerMessager(loggerRepository);
        OpenStreetMapService = new OpenStreetMapService(new HttpClient(), _loggerMessager);
        dadata = new DadataService(_loggerMessager, Options.Create(new DaDataConfig()
        {
            Secret = "5fdd415eaa72b3aa07b2d219c5e2ca5eda3a85bc",
            Token = "1f5d452b294554d406b007d4e6e2e182b5ae1d8c"
        }));
        
        controller = new RestController(dadata, OpenStreetMapService, _loggerMessager);
    }
    
    [Test]
    public void GetAddress()
    {
        // Arrange

        // Act
        
        var rsp = dadata.GetAddress(new PositionModel()
        {
            Geo = "54.7139938 55.971772",
            Lat = 54.7139938,
            Lon = 55.971772,
        }).GetAwaiter().GetResult();
        
        // Assert
        Assert.True(rsp.Data.Locations.Count == 10);
        Assert.True(rsp.Data.Locations.Count > 0);
    }
    
    [Test]
    public void GetGeo()
    {
        // Arrange

        // Act
 
        var rsp = dadata.GetGeo("Россия", "Бехтерева 16", "Уфа").GetAwaiter().GetResult();
        // Assert

        Assert.NotNull(rsp.Data.Lat);
        Assert.NotNull(rsp.Data.Lon);
        Assert.True(rsp.Data.Lat > 54);
        Assert.True(rsp.Data.Lon > 55);

    }
    
    [Test]
    public void GetGeoController()
    {
        // Arrange
    
        // Act
        var rsp = controller.Get("Россия", "Бехтерева 16", "Уфа").GetAwaiter().GetResult();
        // Assert
        Assert.True(rsp is OkObjectResult);
    }
    
    [Test]
    public void SaveLog()
    {
        // Arrange
        var log = new Log()
        {
            Id = 1,
            Message = "Test",
            Date = DateTime.Now.ToString()
        };
        // Act
        _loggerMessager.AddLog(log).GetAwaiter().GetResult();
        // Assert
        Assert.True(context.Logs.Count() == 1);
    }
    
    //написать тест для проверки сервиса OpenStreetMapService 
    [Test]
    public void OpenMapService()
    {        
        // Arrange
        // Act
        var rsp = OpenStreetMapService.GetAddress("Россия", "Бехтерева 16", "Уфа").GetAwaiter().GetResult();
        // Assert
        Assert.NotNull(rsp.Data);
        Assert.True(rsp.StatusCode == StatusCode.Ok);
        Assert.True(rsp.IsSuccess);
    }
    
    
}