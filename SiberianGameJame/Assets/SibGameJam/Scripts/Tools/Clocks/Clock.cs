using System;
using System.Collections;
using UnityEngine;

namespace Tools.Clocks
{
    public abstract class Clock
    {
        public float InitialTime { get; protected set; }
        public float CurrentTime { get; protected set; }
        
        public bool IsTicking { get; protected set; }

        private readonly MonoBehaviour _context;
        private Coroutine _coroutine;
        
        private const string DefaultTimeFormat = @"mm\:ss\:fff";
        private readonly string _timeFormat;

        private const float DefaultAcceleration = 1;
        public float Acceleration { get; protected set; }
        
        public event Action StartEvent = delegate { };
        public event Action StopEvent = delegate { };
        
        public event Action PauseEvent = delegate { };
        public event Action ResumeEvent = delegate { };

        protected Clock(MonoBehaviour context, float initialTime, string timeFormat = DefaultTimeFormat)
        {
            _context = context;
            
            InitialTime = initialTime;
            CurrentTime = initialTime;

            _timeFormat = timeFormat;
        }

        #region MainMethods

        public void Start()
        {
            if (!IsTicking)
            {
                StartTicking();
                StartEvent.Invoke();
            }
        }
        
        public void Stop()
        {
            if (IsTicking)
            {
                StopTicking();
                StopEvent.Invoke();
            }
        }

        public void Resume()
        {
            if (!IsTicking)
            {
                StartTicking();
                ResumeEvent.Invoke();
            }
        }
        
        public void Pause()
        {
            if (IsTicking)
            {
                StartTicking();
                PauseEvent.Invoke();
            }
        }

        public void Reset(float? value = null)
        {
            if (!IsTicking)
            {
                InitialTime = value ?? InitialTime;
                CurrentTime = InitialTime;
            }
        }

        public void HardReset(float? value = null)
        {
            Stop();
            Reset(value);
        }

        #endregion
        
        #region Editing

        public void Add(float time)
        {
            if (time < 0) return;
            CurrentTime += time;
        }
        
        public void Subtract(float time)
        {
            if (time < 0) return;
            CurrentTime -= time;
        }
        
        public void ChangeAcceleration(float acceleration)
        {
            if (acceleration < 0)
                throw new ArgumentException(nameof(acceleration));

            Acceleration = acceleration;
        }

        #endregion
        
        #region Coroutine

        protected abstract void Tick(float deltaTime);
        
        private void StartTicking()
        {
            IsTicking = true;
            _coroutine = _context.StartCoroutine(Ticking());
        }
        
        private void StopTicking()
        {
            if (_coroutine != null)
            {
                IsTicking = false;
                _context.StopCoroutine(_coroutine);
            }
        }

        private IEnumerator Ticking()
        {
            while (IsTicking)
            {
                Tick(Time.deltaTime);
                yield return null;
            }
                
        }

        #endregion

        public override string ToString()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(CurrentTime);
            return timeSpan.ToString(_timeFormat);
        }
    }
}