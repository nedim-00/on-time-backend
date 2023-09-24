namespace OnTime.SharedCore;

public class SystemClock : ISystemClock
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
