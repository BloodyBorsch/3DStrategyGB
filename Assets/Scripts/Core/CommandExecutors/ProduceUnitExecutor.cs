using Abstractions;
using UnityEngine;


namespace Core
{
    public class ProduceUnitExecutor : CommandExecutorBase<IProduceUnitCommand>
    {
        public override void ExecuteConcreteCommand(IProduceUnitCommand command)
        {
            if (command.UnitPrefab == null)
            {
                Debug.LogError("No Prefab");
                return;
            }

            Instantiate(command.UnitPrefab, transform.position, Quaternion.identity);
        }
    }
}
