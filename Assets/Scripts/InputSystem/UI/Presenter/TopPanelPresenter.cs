using Abstractions;
using UnityEngine;
using Zenject;
using UniRx;
using System;

namespace InputSystem
{
    public class TopPanelPresenter : MonoBehaviour
    {
        [SerializeField] TopPanelView _view;
        [SerializeField] MenuPresenter _menu;

        private ITimeModel _timeModel;

        [Inject]
        public void Init(ITimeModel timeModel)
        {
            _timeModel = timeModel;
            _timeModel.GameTime.Subscribe(value => _view.Time = value);
            _menu.Initialize(_timeModel);
            _view.MenuButton.OnClickAsObservable().Subscribe(unit => HandleMenuButtonClick());
        }

        private void HandleMenuButtonClick()
        {
            //покажем меню
            _menu.gameObject.SetActive(true);

            //запаузим игру
            _timeModel.Pause();
        }
    }
}