using UnityEngine;
using Abstractions;


namespace Core
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        protected override void ExecuteConcreteCommand(IStopCommand command)
        {
            Debug.Log("Оcтановка");
        }

        protected override void AnimationLocomotion()
        {
            
        }
    }
}
