using UnityEngine;

namespace EnemyAI
{
    public class FollowStrategy : IStrategy
    {
        private readonly IMoveable _moveable;
        private readonly IDetector<ITargetable> _detector;

        public FollowStrategy(
            IMoveable moveable, 
            IDetector<ITargetable> detector)
        {
            _moveable = moveable;
            _detector = detector;
        }
        
        public Status Process()
        {
            if (!_detector.Detected.IsSome(out var target)) 
                return Status.Failure;
            
            var position = target.Position;
                
            _moveable.MoveTo(position);
            _moveable.LookAt(position);
                
            if (!_moveable.Reached(position)) 
                return Status.Running;
                
            Reset();
            return Status.Success;
        }

        public void Reset() => _moveable.Stop();
    }
}