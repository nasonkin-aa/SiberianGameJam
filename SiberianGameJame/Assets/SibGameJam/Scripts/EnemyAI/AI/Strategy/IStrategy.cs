namespace EnemyAI
{
    public interface IStrategy
    {
        Node.Status Process();
        void Reset();
    }
}