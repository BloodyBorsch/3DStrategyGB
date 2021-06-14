using Abstractions;
using UnityEngine;


namespace Core
{
    public class UnitProductionTask : IUnitProductionTask
    {
        public Sprite Icon { get; }

        public float ProductionTime { get; }

        public float ProductionTimeLeft { get; set; }

        public GameObject UnitPrefab { get; }

        public UnitProductionTask(Sprite icon, float productionTime, GameObject unitPrefab)
        {
            productionTime = 100;
            Icon = icon;
            ProductionTime = productionTime;
            ProductionTimeLeft = productionTime;
            UnitPrefab = unitPrefab;
        }
    }
}