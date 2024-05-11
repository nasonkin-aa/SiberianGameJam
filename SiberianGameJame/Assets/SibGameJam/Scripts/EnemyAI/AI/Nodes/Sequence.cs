namespace EnemyAI
{
    public class Sequence : Node 
    {
        public Sequence(string name, int priority = 0) : base(name, priority) { }

        public override Status Process() 
        {
            if (EndIteration) 
            {
                Reset();
                return Status.Success;
            }

            switch (CurrentChild.Process()) 
            {
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    ChildIndex = 0;
                    return Status.Failure;
                default:
                    ChildIndex++;
                    return Status.Running;
            }
        }
    }
}