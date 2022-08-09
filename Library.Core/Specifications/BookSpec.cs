using Library.Core.Entities;

namespace Library.Core.Specifications;

public class BookSpec : BaseSpecification<Book>
{
    public BookSpec(int Id): base(b=>b.Id == Id)
    {
        AddInclude(b=>b.Ratings);
        AddInclude(b=>b.Reviews);
    }
}