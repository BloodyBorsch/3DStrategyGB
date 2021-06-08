using Abstractions;
using UnityEngine;


namespace Core
{
    public class ProduceUnitExecutor : CommandExecutorBase<IProduceUnitCommand>
    {
        private readonly float _offset = 2.0f;

        protected override void ExecuteConcreteCommand(IProduceUnitCommand command)
        {     
            if (command.UnitPrefab == null)
            {
                Debug.LogError("No Prefab");
                return;
            }

            Instantiate(command.UnitPrefab, transform.position + Vector3.forward * _offset, Quaternion.identity, transform.parent);
        }

        protected override void AnimationLocomotion()
        {
            
        }
    }
}
