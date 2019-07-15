namespace ordering.Domain
{
    public enum OrderStates
    {
        Created,
        Processing,
        Delivering,
        Finished,
        Cancelled
    }
}