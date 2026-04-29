namespace BookApi.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public PublisherSummaryDto? Publisher { get; set; }
    public List<AuthorSummaryDto> Authors { get; set; } = [];
}

public class AuthorSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int? PublisherId { get; set; }
    public List<int> AuthorIds { get; set; } = [];
}

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int? PublisherId { get; set; }
}