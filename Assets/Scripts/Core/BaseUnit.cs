using UnityEngine;
using Abstractions;


namespace Core
{
    public class BaseUnit : MonoBehaviour, ISelectableItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        public Sprite Icon => _icon;
        public float Health => _health;
        public float MaxHealth => _maxHealth;        
    }
}
