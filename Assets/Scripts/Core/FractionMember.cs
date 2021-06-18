using Abstractions;
using UnityEngine;


namespace Core
{
    public class FractionMember : MonoBehaviour, IFractionMember
    {
        [SerializeField] private int _id;
        public int Id => _id;
    }
}