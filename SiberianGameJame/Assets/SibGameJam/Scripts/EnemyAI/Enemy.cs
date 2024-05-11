using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(Entity))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private List<Transform> patrolPoints;
        [SerializeField] private Collider2D collider;
        
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
            
            var selector = new Selector("Main");
            selector.AddChild(new Leaf("Follow", new FollowStrategy(_entity, collider)));
            selector.AddChild(new Leaf("Patrol", new PatrolStrategy(_entity, patrolPoints)));
            
            _tree.AddChild(selector);
        }

        private void Update()
        {
            _tree.Process();
        }
    }
}