using IdeasAppBackend;
using Microsoft.EntityFrameworkCore;

await using var ctx = new IdeasContext();
// our user doesn't have permission to 
// create and delete the database!
await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();

ctx.Ideas.Add(new Idea() {IdeaText = "My fantastic idea!"});

await ctx.SaveChangesAsync();

var res = await ctx.Ideas.Where(t => t.IdeaText.StartsWith("M")).ToListAsync();

Console.WriteLine("Here are the ideas that start with M!\n\n");
Console.WriteLine(string.Join("\n", res));

Console.WriteLine("Let's like all of them!");

foreach (Idea idea in res)
{
    idea.Likes = 123;
    idea.Dislikes = 255;
    idea.EditDate = DateTime.UtcNow;
} 

await ctx.SaveChangesAsync();
