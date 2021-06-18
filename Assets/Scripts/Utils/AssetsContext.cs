using System;
using System.Linq;
using UnityEngine;


namespace Utils
{
    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Strategy/" + nameof(AssetsContext))]
    public class AssetsContext : ScriptableObject
    {
        [SerializeField] private GameObject[] _assets;
        [SerializeField] private Sprite[] _sprites;

        public GameObject GetAsset(string assetName)
        {
            return _assets.FirstOrDefault(asset => asset.gameObject.name == assetName);
        }

        public Sprite GetSprite(string spriteName)
        {
            return _sprites.FirstOrDefault(asset => asset.name == spriteName);
        }
    }
}
