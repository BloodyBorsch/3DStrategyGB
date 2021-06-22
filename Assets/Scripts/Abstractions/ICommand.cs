using UnityEngine;


namespace Abstractions
{
    public interface ICommand
    {

    }

    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
        public Sprite Icon { get; }

        public float ProductionTime { get; }
        public float ProductionTimeLeft { get; }
    }    

    public interface IProduceUnitCommandChomper : IProduceUnitCommand
    {

    }
    
    public interface IProduceUnitCommandSpitter : IProduceUnitCommand
    {

    }

    public interface IMoveCommand : ICommand
    {
        Vector3 Position { get; }
    }

    public interface IAttackCommand : ICommand
    {
        IAttackable Target { get; }
    }

    public interface IPatrolCommand : ICommand
    {
        Vector3 FirstPosition { get; }
        Vector3 SecondPosition { get; }
    }

    public interface IStopCommand : ICommand
    {

    }
}
