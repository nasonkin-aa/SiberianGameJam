using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class FollowStrategy : IStrategy
    {
        private readonly Entity _entity;
        private readonly IMoveable _moveable;
        private readonly Collider2D _collider;
        private readonly ContactFilter2D _contactFilter;

        private readonly List<Collider2D> _colliders = new();
        private Option<ITargetable> _targetable = Option<ITargetable>.None;
        
        public FollowStrategy(Entity entity, Collider2D collider)
        {
            _entity = entity;
            _moveable = entity.Moveable;
            _collider = collider;
            _contactFilter.NoFilter();
        }
        
        public Node.Status Process()
        {
            if (!_targetable.HasValue)
            {
                Option<ITargetable> a = Option<ITargetable>.None;

                Physics2D.OverlapCollider(_collider, _contactFilter, _colliders);
                foreach (var collider in _colliders)
                {
                    if (!collider.TryGetComponent(out ITargetable targetable)) continue;
                    if (ReferenceEquals(targetable, _entity)) continue;

                    _targetable = Option<ITargetable>.Some(targetable);
                }

                return _targetable.HasValue ? Node.Status.Running : Node.Status.Failure;
            }

            var position = _targetable.Value.Position;
            if (position.sqrMagnitude > 64)
            {
                Reset();
                return Node.Status.Failure;
            }
            
            _moveable.MoveTo(position);
            if (!_moveable.Reached(position)) return Node.Status.Running;
            
            Reset();
            return Node.Status.Success;
        }

        public void Reset()
        {
            _moveable.Stop();
            _targetable = Option<ITargetable>.None;
        }
    }
}