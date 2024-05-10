using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(Entity))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private List<Transform> patrolPoints;
        
        private BehaviourTree _tree;
        private Entity _entity;

        public void Initialize(BehaviourTree tree)
        {
            _tree = tree;
        }
        
        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        private void Start()
        {
            _tree = new BehaviourTree("Entity");
            _tree.AddChild(new Leaf("Patrol", new PatrolStrategy(this, _entity.Moveable, patrolPoints)));
        }

        private void Update()
        {
            _tree.Process();
        }
    }
}