using UnityEngine;


namespace Abstractions
{
    public interface IUnitProductionTask
    {
        Sprite Icon { get; }
        float ProductionTime { get; }
        float ProductionTimeLeft { get; set; }

        public GameObject UnitPrefab { get; }
    }
}