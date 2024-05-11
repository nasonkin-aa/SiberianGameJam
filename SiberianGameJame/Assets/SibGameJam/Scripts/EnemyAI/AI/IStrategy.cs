namespace EnemyAI
{
    public interface IStrategy
    {
        Status Process();
        void Reset() {}
    }
}