using UnityEngine;
using Abstractions;


[CreateAssetMenu(fileName = nameof(SelectedItem), menuName = "Strategy/" + nameof(SelectedItem))]
public class SelectedItem : ValuesBase<ISelectableItem>
{
    
}
