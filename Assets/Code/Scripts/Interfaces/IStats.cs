public interface IStats<T>
{
    void SetStats(T stats);
    void IncreaseStats(T stats);
    void DecreaseStats(T stats);
}
