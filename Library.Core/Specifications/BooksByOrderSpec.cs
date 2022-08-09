using Library.Core.Entities;

namespace Library.Core.Specifications;

public class BooksByOrderSpec : BaseSpecification<Book>
{
    public BooksByOrderSpec(BookSpecOrderParams specOrderParams)
    {
        AddInclude(b=>b.Ratings);
        AddInclude(b=>b.Reviews);
        switch (specOrderParams.Order?.ToLower())
        {
            case "author":
                AddOrderBy(b => b.Author);
                break;
            case "title":
                AddOrderBy(b=>b.Title);
                break;
        }
    }
}