using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class Repository : IRepository
{

    private Movie mockMovieObject;
    private Review mockReviewObject;
    private DbContextOptions<RepositoryDbContext> _opts;

    public Repository()
    {
        mockMovieObject = new Movie()
        {
            //Id = 1,
            Summary = "Bob writes a program ...",
            Title = "Bob's Movie",
            ReleaseYear = 2022,
            BoxOfficeSumInMillions = 42
        };
        mockReviewObject = new Review()
        {
            //Id = 1,
            Headline = "Super great movie",
            Rating = 5,
            ReviewerName = "Smølf",
            //MovieId = 1,
            //Movie = mockMovieObject
        };

        var builder = new DbContextOptionsBuilder<RepositoryDbContext>()
            .UseSqlite("Data source=..//GUI/db.db");

        _opts = builder.Options;

        // Create database if needed.
        using (var ctx = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }

        // Add mock data.
        //var mov = AddMovie(mockMovieObject);
        //mockReviewObject.MovieId = mov.Id;
        //mockReviewObject.Movie = mov;
        //AddReview(mockReviewObject);
    }

    public List<Review> GetReviews()
    {
        //return new List<Review>() { mockReviewObject };

        using (var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            return context.ReviewTable.ToList();
        }
    }

    public List<Movie> GetMovies()
    {
        //return new List<Movie>() { mockMovieObject };

        using (var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            return context.MovieTable.ToList();
        }
    }

    public Movie DeleteMovie(int movieId)
    {
        using (var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            var obj = new Movie { Id = movieId };
            context.MovieTable.Remove(obj);
            context.SaveChanges();
            return obj;
        }
    }

    public Review DeleteReview(int reviewId)
    {
        using (var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            var obj = new Review { Id = reviewId };
            context.ReviewTable.Remove(obj);
            context.SaveChanges();
            return obj;
        }
    }

    public Movie AddMovie(Movie movie)
    {
        using (var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            var addedMovie = context.MovieTable.Add(movie);
            context.SaveChanges();


            movie.Id = addedMovie.Entity.Id;
            return movie;
        }
    }

    public Review AddReview(Review review)
    {
        using (var context = new RepositoryDbContext(_opts, ServiceLifetime.Scoped))
        {
            var addedReview = context.ReviewTable.Add(review);
            context.SaveChanges();

            review.Id = addedReview.Entity.Id;
            return review;
        }
    }
}