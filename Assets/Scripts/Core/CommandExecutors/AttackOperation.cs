using UnityEngine;
using Abstractions;
using System.Threading;
using System;
using Utils;
using Zenject;


namespace Core
{
    public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>, ITickable
    {
        private class AttackOperation : IAwaitable<AsyncExtensions.Void>
        {
            private readonly AttackCommandExecutor _attacker;
            private readonly IAttackable _target;

            private bool _idCancelled;            

            public event Action OnComplete;

            public AttackOperation(IAttackable target, AttackCommandExecutor attacker)
            {
                _target = target;
                _attacker = attacker;
                var thread = new Thread(AttackRoutine);
                thread.Start();
            }

            private void AttackRoutine()
            {
                while (true)
                {
                    if (_idCancelled || _target.Health <= 0)
                    {                        
                        OnComplete?.Invoke();
                        return;
                    }

                    Vector3 targetPosition;
                    Vector3 attackerPosition;

                    lock (_attacker)
                    {
                        targetPosition = _attacker._targetPosition;
                        attackerPosition = _attacker._attackerPosition;
                    }

                    var distance = (targetPosition - attackerPosition).sqrMagnitude;

                    if (distance > _attacker._attackDistance)
                    {
                        //подойти
                        _attacker._destination.OnNext(targetPosition);
                        Thread.Sleep(200);
                    }
                    else
                    {
                        //атаковать
                        _attacker._attackTarget.OnNext(_target);
                        Thread.Sleep((int)(0.3f * 1000));
                    }
                }
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter()
            {
                return new AttackOperationAwaiter(this);
            }

            public class AttackOperationAwaiter : IAwaiter<AsyncExtensions.Void>
            {
                private bool _isCompleted;
                public bool IsCompleted => _isCompleted;

                private AttackOperation _operation;
                private Action _continuation;

                public AttackOperationAwaiter(AttackOperation operation)
                {
                    _operation = operation;
                    _operation.OnComplete += HandleRoutineCompleted;
                }

                private void HandleRoutineCompleted()
                {
                    _operation.OnComplete -= HandleRoutineCompleted;
                    _isCompleted = true;
                    _continuation?.Invoke();
                }

                public AsyncExtensions.Void GetResult()
                {
                    return new AsyncExtensions.Void();
                }

                public void OnCompleted(Action continuation)
                {
                    _continuation = continuation;

                    if (IsCompleted) _continuation?.Invoke();
                }
            }

            internal void Cancel()
            {
                _idCancelled = true;
            }
        }
    }
}