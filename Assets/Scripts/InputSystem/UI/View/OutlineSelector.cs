using System.Linq;
using UnityEngine;


public class OutlineSelector : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers;
    [SerializeField] private Material _outlineMaterial;

    private bool _isSelectedCache;

    public void SetSelected(bool isSelected)
    {
        if (isSelected == _isSelectedCache)
        {
            return;
        }        

        if (_renderers.Length > 0) SetMeshRenderer(isSelected, _renderers);
        else if (_skinnedMeshRenderers.Length > 0) SetSkinnedMeshRenderer(isSelected, _skinnedMeshRenderers);
        else
        {
            Debug.Log("No renderer");
            return;
        }

        _isSelectedCache = isSelected;
    }

    private void SetMeshRenderer(bool selected, MeshRenderer[] meshRenderers)
    {    
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            var renderer = meshRenderers[i];
            var materialsList = renderer.materials.ToList();
            if (selected)
            {
                materialsList.Add(_outlineMaterial);
            }
            else
            {
                materialsList.RemoveAt(materialsList.Count - 1);
            }
            renderer.materials = materialsList.ToArray();
        }
    }

    private void SetSkinnedMeshRenderer(bool selected, SkinnedMeshRenderer[] meshRenderers)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            var renderer = meshRenderers[i];
            var materialsList = renderer.materials.ToList();
            if (selected)
            {
                materialsList.Add(_outlineMaterial);
            }
            else
            {
                materialsList.RemoveAt(materialsList.Count - 1);
            }
            renderer.materials = materialsList.ToArray();
        }
    }

}
