using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public interface IPatrolPoints : IEnumerator<Transform>
    {
        void SetClosestTo(Vector3 position);
    }
}