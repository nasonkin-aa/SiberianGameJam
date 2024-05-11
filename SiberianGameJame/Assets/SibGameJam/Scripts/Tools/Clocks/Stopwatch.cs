using UnityEngine;

namespace Tools.Clocks
{
    public class Stopwatch : Clock
    {
        public Stopwatch(MonoBehaviour context) : base(context, 0)
        {
        }

        protected override void Tick(float deltaTime)
        {
            CurrentTime += deltaTime;
        }
    }
}