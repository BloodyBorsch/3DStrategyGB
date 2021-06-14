using Abstractions;
using UnityEngine;
using Utils;


namespace Core
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        private IMoveCommand _command;

        private float _maxFloat = 1.0f;

        public void MoveToPosition(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        private void Awake()
        {
            UpdateManager.SubscribeToUpdate(AnimationLocomotion);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(AnimationLocomotion);
        }

        protected override void ExecuteConcreteCommand(IMoveCommand command)
        {
            _command = command;
            MoveToPosition(_command.Position);
        }       

        protected override void AnimationLocomotion()
        {
            if (_animator != null && _command != null)
            {
                if (_agent.remainingDistance > _agent.stoppingDistance)
                    _animator.SetFloat(_speed, _maxFloat);                
                else _animator.SetFloat(_speed, 0.0f, 0.2f, Time.deltaTime);
            }
        }
    }
}
