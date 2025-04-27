using Microsoft.EntityFrameworkCore;
using TestLasmartV2.Data;
using TestLasmartV2.Entities;
using TestLasmartV2.Repositories;
using TestLasmartV2.Services.PointService;
using Point = TestLasmartV2.Entities.Point;

namespace TestLasmartV2.Tests;

[TestFixture]
public class PointServiceTests
{
    private ApplicationDbContext _context;
    private PointRepository _pointRepository;
    private PointService _pointService;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestLasmartV2Test").Options;
        _context = new ApplicationDbContext(options);
        _pointRepository = new PointRepository(_context);
        _pointService = new PointService(_pointRepository);
    }
    
    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task AddPointAsync_ShouldAddPointToDatabase()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 10, Color = "red" };
        await _pointService.AddPointAsync(point);
        var allPoints = await _pointService.GetPointsAsync();
        
        var pointFromDb = await _pointRepository.GetByIdAsync(1);

        Assert.That(allPoints.Count().Equals(1));
        Assert.That(pointFromDb.Equals(point));
        Assert.Pass();
    }
    
    [Test]
    public async Task AddPointAsync_ShouldAddPointWithCommentsToDatabase()
    {
        var comments = new List<Comment>
        {
            new Comment { Id = 1, Text = "Comment 1", BgColor = "red", PointId = 1 },
            new Comment { Id = 2, Text = "Comment 2", BgColor = "green", PointId = 1 }
        };
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 10, Color = "red", Comments  = comments};
        
        await _pointService.AddPointAsync(point);
        var allPoints = await _pointService.GetPointsAsync();
        
        var pointFromDb = await _pointRepository.GetByIdAsync(1);

        Assert.That(allPoints.Count().Equals(1));
        Assert.That(pointFromDb.Equals(point));
        Assert.Pass();
    }

    [Test]
    public async Task GetPointsAsync_ShouldReturnAllPoints()
    {
        var points = new List<Point>
        {
            new Point { Id = 1, X = 10, Y = 10, Radius = 10, Color = "red" },
            new Point { Id = 2, X = 15, Y = 15, Radius = 10, Color = "green" }
        };
        foreach (var point in points)
        {
            await _pointService.AddPointAsync(point);
        }
        
        var allPoints = await _pointService.GetPointsAsync();
        
        Assert.That(allPoints.Count().Equals(2));
        Assert.Pass();
    }

    [Test]
    public async Task DeletePointAsync_ShouldRemovePointFromDatabase()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 5, Color = "red" };
        await _pointService.AddPointAsync(point);
        
        var allPointsFirst = await _pointService.GetPointsAsync();
        
        Assert.That(allPointsFirst.Count().Equals(1));

        await _pointService.DeletePointAsync(point.Id);
        var allPointsSecond = await _pointService.GetPointsAsync();
        
        Assert.That(allPointsSecond.Count().Equals(0));
    }
    
    [Test]
    public async Task UpdatePointAsync_ShouldUpdatePointInDatabase()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 5, Color = "red" };
        await _pointService.AddPointAsync(point);
        
        var pointFromDb = await _pointRepository.GetByIdAsync(1);
        
        pointFromDb.X = 15;
        pointFromDb.Y = 15;
        pointFromDb.Radius = 10;
        pointFromDb.Color = "green";
        
        await _pointService.UpdatePointAsync(pointFromDb);
        
        var updatedPointFromDb = await _pointRepository.GetByIdAsync(1);
        
        Assert.That(updatedPointFromDb.Equals(pointFromDb));
        Assert.Pass();
    }
}