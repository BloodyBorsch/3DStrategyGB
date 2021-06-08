using UnityEngine;
using Abstractions;


namespace Core
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        protected override void ExecuteConcreteCommand(IAttackCommand command)
        {
            Debug.Log("Атака" + command.Target);
        }

        protected override void AnimationLocomotion()
        {
            
        }
    }
}
