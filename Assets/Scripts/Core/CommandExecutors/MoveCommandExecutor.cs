using UnityEngine;
using Abstractions;


namespace Core
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteConcreteCommand(IMoveCommand command)
        {
            //TODO плавное перемещение во времени
            transform.position = command.Position;
        }
    }
}
