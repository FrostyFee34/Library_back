using Library.Core.Entities;

namespace Library.Core.Specifications;

public class BookSpec : BaseSpecification<Book>
{
    public BookSpec(int id) : base(b => b.Id == id)
    {
        AddInclude(b => b.Ratings);
        AddInclude(b => b.Reviews);
    }
}