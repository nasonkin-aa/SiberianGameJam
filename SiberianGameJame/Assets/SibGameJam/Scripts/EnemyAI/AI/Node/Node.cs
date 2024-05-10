using System.Collections.Generic;

namespace EnemyAI
{
    public abstract class Node
    {
        private const string DefaultName = "UnnamedNode";
        
        public enum Status { Success, Failure, Running }
        
        public readonly string name;
        public readonly int priority;
        
        public readonly List<Node> children = new();
        protected int currentChild;

        protected Node(string name = DefaultName, int priority = 0) 
        {
            this.name = name;
            this.priority = priority;
        }
        
        public void AddChild(Node child) => children.Add(child);
        
        public virtual Status Process() => children[currentChild].Process();

        public virtual void Reset() 
        {
            currentChild = 0;
            
            foreach (var child in children)
                child.Reset();
        }
    }
}
