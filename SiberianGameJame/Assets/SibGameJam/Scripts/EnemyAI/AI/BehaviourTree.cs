using System.Text;
using UnityEngine;

namespace EnemyAI
{
    [System.Serializable]
    public class BehaviourTree : Node 
    {
        private readonly IPolicy _policy;
        
        public BehaviourTree(string name, IPolicy policy = null) : base(name) 
        {
            _policy = policy ?? Policies.RunForever;
        }

        public override Status Process()
        {
            var status = Children[CurrentChild].Process();
            if (_policy.ShouldReturn(status)) return status;
            
            CurrentChild = (CurrentChild + 1) % Children.Count;
            
            return Status.Running;
        }

        public void PrintTree() 
        {
            var sb = new StringBuilder();
            PrintNode(this, 0, sb);
            Debug.Log(sb.ToString());
        }

        static void PrintNode(Node node, int indentLevel, StringBuilder sb) 
        {
            sb.Append(' ', indentLevel * 2).AppendLine(node.Name);
            
            foreach (Node child in node.Children)
                PrintNode(child, indentLevel + 1, sb);
        }
    }
}