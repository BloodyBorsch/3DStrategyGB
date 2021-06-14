using Abstractions;
using UnityEngine;
using System;
using Zenject;


namespace InputSystem
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAssetAttributes("Chomper")] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;

        [Inject(Id = "Chomper")] public Sprite Icon { get; }

        [Inject(Id = "Chomper")] public float ProductionTime { get; }

        public float ProductionTimeLeft { get; }
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
        public Vector3 FirstPosition { get; }

        public Vector3 SecondPosition { get; }

        public PatrolUnitCommand(Vector3 targetPosition, Vector3 firstPosition)
        {
            FirstPosition = firstPosition;
            SecondPosition = targetPosition;
        }
    }

    public class AttackUnitCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AttackUnitCommand(IAttackable obj)
        {
            Target = obj;
        }
    }

    public class StopUnitCommand : IStopCommand
    {

    }
}
