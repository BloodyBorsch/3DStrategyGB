using UnityEngine;
using Abstractions;


namespace Core
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteConcreteCommand(IAttackCommand command)
        {
            Debug.Log("Атака");
        }
    }
}
