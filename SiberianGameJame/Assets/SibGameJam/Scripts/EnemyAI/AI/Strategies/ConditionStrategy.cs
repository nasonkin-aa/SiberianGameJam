using System;

namespace EnemyAI
{
    public class ConditionStrategy : IStrategy 
    {
        private readonly Func<bool> _predicate;
        
        public ConditionStrategy(Func<bool> predicate) 
        {
            _predicate = predicate;
        }
        
        public Status Process() => _predicate() ? Status.Success : Status.Failure;
    }
}