using Microsoft.AspNetCore.Mvc;
using TestLasmartV2.DTOs.Input;
using TestLasmartV2.Entities;
using TestLasmartV2.Services.CommentService;
using TestLasmartV2.Services.PointService;

namespace TestLasmartV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IPointService _pointService;
    public CommentController(ICommentService commentService, IPointService pointService)
    {
        _commentService = commentService;
        _pointService = pointService;
    }
    
    [HttpGet]
    public async Task<ActionResult<Comment>> GetComments()
    {
        return Ok(await _commentService.GetCommentsAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetCommentById(int id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult> AddComment(CommentInputDTO commentInput)
    {
        var point = await _pointService.GetPointByIdAsync(commentInput.PointId);
        if (point == null)
        {
            return NotFound();       
        }
        var comment = new Comment()
        {
            Text = commentInput.Text,
            BgColor = commentInput.BgColor,
            PointId = commentInput.PointId,
        };
        await _commentService.AddCommentAsync(comment);
        return CreatedAtAction(nameof(GetCommentById), new {id = comment.Id}, comment);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Comment>> UpdateComment(int id, CommentInputDTO commentInput)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        
        comment.Text = commentInput.Text;
        comment.BgColor = commentInput.BgColor;
        
        if (id != comment.Id)
        {
            return BadRequest();
        }
        await _commentService.UpdateCommentAsync(comment);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Comment>> DeleteComment(int id)
    {
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}