namespace EnemyAI
{
    public class Inverter : Node 
    {
        public Inverter(string name) : base(name) { }
        
        public override Status Process() 
        {
            switch (CurrentChild.Process()) 
            {
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    return Status.Success;
                default:
                    return Status.Failure;
            }
        }
    }
}