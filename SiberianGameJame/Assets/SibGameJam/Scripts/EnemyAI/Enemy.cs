using Tools;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(Entity))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private PatrolPoints patrolPoints;
        [SerializeField] private ColliderDetector<ITargetable> followDetector;
        [SerializeField] private ColliderDetector<IDamageable> attackDetector;
        [SerializeField] private ColliderDetector<Collider2D> wallDetector;

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

            var patrollingAction = new Sequence("Patrolling Action");
            patrollingAction.AddChild(new Leaf("Walking Around", new PatrolStrategy(_entity, patrolPoints)));
            patrollingAction.AddChild(new Leaf("Stop", new ActionStrategy(_entity.Moveable.Stop)));
            patrollingAction.AddChild(NodeFactory.CreateWaitNode("Looking Around", this, RandomTime));

            var patrol = new Sequence("Patrol", 5);
            patrol.AddChild(NodeFactory.CreateWhileNode("Patrol Loop", patrollingAction));

            var follow = new Sequence("Follow", 10);

            var followMain = new Sequence("Follow Main");
            followMain.AddChild(new Leaf("Try Find", new ConditionStrategy(() =>
            {
                followDetector.Handle();
                return followDetector.Detected.HasValue;
            })));
            var followPredicate = new Leaf("If Find And In Follow Radius", new ConditionStrategy(() =>
                followDetector.Detected.IsSome(out var some) && 
                _entity.Moveable.Position.InRange(some.Position, 5)
            ));
            var followAction = new Sequence("Follow Action");
            followAction.AddChild(new Leaf("Follow", new FollowStrategy(_entity.Moveable, followDetector)));
            
            followMain.AddChild(NodeFactory.CreateWhileNode("Follow Loop", followPredicate, followAction));
            
            follow.AddChild(followMain);
            follow.AddChild(NodeFactory.CreateWaitNode("Wait After Miss", this, () => 2));

            var kissGround = new Sequence("Kiss The Ground", 12);
            kissGround.AddChild(new Leaf("", new ConditionStrategy(() =>
            {
                wallDetector.Handle();
                return wallDetector.Detected.HasValue;
            })));
            kissGround.AddChild(new Leaf("", new ActionStrategy(_entity.Jumpable.Jump)));
            
            var attack = new Sequence("Attack", 15);

            var attackPredicate = new Leaf("If In Attack Range", new ConditionStrategy(() =>
            {
                attackDetector.Handle();
                return attackDetector.Detected.HasValue;
            }));
            var attackAction = new Sequence("Attack Action");
            attackAction.AddChild(new Leaf("Do Attack", new ActionStrategy(() => 
                _entity.Attackable.Attack(attackDetector.Detected.Value))));
            attackAction.AddChild(NodeFactory.CreateWaitNode("Attack Cooldown", this, () => 1));
            
            attack.AddChild(new Leaf("If In Attack Range", new ConditionStrategy(() => 
            {
                attackDetector.Handle();
                return attackDetector.Detected.HasValue;
            })));
            attack.AddChild(new Leaf("Stop", new ActionStrategy(_entity.Moveable.Stop)));
            attack.AddChild(NodeFactory.CreateWhileNode("Attack Loop", attackPredicate, attackAction));

            var root = new PrioritySelector("Root");
            root.AddChild(follow);
            root.AddChild(patrol);
            root.AddChild(kissGround);
            root.AddChild(attack);
            
            _tree.AddChild(root);
        }

        private void Update()
        {
            _tree.Process();
        }

        private float RandomTime() => Random.Range(1, 2);
    }
}