using Abstractions;
using UnityEngine;
using System;
using Zenject;


namespace InputSystem
{    
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAssetAttributes("Ellen")] private GameObject _unitPrefab;        
        public GameObject UnitPrefab => _unitPrefab;
    }

    public class MoveUnitCommand : IMoveCommand
    {
        public Vector3 Position { get; }
                
        public MoveUnitCommand(Vector3 position)
        {
            Position = position;
        }
    }

    public class PatrolUnitCommand : IPatrolCommand
    {
        public Vector3 FirstPosition { get; set; }

        public Vector3 SecondPosition { get; }

        public PatrolUnitCommand(Vector3 targetPosition)
        {            
            SecondPosition = targetPosition;
        }

        public Vector3 SetFirstPosition(Vector3 position) => FirstPosition = position;
    }

    public class AttackUnitCommand : IAttackCommand
    {
        public ISelectableItem Target { get; }

        public AttackUnitCommand(ISelectableItem obj)
        {
            Target = obj;
        }
    }

    public class StopUnitCommand : IStopCommand
    {

    }
}
