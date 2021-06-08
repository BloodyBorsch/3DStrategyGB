using System;
using UnityEngine;


namespace Utils
{
    public class TimeManager
    {
        private Action _method;
        
        private float _timeCD;
        private float _currentTime;

        private bool _inAction;
        private bool _isRepeating;

        public TimeManager(Action method, float time, bool isRepeating = false)
        {
            _method += method;
            _timeCD = time;
            _currentTime = time;
            _isRepeating = isRepeating;

            UpdateManager.SubscribeToUpdate(Execute);
        }

        public void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Execute);
            _method = null;
        }

        public void UseCoolDown()
        {
            _inAction = true;
            _currentTime = _timeCD;
        }

        private void Execute()
        {       
            if (!_inAction) return;            

            var time = Time.deltaTime;
            _currentTime -= time;

            if (_currentTime <= 0.0f)
            {
                _method?.Invoke();

                if (!_isRepeating)
                {
                    RemoveCoolDown();
                }
                else
                {
                    _currentTime = _timeCD;
                }
            }
        }        

        private void RemoveCoolDown()
        {
            _inAction = false;
        }
    }
}

