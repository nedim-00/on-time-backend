namespace OnTime.Domain.Entities;

public class Metadata
{
    public Metadata()
    {
        CreatedAt = DateTime.Now;
        VoidedAt = null;
    }

    public void Void()
    {
        VoidedAt = DateTime.Now;
    }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? VoidedAt { get; private set; }
}
