using System;

namespace Kurao.Tools
{
    public class Timer
    {
        private float _duration;
        private float _timeRemaining;
        private bool _isRunning;

        public bool IsRunning => _isRunning;
        public bool IsFinished => !_isRunning && _timeRemaining <= 0f;

        public event Action OnTimerCompleted;

        public Timer(float duration)
        {
            _duration = duration;
            _timeRemaining = duration;
            _isRunning = false;
        }

        public void Start()
        {
            _timeRemaining = _duration;
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Reset()
        {
            _timeRemaining = _duration;
            _isRunning = false;
        }

        public void Tick(float deltaTime)
        {
            if (!_isRunning) return;

            _timeRemaining -= deltaTime;

            if (_timeRemaining <= 0f)
            {
                _timeRemaining = 0f;
                _isRunning = false;
                OnTimerCompleted?.Invoke();
            }
        }

        public float GetNormalizedTime() => 1f - (_timeRemaining / _duration);
        public float GetTimeRemaining() => _timeRemaining;
    }
}
