using UnityEngine;


namespace Abstractions
{
    public interface ICommand
    {

    }

    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }    

    public interface IMoveCommand : ICommand
    {
        Vector3 Position { get; }
    }

    public interface IAttackCommand : ICommand
    {
        ISelectableItem Target { get; }
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
