using UnityEngine;

namespace EnemyAI
{
    public class FollowStrategy : IStrategy
    {
        private readonly IMoveable _moveable;
        private readonly IDetector<ITargetable> _detector;
        
        // private ITargetable _target;

        public FollowStrategy(
            IMoveable moveable, 
            IDetector<ITargetable> detector)
        {
            _moveable = moveable;
            _detector = detector;
        }
        
        public Status Process()
        {
            // if (_target == null)
            // {
            //     _detector.Handle();
            //
            //     var detected = _detector.Detected;
            //     if (!detected.IsSome(out _target))
            //         return Status.Failure;
            // }
            //
            // var position = _target.Position;
            // if (!_moveable.Position.InRange(position, _range))
            // {
            //     Reset();
            //     return Status.Failure;
            // }
            //
            // _moveable.MoveTo(position);
            // _moveable.LookAt(position);
            //
            // if (!_moveable.Reached(position)) 
            //     return Status.Running;
            //     
            // Reset();
            // return Status.Success;

            Debug.Log("SS");
            
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