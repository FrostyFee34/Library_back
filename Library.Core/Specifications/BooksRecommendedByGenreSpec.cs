using Library.Core.Entities;

namespace Library.Core.Specifications;

public class BooksRecommendedByGenreSpec : BaseSpecification<Book>
{
    public BooksRecommendedByGenreSpec(BookSpecGenreParams specParams) : base(b =>
        (specParams.Genre == null || b.Genre == specParams.Genre) && b.Reviews.Count > 10)
    {
        AddInclude(b => b.Reviews);
        AddInclude(b => b.Ratings);
        AddOrderBy(b => b.Ratings.Sum(x => x.Score) / (double)b.Ratings.Count);
        TakeNumberOf(10);
    }
}