using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TestLasmartV2.Data;
using TestLasmartV2.Entities;
using TestLasmartV2.Repositories;
using TestLasmartV2.Repositories.CommentRepository;
using TestLasmartV2.Services.CommentService;
using TestLasmartV2.Services.PointService;

namespace TestLasmartV2.Tests;

[TestFixture]
public class CommentServiceTests
{
    private ApplicationDbContext _context;
    private CommentRepository _commentRepository;
    private CommentService _commentService;
    private PointService _pointService;
    private PointRepository _pointRepository;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestLasmartV2Test").Options;
        _context = new ApplicationDbContext(options);
        _commentRepository = new CommentRepository(_context);
        _commentService = new CommentService(_commentRepository);
        
        _pointRepository = new PointRepository(_context);
        _pointService = new PointService(_pointRepository);
    }
    
    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task GetCommentsAsync_ShouldReturnAllComments()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 10, Color = "red"};
        await _pointService.AddPointAsync(point);
        
        var comment = new Comment
        {
            Id = 1,
            Text = "Comment 1",
            BgColor = "red",
            PointId = 1
        };
        
        await _commentService.AddCommentAsync(comment);
        
        var allComments = await _commentService.GetCommentsAsync();
        
        Assert.That(allComments.Count().Equals(1));
        Assert.Pass();
    }

    [Test]
    public async Task GetCommentByIdAsync_ShouldReturnCommentById()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 10, Color = "red"};
        await _pointService.AddPointAsync(point);
        
        var comment = new Comment
        {
            Id = 1,
            Text = "Comment 1",
            BgColor = "red",
            PointId = 1
        };
        
        await _commentService.AddCommentAsync(comment);
        
        var commentFromDb = await _commentService.GetCommentByIdAsync(1);
        
        Assert.That(commentFromDb.Equals(comment));;
        Assert.Pass();
    }

    [Test]
    public async Task AddCommentAsync_ShouldAddComment()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 10, Color = "red"};
        await _pointService.AddPointAsync(point);
        
        var comment = new Comment
        {
            Id = 1,
            Text = "Comment 1",
            BgColor = "red",
            PointId = 1
        };
        
        await _commentService.AddCommentAsync(comment);
        
        var commentFromDb = await _commentService.GetCommentByIdAsync(1);
        var pointFromDb = await _pointRepository.GetByIdAsync(1);
        
        Assert.That(commentFromDb.Equals(comment));
        Assert.That(pointFromDb.Comments.Count().Equals(1));
        Assert.Pass();
    }

    [Test]
    public async Task UpdateCommentAsync_ShouldUpdateComment()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 10, Color = "red"};
        await _pointService.AddPointAsync(point);
        
        var comment = new Comment
        {
            Id = 1,
            Text = "Comment 1",
            BgColor = "red",
            PointId = 1
        };
        await _commentService.AddCommentAsync(comment);
        
        var commentFromDb = await _commentService.GetCommentByIdAsync(1);
        
        commentFromDb.Text = "Comment 2";
        commentFromDb.BgColor = "green";
        
        await _commentService.UpdateCommentAsync(commentFromDb);

        var updatedCommentFromDb = await _commentService.GetCommentByIdAsync(1);
        
        Assert.That(commentFromDb.Equals(updatedCommentFromDb));
    }

    [Test]
    public async Task RemoveCommentAsync_ShouldRemoveComment()
    {
        var point = new Point {Id = 1, X = 10, Y = 10, Radius = 10, Color = "red"};
        await _pointService.AddPointAsync(point);
        
        var comment = new Comment
        {
            Id = 1,
            Text = "Comment 1",
            BgColor = "red",
            PointId = 1
        };
        
        await _commentService.AddCommentAsync(comment);
        var allComments = await _commentService.GetCommentsAsync();
        Assert.That(allComments.Count().Equals(1));
        
        await _commentService.DeleteCommentAsync(1);
        var allCommentsAfterDelete = await _commentService.GetCommentsAsync();
        
        Assert.That(allCommentsAfterDelete.Count().Equals(0));
        Assert.Pass();
    }
}