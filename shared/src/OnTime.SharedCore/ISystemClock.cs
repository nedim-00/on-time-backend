namespace OnTime.SharedCore;

public interface ISystemClock
{
    DateTimeOffset Now { get; }
}
