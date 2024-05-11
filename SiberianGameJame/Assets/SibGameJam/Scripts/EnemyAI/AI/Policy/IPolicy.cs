namespace EnemyAI
{
    public interface IPolicy 
    {
        bool ShouldReturn(Status status);
    }
}