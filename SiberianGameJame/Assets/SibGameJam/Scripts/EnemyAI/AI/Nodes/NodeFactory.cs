using System;
using Tools.Clocks;
using UnityEngine;

namespace EnemyAI
{
    public static class NodeFactory
    {
        public static Node CreateWhileNode(string name, Func<bool> condition, Action action = null)
        {
            var whileNode = new BehaviourTree(name, Policies.RunUntilFailure);
            whileNode.AddChild(new Leaf($"{name}: Condition", new ConditionStrategy(condition)));
            if (action != null) whileNode.AddChild(new Leaf($"{name}: Action", new ActionStrategy(action)));

            var whileInverter = new Inverter("WhileInverter");
            whileInverter.AddChild(whileNode);
            
            return whileInverter;
        }
        
        public static Node CreateWhileNode(string name, Node condition, Node action = null)
        {
            var whileNode = new BehaviourTree(name, Policies.RunUntilFailure);
            whileNode.AddChild(condition);
            if (action != null) whileNode.AddChild(action);
            
            var whileInverter = new Inverter("WhileInverter");
            whileInverter.AddChild(whileNode);
            
            return whileInverter;
        }

        public static Node CreateWaitNode(string name, MonoBehaviour context, Func<float> resetTime)
        {
            var countdown = new Countdown(context);
            
            var waitNode = new Sequence("Wait Node");
            waitNode.AddChild(new Leaf("Start Timer", new ActionStrategy(() =>
            {
                countdown.HardReset(resetTime());
                countdown.Start();
            })));
            waitNode.AddChild(CreateWhileNode("While", () => countdown.IsTicking));

            return waitNode;
        }
    }
}