using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IdeasAppBackend;

public class IdeasContext : DbContext
{
    public DbSet<Idea> Ideas { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost;Username=localadmin;Password=password;Database=ideasdb").UseSnakeCaseNamingConvention().UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
}

public class Idea
{
    public override string ToString() => IdeaText;

    [Key]
    public int IdeaId { get; set; }
    public string IdeaText { get; set; } = "";

    public uint Likes { get; set; }
    public uint Dislikes { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime EditDate { get; set; }

    public Idea()
    {
        CreationDate = DateTime.UtcNow;
        EditDate = DateTime.UtcNow;
    }
}

public class Comment
{
    [Key]
    public int CommentId { get; set; }
    public string CommentText { get; set; } = "";
    public int IdeaId { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime EditDate { get; set; }
}
