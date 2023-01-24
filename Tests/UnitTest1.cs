using System.Configuration;
using DAL;
using DAL.Repositories;
using Domain.Entities;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Implementations;
using WebApplication4.Controllers;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }
    
    [Test]
    public void IndexViewDataMessage()
    {
        // Arrange
        
 
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        
        var context = new AppDbContext(options);
        
        var loggerRepository = new LoggerRepository(context);
        var loggerMessage = new LoggerMessage(loggerRepository);
        var OpenStreetMapService = new OpenStreetMapService(new HttpClient(), loggerMessage);
        var dadata = new DadataService(loggerMessage, Options.Create(new DaDataConfig()));
        var controller = new RestController(dadata, OpenStreetMapService, loggerMessage);
        // Act
 
        var rsp = controller.Get("Россия", "Москва", "Ленина").GetAwaiter();
        // Assert
        
        var result = new BaseResponse<PositionModel>();
        Assert.Equals(rsp, result);
    }

    
    [Test]
    public void Test1()
    {
        
    }
}