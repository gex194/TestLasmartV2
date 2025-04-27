using TestLasmartV2.Entities;

namespace TestLasmartV2.Repositories;

public interface IPointRepository
{
    Task<IEnumerable<Point>> GetAllAsync();
    Task<Point> GetByIdAsync(int id);
    Task AddAsync(Point point);
    Task UpdateAsync(Point point);
    Task DeleteAsync(int id);
}