namespace Library.API.DTOs;

public class BookOverviewDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Rating { get; set; }
    public decimal ReviewsNumber { get; set; }
}