using TestLasmartV2.Entities;

namespace TestLasmartV2.Services.PointService;

public interface IPointService
{
    Task<IEnumerable<Point>> GetPointsAsync();
    Task<Point> GetPointByIdAsync(int id);
    Task AddPointAsync(Point point);
    Task UpdatePointAsync(Point point);
    Task DeletePointAsync(int id);
}