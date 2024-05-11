using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    [System.Serializable]
    public class PatrolPoints : IPatrolPoints
    {
        [SerializeField] private List<Transform> transforms;

        private int _currentIndex;

        public PatrolPoints()
        {
            Reset();
            transforms = new List<Transform>();
        }

        public void SetClosestTo(Vector3 position)
        {
            float minDistance = float.MaxValue;
            int index = 0;

            for (int i = 0; i < transforms.Count; i++)
            {
                var distance = Vector3.Distance(transforms[i].position, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }

            _currentIndex = index;
        }

        public bool MoveNext()
        {
            _currentIndex++;

            if (_currentIndex >= transforms.Count)
                Reset();
            
            return true;
        }

        public void Reset() => _currentIndex = 0;

        public Transform Current => transforms[_currentIndex];

        object IEnumerator.Current => Current;

        public void Dispose() {}
    }
}