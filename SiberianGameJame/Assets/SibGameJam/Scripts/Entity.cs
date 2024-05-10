using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(IMoveable))]
    public class Entity : MonoBehaviour
    {
        [SerializeField] private List<Transform> patrolPoints;

        private IMoveable _moveable;
        
        private BehaviourTree _tree;

        private void Awake()
        {
            _moveable = GetComponent<IMoveable>();
            
            _tree = new BehaviourTree("Entity");
            _tree.AddChild(new Leaf("Patrol", new PatrolStrategy(_moveable, patrolPoints)));
        }

        public void Initialize(BehaviourTree tree)
        {
            _tree = tree;
        }

        private void Update()
        {
            _tree.Process();
        }
    }
}