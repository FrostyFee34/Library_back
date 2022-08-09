namespace Library.Core.Entities;

public class Review : BaseEntity
{
    public int BookId { get; set; }
    public string Message { get; set; }
    public string Reviewer { get; set; }

}