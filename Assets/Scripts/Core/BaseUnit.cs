using UnityEngine;
using Abstractions;
using UnityEngine.AI;
using System.Collections.Generic;


namespace Core
{
    public class BaseUnit : MonoBehaviour, ISelectableItem, IAttackable, IAttacker
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _visionRange;

        private bool _canPerformAutoAttack;

        public Vector3 Position { get; private set; }

        protected NavMeshAgent _agent;
        protected Animator _animator;
        protected AttackCommandExecutor _attackExecutor;

        private ICommandExecutor[] _executors;

        public Sprite Icon => _icon;
        public float Health => _health;
        public float MaxHealth => _maxHealth;

        public float VisionRange => _visionRange;

        private void Awake()
        {      
            if (GetComponent<NavMeshAgent>()) _agent = GetComponent<NavMeshAgent>();
            if (GetComponent<Animator>()) _animator = GetComponent<Animator>();
            if (GetComponent<AttackCommandExecutor>()) _attackExecutor = GetComponent<AttackCommandExecutor>();
            if (GetComponent<ICommandExecutor>() != null) _executors = GetComponents<ICommandExecutor>();

            if (_executors != null) foreach (var executor in _executors) executor.CreateDependances(_agent, _animator);
        }

        protected void Update()
        {
            Position = transform.position;
            _canPerformAutoAttack = _attackExecutor.CurrentCommand == null;
        }

        public bool CanPerformAutoAttack()
        {
            return _canPerformAutoAttack;
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

        public void AttackTarget(IAttackable target)
        {
            _attackExecutor.Execute(new AutoAttackCommand(target));            
        }
    }
}
