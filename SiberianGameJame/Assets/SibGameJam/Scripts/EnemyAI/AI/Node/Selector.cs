namespace EnemyAI
{
    public class Selector : Node 
    {
        public Selector(string name, int priority = 0) : base(name, priority) { }

        public override Status Process() 
        {
            if (CurrentChild >= Children.Count) 
            {
                Reset();
                return Status.Failure;
            }

            switch (Children[CurrentChild].Process()) 
            {
                case Status.Running:
                    return Status.Running;
                case Status.Success:
                    Reset();
                    return Status.Success;
                default:
                    CurrentChild++;
                    return Status.Running;
            }
        }
    }
}