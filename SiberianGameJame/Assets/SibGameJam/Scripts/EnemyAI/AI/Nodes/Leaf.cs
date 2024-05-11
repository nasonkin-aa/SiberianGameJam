namespace EnemyAI
{
    public class Leaf : Node 
    {
        private readonly IStrategy strategy;

        public Leaf(string name, IStrategy strategy, int priority = 0) : base(name, priority) 
        {
            this.strategy = strategy;
        }
        
        public override Status Process() => strategy.Process();

        public override void Reset() => strategy.Reset();
    }
}