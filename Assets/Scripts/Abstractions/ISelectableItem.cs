using UnityEngine;


namespace Abstractions
{
    public interface ISelectableItem
    {
        Vector3 Position { get; }
        Sprite Icon { get; }
        float Health { get; }
        float MaxHealth { get; }   
    }
}
