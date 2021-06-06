using UnityEngine;
using Abstractions;
using System.Threading.Tasks;
using System;

namespace Core
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {        
        protected override void ExecuteConcreteCommand(IPatrolCommand command)
        {
            Patrol(command.FirstPosition, command.SecondPosition);
        }

        protected override void AnimationLocomotion()
        {
            
        }

        private async void Patrol(Vector3 first, Vector3 second)
        {
            while (true)
            {
                await MoveTo(first);
                await MoveTo(second);
            }            
        }

        private async Task MoveTo(Vector3 whereToMove)
        {
            _agent.SetDestination(whereToMove);

            while ((transform.position - whereToMove).sqrMagnitude >= _agent.stoppingDistance * _agent.stoppingDistance)
            {
                await Task.Yield();
            }
        }
    }
}
