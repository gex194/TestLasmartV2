using TestLasmartV2.Entities;

namespace TestLasmartV2.Repositories.CommentRepository;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllAsync();
    Task<Comment> GetByIdAsync(int id);
    Task AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
}