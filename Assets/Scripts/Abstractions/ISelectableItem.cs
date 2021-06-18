using UnityEngine;


namespace Abstractions
{
    public interface ISelectableItem : IHealthHolder
    {
        Vector3 Position { get; }
        Sprite Icon { get; }        
    }
}
