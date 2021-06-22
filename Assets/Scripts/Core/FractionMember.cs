using Abstractions;
using UnityEngine;


namespace Core
{
    public class FractionMember : MonoBehaviour, IFractionMember
    {
        [SerializeField] private FractionTeam _team;

        public FractionTeam Team => _team;

        protected void Start()
        {
            AutoAttackGlobalBehavior.Units.AddOrUpdate(gameObject.GetComponent<BaseUnit>(), this, (u, fraction) => fraction);
        }

        protected void OnDestroy()
        {
            AutoAttackGlobalBehavior.Units.TryRemove(gameObject.GetComponent<BaseUnit>(), out var value);
        }
    }

    
}