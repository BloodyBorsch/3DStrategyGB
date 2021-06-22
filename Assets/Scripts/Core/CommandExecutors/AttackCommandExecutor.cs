using UnityEngine;
using Abstractions;
using System;
using Zenject;
using UniRx;


namespace Core
{
    public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        //TODO прокинуть значения
        [Inject(Id = "Distance")] private float _attackDistance = 1.0f;
        [Inject(Id = "AttackSpeed")] private float _attackSpeed = 0.3f;
        [Inject(Id = "AttackDamage")] private float _attackDamage = 100.0f;

        private Vector3 _targetPosition;
        private Vector3 _attackerPosition;

        private AttackOperation _currentAttackOperation;
        private Subject<Vector3> _destination = new Subject<Vector3>();
        private Subject<IAttackable> _attackTarget = new Subject<IAttackable>();

        private IAttackable _target;


        protected override async void ExecuteConcreteCommand(IAttackCommand command)
        {
            Debug.Log("Атака" + command.Target);
            _target = command.Target;
            _currentAttackOperation = new AttackOperation(_target, this);

            try
            {
                await _currentAttackOperation;
                _currentAttackOperation = null;
                CurrentCommand = null;
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("Атака отменена");
            }            
        }

        protected override void AnimationLocomotion()
        {

        }

        [Inject]
        private void Init()
        {
            _destination.ObserveOn(Scheduler.MainThread).Subscribe(MoveTo).AddTo(this);
            _attackTarget.ObserveOnMainThread().Subscribe(AttackTarget).AddTo(this);
        }

        public void Update()
        {
            if (!gameObject.activeSelf || _target == null || _currentAttackOperation == null) return;

            lock (this)
            {
                _attackerPosition = gameObject.transform.position;
                _targetPosition = _target.Position;
            }            
        }

        private void MoveTo(Vector3 position)
        {
            if (gameObject.activeSelf && _agent.isActiveAndEnabled) _agent.SetDestination(position);
        }

        private void AttackTarget(IAttackable target)
        {
            if (_agent.isActiveAndEnabled) _agent.ResetPath();

            target.RecieveDamage(_attackDamage);

            if ((target.Health <= 0 || target == null) && _currentAttackOperation != null)
            {
                _currentAttackOperation.Cancel();
                _currentAttackOperation = null;
            }
        }
    }
}
