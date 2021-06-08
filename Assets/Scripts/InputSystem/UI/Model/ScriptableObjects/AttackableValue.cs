using UnityEngine;
using Abstractions;


[CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Strategy/" + nameof(AttackableValue))]
public class AttackableValue : ValuesBase<IAttackable>
{

}
