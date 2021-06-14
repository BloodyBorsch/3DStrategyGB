using Abstractions;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


namespace Model
{
    public class UnitProductionPanel
    {
        public IReactiveCollection<IUnitProductionTask> UnitProductionQueue =
            new ReactiveCollection<IUnitProductionTask>();

        public void HandleSelectionChanged(ISelectableItem value)
        {            
            var unitProducer = (value as Component)?.GetComponent<IUnitProducer>();

            UnitProductionQueue = unitProducer?.Queue;
        }
    }
}