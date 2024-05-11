using System.Collections.Generic;
using System.Linq;

namespace EnemyAI
{
    public class PrioritySelector : Node
    {
        private IEnumerable<Node> _sortedChildren;
        private IEnumerable<Node> SortedChildren => _sortedChildren ??= SortChildren();

        public PrioritySelector(string name, int priority = 0) : base(name, priority)
        {
        }

        public override void Reset()
        {
            base.Reset();
            _sortedChildren = null;
        }

        public override Status Process()
        {
            foreach (var child in SortedChildren)
            {
                switch (child.Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    default:
                        continue;
                }
            }

            Reset();
            return Status.Failure;
        }
        
        protected virtual IEnumerable<Node> SortChildren() => 
            Children.OrderByDescending(child => child.Priority);
    }
}