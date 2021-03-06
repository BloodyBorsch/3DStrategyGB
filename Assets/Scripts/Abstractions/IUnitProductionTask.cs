using UnityEngine;
using UniRx;


namespace Abstractions
{
    public interface IUnitProductionTask
    {
        Sprite Icon { get; }
        ReactiveProperty<float> ProductionTimeLeft { get; set; }
        float ProductionTime { get; }      
        public GameObject UnitPrefab { get; }
    }
}