using UnityEngine;
using Abstractions;
using UnityEngine.AI;
using System.Collections.Generic;


namespace Core
{
    public class BaseUnit : MonoBehaviour, ISelectableItem, IAttackable
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        public Vector3 Position => transform.position;

        protected NavMeshAgent _agent;
        protected Animator _animator;

        private ICommandExecutor[] _executors;

        public Sprite Icon => _icon;
        public float Health => _health;
        public float MaxHealth => _maxHealth;

        private void Awake()
        {      
            if (GetComponent<NavMeshAgent>()) _agent = GetComponent<NavMeshAgent>();
            if (GetComponent<Animator>()) _animator = GetComponent<Animator>();
            if (GetComponent<ICommandExecutor>() != null) _executors = GetComponents<ICommandExecutor>();

            if (_executors != null) foreach (var executor in _executors) executor.CreateDependances(_agent, _animator);
        }

        public void RecieveDamage(float value)
        {
            _health -= value;

            if (_health <= 0)
            {
                Debug.Log("Unit Dead");
                Destroy(gameObject);
            }
        }
    }
}
