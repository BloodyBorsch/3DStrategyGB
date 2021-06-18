using Abstractions;
using Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using View;
using Zenject;


namespace Presenter
{
    public class UnitProductionPresenter : MonoBehaviour
    {
        [Inject] private SelectedItem _item;
        [Inject] private UnitProductionPanel _productionPanel;

        [SerializeField] UnitProductionView _view;

        private ISelectableItem _currentSelected;

        private void Start()
        {
            _item.OnSelected += HandleSelectionChanged;
        }

        private void HandleSelectionChanged(ISelectableItem value)
        {
            if (_currentSelected == _item.Value) return;

            _currentSelected = _item.Value;
            _productionPanel.HandleSelectionChanged(_currentSelected);
            UpdateUnitProductionQueue();
        }

        private void UpdateUnitProductionQueue()
        {
            if (_productionPanel.UnitProductionQueue == null)
            {
                return;
            }

            _view.DisplayQueue(_productionPanel.UnitProductionQueue);

            _productionPanel.UnitProductionQueue.ObserveAdd().Subscribe(addEvent =>
            {
                _view.AddNewItem(addEvent);
            }).AddTo(this);
        }
    }
}