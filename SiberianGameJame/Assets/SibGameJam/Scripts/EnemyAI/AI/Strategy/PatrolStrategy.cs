using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class PatrolStrategy : IStrategy
    {
        private readonly IReadOnlyList<Transform> _patrolPoints;
        private readonly float _minWaitTime;
        private readonly float _maxWaitTime;
        
        private readonly Entity _entity;
        private readonly IMoveable _moveable;
        
        private int _currentIndex;
        private Coroutine _coroutine;

        public PatrolStrategy(Entity entity)
        {
            _entity = entity;
            _moveable = entity.Moveable;
        }
        
        public PatrolStrategy(Entity entity, 
            IReadOnlyList<Transform> patrolPoints,
            float minWaitTime = 0,
            float maxWaitTime = 0
            ) : this(entity)
        {
            _patrolPoints = patrolPoints;
            _minWaitTime = minWaitTime;
            _maxWaitTime = maxWaitTime;
        }

        public Node.Status Process() 
        {
            if (_currentIndex >= _patrolPoints.Count)
            {
                Reset();
                return Node.Status.Success;
            }

            var position = _patrolPoints[_currentIndex].position;
            _moveable.MoveTo(position);

            if (!_moveable.Reached(position)) return Node.Status.Running;
            
            _moveable.Stop();
            _coroutine ??= _entity.StartCoroutine(WaitSomeTime());

            return Node.Status.Running;
        }

        public void Reset() => _currentIndex = 0;
        
        private IEnumerator WaitSomeTime()
        {
            yield return new WaitForSeconds(Random.Range(_minWaitTime, _maxWaitTime));
            
            _currentIndex++;
            _coroutine = null;
        }
    }
}