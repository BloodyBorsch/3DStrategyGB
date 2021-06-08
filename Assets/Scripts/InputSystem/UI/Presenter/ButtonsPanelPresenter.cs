using Abstractions;
using Zenject;
using System.Linq;
using UnityEngine;
using Model;
using Utils;


namespace InputSystem
{
    public class ButtonsPanelPresenter : MonoBehaviour
    {
        [SerializeField] private ButtonsPanelView _view;
        [Inject] private SelectedItem _item;
        [Inject] private AttackableValue _target;
        [Inject] private ButtonPanel buttonPanel;

        private ISelectableItem _currentSelected;        

        private void Start()
        {
            _item.OnSelected += HandleSelectionChanged;            
            SetButtons(_item.Value);
            _view.OnClick += HandleClick;            
        }

        private void HandleSelectionChanged(ISelectableItem value)
        {
            value = _item.Value;
            buttonPanel.HandleSelectionChanged();
            SetButtons(_item.Value);
        }

        private void SetButtons(ISelectableItem value)
        {
            if (_currentSelected == value) return;

            _currentSelected = value;
            _view.ClearButtons();

            var commandExecutors = (_currentSelected as Component)?.GetComponentsInParent<ICommandExecutor>().ToList();

            _view.SetButtons(commandExecutors);
        }

        private void HandleClick(ICommandExecutor executor)
        {            
            buttonPanel.HandleClick(executor);            
        }
    }
}
