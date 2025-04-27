using TestLasmartV2.Entities;
using TestLasmartV2.Repositories;

namespace TestLasmartV2.Services.PointService;

public class PointService : IPointService
{
    private readonly IPointRepository _pointRepository;
    
    public PointService(IPointRepository pointRepository)
    {
        _pointRepository = pointRepository;
    }

    public async Task<IEnumerable<Point>> GetPointsAsync()
    {
        return await _pointRepository.GetAllAsync();
    }

    public async Task<Point> GetPointByIdAsync(int id)
    {
        return await _pointRepository.GetByIdAsync(id);
    }

    public async Task AddPointAsync(Point point)
    {
        await _pointRepository.AddAsync(point);
    }
    
    public async Task UpdatePointAsync(Point point)
    {
        await _pointRepository.UpdateAsync(point);
    }
    public async Task DeletePointAsync(int id)
    {
        await _pointRepository.DeleteAsync(id);
    }
}