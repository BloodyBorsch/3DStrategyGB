using Abstractions;
using UnityEngine;
using UniRx;


namespace Core
{
    public class UnitProductionTask : IUnitProductionTask
    {
        public Sprite Icon { get; }

        public float ProductionTime { get; }

        public ReactiveProperty<float> ProductionTimeLeft { get; set; }

        public GameObject UnitPrefab { get; }

        public UnitProductionTask(Sprite icon, float productionTime, GameObject unitPrefab)
        {
            productionTime = 4.0f;
            Icon = icon;
            ProductionTime = productionTime;
            ProductionTimeLeft = new ReactiveProperty<float>(productionTime);
            UnitPrefab = unitPrefab;
        }
    }
}