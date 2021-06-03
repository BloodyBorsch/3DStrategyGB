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
        Vector3 FirstPosition { get; set; }
        Vector3 SecondPosition { get; }

        Vector3 SetFirstPosition(Vector3 position);
    }

    public interface IStopCommand : ICommand
    {

    }
}
