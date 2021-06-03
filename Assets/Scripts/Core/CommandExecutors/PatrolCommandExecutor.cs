using UnityEngine;
using Abstractions;


namespace Core
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {        
        public override void ExecuteConcreteCommand(IPatrolCommand command)
        {
            Debug.Log("Патруль");
            command.SetFirstPosition(transform.position);            
        }
    }
}
