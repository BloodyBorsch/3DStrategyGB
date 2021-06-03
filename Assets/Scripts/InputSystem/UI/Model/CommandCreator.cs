using Abstractions;
using System;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        public void CreateCommand(ICommandExecutor commandExecutor, Action<T> onCreate)
        {
            if (commandExecutor as CommandExecutorBase<T>)
            {
                CreateSpecificCommand(onCreate);
            }
        }

        protected abstract void CreateSpecificCommand(Action<T> onCreate);
    }

    public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void CreateSpecificCommand(Action<IProduceUnitCommand> onCreate)
        {

            onCreate?.Invoke(_context.Inject(new ProduceUnitCommand()));
        }
    }

    public class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IMoveCommand> _onCreate;
        private Vector3Value _currentGroundPosition;

        [Inject]
        private void Init(Vector3Value currentGroundPosition)
        {
            _currentGroundPosition = currentGroundPosition;
            currentGroundPosition.OnSelected += HandleCurrentGroundPositionChanged;
        }

        private void HandleCurrentGroundPositionChanged(Vector3 onSelected)
        {
            onSelected = _currentGroundPosition.Value;
            _onCreate?.Invoke(_context.Inject(new MoveUnitCommand(onSelected)));
        }

        protected override void CreateSpecificCommand(Action<IMoveCommand> onCreate)
        {
            _onCreate = onCreate;
        }
    }

    public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IPatrolCommand> _onCreate;
        private Vector3Value _currentGroundPosition;

        [Inject]
        private void Init(Vector3Value currentGroundPosition)
        {
            _currentGroundPosition = currentGroundPosition;
            currentGroundPosition.OnSelected += HandleCurrentGroundPositionChanged;
        }

        private void HandleCurrentGroundPositionChanged(Vector3 onSelected)
        {
            onSelected = _currentGroundPosition.Value;
            _onCreate?.Invoke(_context.Inject(new PatrolUnitCommand(onSelected)));
        }

        protected override void CreateSpecificCommand(Action<IPatrolCommand> onCreate)
        {
            _onCreate = onCreate;
        }
    }

    public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IAttackCommand> _onCreate;
        private SelectedItem _currentTarget;

        [Inject]
        private void Init(SelectedItem currentGroundPosition)
        {
            _currentTarget = currentGroundPosition;
            currentGroundPosition.OnSelected += HandleCurrentGroundPositionChanged;
        }

        private void HandleCurrentGroundPositionChanged(ISelectableItem onSelected)
        {
            onSelected = _currentTarget.Value;
            _onCreate?.Invoke(_context.Inject(new AttackUnitCommand(onSelected)));
        }

        protected override void CreateSpecificCommand(Action<IAttackCommand> onCreate)
        {
            _onCreate = onCreate;
        }
    }

    public class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void CreateSpecificCommand(Action<IStopCommand> onCreate)
        {
            onCreate?.Invoke(_context.Inject(new StopUnitCommand()));
        }
    }
}
