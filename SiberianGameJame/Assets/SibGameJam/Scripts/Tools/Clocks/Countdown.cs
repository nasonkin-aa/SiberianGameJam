using UnityEngine;

namespace Tools.Clocks
{
    public class Countdown : Clock
    {
        public Countdown(MonoBehaviour context, float initialTime = 0) : base(context, initialTime)
        {
        }

        protected override void Tick(float deltaTime)
        {
            CurrentTime -= deltaTime;

            if (CurrentTime < 0)
            {
                CurrentTime = 0;
                Stop();
            }
        }
    }
}