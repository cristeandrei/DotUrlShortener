namespace ReduceUrl.Data.Models;

public class ReducedUrl
{
    public int ReducedUrlId { get; private set; }

    public required string Path { get; init; }

    public required DateTime CreationDateTime { get; init; }
}
