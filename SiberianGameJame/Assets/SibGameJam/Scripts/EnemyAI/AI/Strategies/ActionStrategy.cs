using System;

namespace EnemyAI
{
    public class ActionStrategy : IStrategy 
    {
        private readonly Action _action;
        
        public ActionStrategy(Action action) 
        {
            _action = action;
        }
        
        public Status Process() 
        {
            _action();
            return Status.Success;
        }
    }
}