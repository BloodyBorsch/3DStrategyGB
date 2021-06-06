using UnityEngine;
using UnityEngine.AI;


namespace Abstractions
{
    public interface ICommandExecutor
    {
        void Execute(ICommand command);
        void CreateDependances(NavMeshAgent agent, Animator animator);
    }

    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        protected NavMeshAgent _agent;
        protected Animator _animator;
        
        protected readonly string _speed = "Speed";

        public void Execute(ICommand command)
        {
            ExecuteConcreteCommand((T)command);
        }

        public void CreateDependances(NavMeshAgent agent, Animator animator)
        {
            _agent = agent;
            _animator = animator;
        }

        protected abstract void ExecuteConcreteCommand(T command);

        protected abstract void AnimationLocomotion();
    }
}
