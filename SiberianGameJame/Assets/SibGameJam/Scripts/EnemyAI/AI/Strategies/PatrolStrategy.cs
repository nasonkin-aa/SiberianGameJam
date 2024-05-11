namespace EnemyAI
{
    public class PatrolStrategy : IStrategy
    {
        private readonly IPatrolPoints _patrolPoints;

        private readonly Entity _entity;
        private readonly IMoveable _moveable;
        
        private int _currentIndex;
        
        public PatrolStrategy(Entity entity, IPatrolPoints patrolPoints)
        {
            _entity = entity;
            _moveable = entity.Moveable;
            _patrolPoints = patrolPoints;
        }

        public Status Process() 
        {
            var position = _patrolPoints.Current!.position;
            _moveable.MoveTo(position);
            _moveable.LookAt(position);

            if (_moveable.Reached(position))
            {
                _patrolPoints.MoveNext();
                return Status.Success;
            }

            return Status.Running;
        }
    }
}