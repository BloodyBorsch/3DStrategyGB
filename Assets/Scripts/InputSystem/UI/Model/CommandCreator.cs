using Abstractions;
using System;
using System.Threading;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        [Inject] protected AssetsContext _context;
        [Inject] protected Vector3Value _currentGroundPosition;
        [Inject] protected SelectedItem _selectedItem;

        public void CreateCommand(ICommandExecutor commandExecutor, Action<T> onCreate)
        {
            if (commandExecutor as CommandExecutorBase<T>)
            {
                CreateSpecificCommand(onCreate);
            }
        }

        public virtual void CancelCommand() { }

        protected abstract void CreateSpecificCommand(Action<T> onCreate);
    }

    public abstract class CancelableCommandCreatorBase<T, TParam> : CommandCreatorBase<T> where T : ICommand
    {
        protected CancellationTokenSource _cancellationSource;        
        
        [Inject] private IAwaitable<TParam> _param;

        protected override async void CreateSpecificCommand(Action<T> onCreate)
        {
            _cancellationSource = new CancellationTokenSource();

            try
            {
                var param = await _param.AsTask().WithCancellation(_cancellationSource.Token);
                onCreate?.Invoke(_context.Inject(CreateSpecificCommand(param)));
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("Operation Canceled");
            }
        }

        protected abstract T CreateSpecificCommand(TParam param);

        public override void CancelCommand()
        {
            if (_cancellationSource == null) return;

            _cancellationSource.Cancel();
            _cancellationSource.Dispose();
            _cancellationSource = null;
        }
    }

    public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {       
        protected override void CreateSpecificCommand(Action<IProduceUnitCommand> onCreate)
        {
            onCreate?.Invoke(_context.Inject(new ProduceUnitCommand()));
        }
    }

    public class MoveCommandCreator : CancelableCommandCreatorBase<IMoveCommand, Vector3>
    {         
        protected override IMoveCommand CreateSpecificCommand(Vector3 param)
        {
            return new MoveUnitCommand(param);
        }
    }

    public class PatrolCommandCreator : CancelableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        protected override IPatrolCommand CreateSpecificCommand(Vector3 param)
        {
            return new PatrolUnitCommand(param, _selectedItem.Value.Position);
        }
    }

    public class AttackCommandCreator : CancelableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateSpecificCommand(IAttackable param)
        {
            return new AttackUnitCommand(param);
        }
    }

    public class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        protected override void CreateSpecificCommand(Action<IStopCommand> onCreate)
        {            
            onCreate?.Invoke(_context.Inject(new StopUnitCommand()));
        }
    }
}
