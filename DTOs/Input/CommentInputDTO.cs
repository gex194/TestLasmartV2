namespace TestLasmartV2.DTOs.Input;

public class CommentInputDTO
{
    public required string Text { get; set; }
    public string? BgColor { get; set; }
    public int PointId { get; set; }
}