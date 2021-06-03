using Abstractions;
using UnityEngine;


public class SelectedItemPresenter : MonoBehaviour
{
    [SerializeField] private SelectedItemView _view;
    [SerializeField] private SelectedItem _item;

    private readonly string _healthTitle = "Health";

    private void Start()
    {
        _item.OnSelected += UpdateView;

        UpdateView(_item.Value);
    }

    private void UpdateView(ISelectableItem selected)
    {
        var hasItem = selected != null;

        _view.gameObject.SetActive(hasItem);

        if (!hasItem) return;

        _view.Icon = selected.Icon;
        _view.MaxHealth = selected.MaxHealth;
        _view.Health = selected.Health;        
        _view.Text = _healthTitle;
        _view.BackGroundText = $"{selected.Health} / {selected.MaxHealth}";        
    }
}
