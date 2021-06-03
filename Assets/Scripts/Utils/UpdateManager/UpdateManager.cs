﻿using System;
using UnityEngine;


namespace Utils
{
    public class UpdateManager : MonoBehaviour
    {
        private static event Action OnUpdateEvent;
        private static event Action OnFixedUpdateEvent;
        private static event Action OnLateUpdateEvent;

        public static void SubscribeToUpdate(Action callback)
        {
            OnUpdateEvent += callback;            
        }

        public static void SubscribeToFixedUpdate(Action callback)
        {
            OnFixedUpdateEvent += callback;
        }

        public static void SubscribeToLateUpdate(Action callback)
        {
            OnLateUpdateEvent += callback;
        }

        public static void UnsubscribeFromUpdate(Action callback)
        {
            OnUpdateEvent -= callback;
        }

        public static void UnsubscribeFromFixedUpdate(Action callback)
        {
            OnFixedUpdateEvent -= callback;
        }

        public static void UnsubscribeFromLateUpdate(Action callback)
        {
            OnLateUpdateEvent -= callback;
        }

        private void Update()
        {
            if (OnUpdateEvent != null) OnUpdateEvent.Invoke();
        }

        private void FixedUpdate()
        {
            if (OnFixedUpdateEvent != null) OnFixedUpdateEvent.Invoke();
        }

        private void LateUpdate()
        {
            if (OnLateUpdateEvent != null) OnLateUpdateEvent.Invoke();
        }
    }
}