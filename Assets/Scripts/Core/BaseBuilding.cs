using Abstractions;
using UnityEngine;


namespace Core
{
    public class BaseBuilding : MonoBehaviour, ISelectableItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        public Vector3 Position => transform.position;
        public Sprite Icon => _icon;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
    }
}
