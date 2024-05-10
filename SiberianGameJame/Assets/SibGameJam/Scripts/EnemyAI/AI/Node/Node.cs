using System.Collections.Generic;

namespace EnemyAI
{
    public abstract class Node
    {
        private const string DefaultName = "UnnamedNode";
        
        public enum Status { Success, Failure, Running }
        
        public readonly string Name;
        public readonly int Priority;
        
        public readonly List<Node> Children = new(1);
        protected int ChildIndex;

        public Node CurrentChild => Children[ChildIndex];
        public bool EndIteration => ChildIndex >= Children.Count;
        
        protected Node(string name = DefaultName, int priority = 0) 
        {
            Name = name;
            Priority = priority;
        }
        
        public void AddChild(Node child) => Children.Add(child);
        
        public virtual Status Process() => Children[ChildIndex].Process();

        public virtual void Reset() 
        {
            ChildIndex = 0;
            
            foreach (var child in Children)
                child.Reset();
        }
    }
}
