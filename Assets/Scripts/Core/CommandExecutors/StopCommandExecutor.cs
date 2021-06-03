using UnityEngine;
using Abstractions;


namespace Core
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteConcreteCommand(IStopCommand command)
        {
            Debug.Log("Отановка");
        }
    }
}
