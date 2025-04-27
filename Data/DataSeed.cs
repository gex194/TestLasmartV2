using TestLasmartV2.Entities;

namespace TestLasmartV2.Data;

public class DataSeed
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Points.Any())
        {
            var firstPoint = new Point
            {
                Id = 1, X = 12, Y = 10, Radius = 10, Color = "red", Comments = new List<Comment>()
            };
            var secondPoint = new Point
            {
                Id = 2, X = 15, Y = 15, Radius = 5, Color = "green", Comments = new List<Comment>()
            };
            
            var comment1 = new Comment
            {
                Id = 1,
                Text = "First comment",
                BgColor = "yellow",
                PointId = firstPoint.Id
            };
            
            var comment2 = new Comment
            {
                Id = 2,
                Text = "Second comment",
                BgColor = "green",
                PointId = firstPoint.Id
            };

            var comment3 = new Comment
            {
                Id = 3,
                Text = "Third comment",
                BgColor = "blue",
                PointId = secondPoint.Id
            };

            firstPoint.Comments.Add(comment1);
            firstPoint.Comments.Add(comment2);
            secondPoint.Comments.Add(comment3);
            
            context.Points.AddRange(firstPoint, secondPoint);
            context.Comments.AddRange(comment1, comment2, comment3);
        
            context.SaveChanges();
        }
    }
}