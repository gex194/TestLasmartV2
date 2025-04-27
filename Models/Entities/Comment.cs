namespace TestLasmartV2.Entities;

public class Comment
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public string? BgColor { get; set; }
    public int PointId { get; set; }
}