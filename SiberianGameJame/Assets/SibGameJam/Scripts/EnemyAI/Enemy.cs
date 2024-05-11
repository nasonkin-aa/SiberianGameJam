using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyAI
{
    [RequireComponent(typeof(Entity))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private PatrolPoints patrolPoints;
        [SerializeField] private Collider2D detector;
        [SerializeField] private ContactFilter2D contactFilter;

        private BehaviourTree _tree;
        private Entity _entity;

        private ColliderDetector<ITargetable> _detector;

        public void Initialize(BehaviourTree tree)
        {
            _tree = tree;
        }
        
        private void Awake()
        {
            _entity = GetComponent<Entity>();
            _detector = new ColliderDetector<ITargetable>(detector, contactFilter);
        }

        private void Start()
        {
            _tree = new BehaviourTree("Entity");

            var patrollingAction = new Sequence("Patrolling Action");
            patrollingAction.AddChild(new Leaf("Walking Around", new PatrolStrategy(_entity, patrolPoints)));
            patrollingAction.AddChild(new Leaf("Stop", new ActionStrategy(_entity.Moveable.Stop)));
            patrollingAction.AddChild(NodeFactory.CreateWaitNode("Looking Around", this, RandomTime));

            var patrol = new Sequence("Patrol", 5);
            patrol.AddChild(new Leaf("Find Closest Point", new ActionStrategy(() => patrolPoints.SetClosestTo(_entity.Moveable.Position))));
            patrol.AddChild(NodeFactory.CreateWhileNode("Patrol Loop", patrollingAction));

            var follow = new Sequence("Follow", 10);

            var followMain = new Sequence("Follow Main");
            followMain.AddChild(new Leaf("Try Find", new ConditionStrategy(() =>
            {
                _detector.Handle();
                return _detector.Detected.HasValue;
            })));
            var followPredicate = new Leaf("If Find And In Follow Radius", new ConditionStrategy(() =>
                _detector.Detected.IsSome(out var some) && 
                _entity.Moveable.Position.InRange(some.Position, 5)
            ));
            var followAction = new Leaf("Follow Action", new FollowStrategy(_entity.Moveable, _detector));
            followMain.AddChild(NodeFactory.CreateWhileNode("Follow Loop", followPredicate, followAction));
            
            follow.AddChild(followMain);
            follow.AddChild(NodeFactory.CreateWaitNode("Wait After Miss", this, () => 2));

            var attack = new Sequence("Attack", 15);
            
            var root = new PrioritySelector("Root");
            root.AddChild(follow);
            root.AddChild(patrol);
            // root.AddChild(attack);
            
            _tree.AddChild(root);
        }

        private void Update()
        {
            _tree.Process();
        }

        private float RandomTime() => Random.Range(1, 2);
    }
}