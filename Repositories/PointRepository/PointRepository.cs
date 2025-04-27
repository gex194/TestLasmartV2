using Microsoft.EntityFrameworkCore;
using TestLasmartV2.Data;
using TestLasmartV2.Entities;

namespace TestLasmartV2.Repositories;

public class PointRepository : IPointRepository
{
    private readonly ApplicationDbContext _context;

    public PointRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Point>> GetAllAsync()
    {
        return await _context.Points.ToListAsync();
    }

    public async Task<Point> GetByIdAsync(int id)
    {
        return await _context.Points.FindAsync(id);
    }

    public async Task AddAsync(Point point)
    {
        await _context.Points.AddAsync(point);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Point point)
    {
        _context.Points.Update(point);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var point = await _context.Points.FindAsync(id);
        if (point != null)
        {
            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
        }
    }
}