using Abstractions;
using System;
using UnityEngine;
using Zenject;
using UniRx;
using Utils;


namespace Core
{
    public class TimeModel : ITimeModel, ITickable
    {
        public IObservable<int> GameTime => _gameTime.Select(value => (int)value);

        private readonly ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        private bool _isPaused;

        public void Tick()
        {
            if (!_isPaused) _gameTime.Value += Time.deltaTime;
        }

        public void Pause()
        {
            _isPaused = true;
            UpdateManager.Pause();
        }

        public void UnPause()
        {
            _isPaused = false;
            UpdateManager.Pause();
        }
    }
}