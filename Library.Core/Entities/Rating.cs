namespace Library.Core.Entities;

public class Rating : BaseEntity
{
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int Score { get; set; }


}