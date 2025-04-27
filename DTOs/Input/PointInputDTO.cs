using TestLasmartV2.Entities;

namespace TestLasmartV2.DTOs.Input;

public class PointInputDTO
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Radius { get; set; }
    public string? Color { get; set; }
    public List<Comment>? Comments { get; set; }
}