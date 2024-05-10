namespace EnemyAI
{
    public interface IPolicy 
    {
        bool ShouldReturn(Node.Status status);
    }
}