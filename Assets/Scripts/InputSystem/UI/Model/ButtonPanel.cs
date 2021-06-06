﻿using Abstractions;
using InputSystem;
using Zenject;

 
namespace Model
{
    public class ButtonPanel
    {
        [Inject] private CommandCreatorBase<IProduceUnitCommand> _produceUnitCommandCreator;
        [Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacklCommandCreator;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patrolCommandCreator;
        [Inject] private CommandCreatorBase<IStopCommand> _stopCommandCreator;

        private bool _isPending;

        public void HandleClick(ICommandExecutor commandExecutor)
        {
            CancelPendingCommand();

            _isPending = true;

            _produceUnitCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _moveCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _attacklCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _patrolCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
            _stopCommandCreator.CreateCommand(commandExecutor, command => ExecuteSpecificCommand(commandExecutor, command));
        }

        public void HandleSelectionChanged()
        {
            CancelPendingCommand();
        }

        private void CancelPendingCommand()
        {
            if (!_isPending) return;

            _produceUnitCommandCreator.CancelCommand();
            _moveCommandCreator.CancelCommand();
            _attacklCommandCreator.CancelCommand();
            _patrolCommandCreator.CancelCommand();
            _stopCommandCreator.CancelCommand();
        }

        private void ExecuteSpecificCommand(ICommandExecutor executor, ICommand command)
        {
            executor.Execute(command);
            _isPending = false;
        }
    }
}
