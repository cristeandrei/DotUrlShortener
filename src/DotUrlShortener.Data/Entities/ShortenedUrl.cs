namespace DotUrlShortener.Data.Entities;

public class ShortenedUrl
{
    public int ShortenedUrlId { get; private set; }

    public required string Path { get; init; }

    public required DateTime CreationDateTime { get; init; }
}
