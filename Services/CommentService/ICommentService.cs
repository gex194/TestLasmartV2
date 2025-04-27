using TestLasmartV2.Entities;

namespace TestLasmartV2.Services.CommentService;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetCommentsAsync();
    Task<Comment> GetCommentByIdAsync(int id);
    Task AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
}