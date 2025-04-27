using Microsoft.AspNetCore.Mvc;
using TestLasmartV2.DTOs.Input;
using TestLasmartV2.Entities;
using TestLasmartV2.Services.PointService;

namespace TestLasmartV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PointController : ControllerBase
{
    private readonly IPointService _pointService;

    public PointController(IPointService pointService)
    {
        _pointService = pointService;
    }
    
    [HttpGet]
    public async Task<ActionResult<Point>> GetPoints()
    {
        return Ok(await _pointService.GetPointsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Point>> GetPointById(int id)
    {
        var point = await _pointService.GetPointByIdAsync(id);
        if (point == null)
        {
            return NotFound();
        }
        return Ok(point);
    }
    
    [HttpPost]
    public async Task<ActionResult<Point>> AddPoint(PointInputDTO pointInput)
    {
        var point = new Point()
        {
            X = pointInput.X,
            Y = pointInput.Y,
            Radius = pointInput.Radius,
            Color = pointInput.Color,
        };
        await _pointService.AddPointAsync(point);
        return CreatedAtAction(nameof(GetPointById), new {id = point.Id}, point);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Point>> UpdatePoint(int id, PointInputDTO pointInput)
    {
        var point = await _pointService.GetPointByIdAsync(id);
        if (point == null)
        {
            return NotFound();
        }
        
        point.X = pointInput.X;
        point.Y = pointInput.Y;
        if (pointInput.Radius != 0 && pointInput.Color != string.Empty)
        {
            point.Radius = pointInput.Radius;
            point.Color = pointInput.Color;
        }
        
        if (id != point.Id)
        {
            return BadRequest();
        }
        await _pointService.UpdatePointAsync(point);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Point>> DeletePoint(int id)
    {
        await _pointService.DeletePointAsync(id);
        return NoContent();
    }
}