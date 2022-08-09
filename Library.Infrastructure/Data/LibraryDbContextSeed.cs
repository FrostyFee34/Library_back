using System.Reflection;
using Library.Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Library.Infrastructure.Data;

public class LibraryDbContextSeed
{
    public static async Task SeedAsync(LibraryDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!context.Books.Any())
            {
                var booksData = await File.ReadAllTextAsync($"{path}/Data/SeedData/seedBooks.json");
                var books = JsonConvert.DeserializeObject<List<Book>>(booksData);
                if (books != null)
                    foreach (var book in books)
                        context.Add(book);
                await context.SaveChangesAsync();
            }

            if (!context.Ratings.Any())
            {
                var ratingsData = await File.ReadAllTextAsync($"{path}/Data/SeedData/seedRatings.json");
                var ratings = JsonConvert.DeserializeObject<List<Rating>>(ratingsData);
                if (ratings != null)
                    foreach (var rating in ratings)
                        context.Ratings.Add(rating);
                await context.SaveChangesAsync();
            }

            if (!context.Reviews.Any())
            {
                var reviewsData = await File.ReadAllTextAsync($"{path}/Data/SeedData/seedReviews.json");
                var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewsData);
                if (reviews != null)
                    foreach (var review in reviews)
                        context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<LibraryDbContextSeed>();
            logger.LogError(e, "Error during data seeding.");
        }
    }
}