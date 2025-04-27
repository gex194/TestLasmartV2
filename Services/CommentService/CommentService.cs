using TestLasmartV2.Entities;
using TestLasmartV2.Repositories.CommentRepository;

namespace TestLasmartV2.Services.CommentService;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task<IEnumerable<Comment>> GetCommentsAsync()
    {
        return await _commentRepository.GetAllAsync();
    }
    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _commentRepository.GetByIdAsync(id);
    }
    public async Task AddCommentAsync(Comment comment)
    {
        await _commentRepository.AddAsync(comment);
    }
    public async Task UpdateCommentAsync(Comment comment)
    {
        await _commentRepository.UpdateAsync(comment);
    }
    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}