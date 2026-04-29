namespace BookApi.DTOs;

public class PublisherDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<string> BookTitles { get; set; } = [];
}

public class CreatePublisherDto
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class UpdatePublisherDto
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class PublisherSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}