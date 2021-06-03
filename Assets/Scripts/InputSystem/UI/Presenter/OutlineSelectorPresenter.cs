using Abstractions;
using UnityEngine;


public class OutlineSelectorPresenter : MonoBehaviour
{
    [SerializeField] private SelectedItem _selectable;

    private OutlineSelector[] _outlineSelectors;
    private ISelectableItem _currentSelectable;

    private void Start()
    {
        _selectable.OnSelected += DrawOutline;        
        DrawOutline(_selectable.Value);
    }

    private void DrawOutline(ISelectableItem selected)
    {
        if (_currentSelectable == selected)
        {
            return;
        }
        _currentSelectable = selected;

        SetOutline(_outlineSelectors, false);
        _outlineSelectors = null;

        if (selected != null)
        {
            _outlineSelectors = (selected as Component).GetComponentsInParent<OutlineSelector>();
            SetOutline(_outlineSelectors, true);
        }

        static void SetOutline(OutlineSelector[] selectors, bool value)
        {
            if (selectors != null)
            {
                for (int i = 0; i < selectors.Length; i++)
                {
                    selectors[i].SetSelected(value);
                }
            }
        }
    }
}
