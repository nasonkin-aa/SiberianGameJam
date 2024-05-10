using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class PatrolStrategy : IStrategy
    {
        private readonly IMoveable moveable;
        private readonly List<Transform> patrolPoints;
        private int currentIndex;

        public PatrolStrategy(
            IMoveable moveable,
            List<Transform> patrolPoints)
        {
            this.moveable = moveable;
            this.patrolPoints = patrolPoints;
        }

        public Node.Status Process() 
        {
            if (currentIndex == patrolPoints.Count) return Node.Status.Success;

            var target = patrolPoints[currentIndex];
            moveable.MoveTo(target.position);

            if (moveable.Reached(target.position))
            {
                currentIndex++;
                moveable.MoveBy(Vector3.zero);
            }

            return Node.Status.Running;
        }
        
        public void Reset() => currentIndex = 0;
    }
}