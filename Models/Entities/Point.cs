namespace TestLasmartV2.Entities;

public class Point
{
    public int Id { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Radius { get; set; }
    public string? Color { get; set; }
    public List<Comment>? Comments { get; set; }
}