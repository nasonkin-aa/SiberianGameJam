using UnityEngine;

namespace EnemyAI
{
    public class Selector : Node 
    {
        public Selector(string name, int priority = 0) : base(name, priority) { }

        public override Status Process() 
        {
            if (EndIteration) 
            {
                Reset();
                return Status.Failure;
            }
            
            switch (CurrentChild.Process()) 
            {
                case Status.Running:
                    return Status.Running;
                case Status.Success:
                    Reset();
                    return Status.Success;
                default:
                    ChildIndex++;
                    return Status.Running;
            }
        }
    }
}