using Abstractions;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;


namespace Core
{
    public class ProduceUnitExecutor : CommandExecutorBase<IProduceUnitCommand>, ITickable, IUnitProducer
    {
        private readonly float _offset = 2.0f;

        public IReactiveCollection<IUnitProductionTask> Queue => _queue;
        public IReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();        

        protected override void ExecuteConcreteCommand(IProduceUnitCommand command)
        {     
            if (command.UnitPrefab == null)
            {
                Debug.LogError("No Prefab");
                return;
            }

            _queue.Add(new UnitProductionTask(command.Icon, command.ProductionTime, command.UnitPrefab));            
        }

        protected override void AnimationLocomotion()
        {
            
        }

        public void Tick()
        {
            if (_queue.Count == 0) return;

            var currentTask = _queue[0];
            currentTask.ProductionTimeLeft -= Math.Min(Time.deltaTime, currentTask.ProductionTimeLeft);

            if (currentTask.ProductionTime <= 0)
            {
                CreateUnit(currentTask);
                _queue.Remove(currentTask);
            }
        }

        private void CreateUnit(IUnitProductionTask task)
        {
            var creation = Instantiate(task.UnitPrefab, 
                transform.position + Vector3.forward * _offset, 
                Quaternion.identity, transform.parent);

            if (creation.GetComponent<MoveCommandExecutor>() != null)
            {
                var unit = creation.GetComponent<MoveCommandExecutor>();
                unit.MoveToPosition(transform.position + Vector3.forward * _offset);
            }            
        }
    }
}
